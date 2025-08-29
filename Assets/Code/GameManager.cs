using Assets.Code.Interfaces;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Code
{
    public class GameManager : MonoBehaviour
    {
        public int Score = 0;

        private List<Biscuit> SpawnedBiscuits = new List<Biscuit>();

        private bool isGamePaused = false;

        public UnityEvent<bool> OnGamePause = new UnityEvent<bool>();

        /// <summary>
        /// Event int currentScore, int scoreAdded.  
        /// scoreAdded is 0 on initialization
        /// </summary>
        public UnityEvent<int, int> OnScoreChanged = new UnityEvent<int, int>();

        public UnityEvent<Biscuit> OnBiscuitSpawned = new UnityEvent<Biscuit>();

        public UnityEvent<Biscuit> OnBiscuitDespawned = new UnityEvent<Biscuit>();

        public static GameManager Instance { get; private set; }

        private List<ITick> TickRegisteredObjects = new List<ITick>();
        private List<ITick> TickObjectsToRemove = new List<ITick>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
                Instance = this;
            }
            else
            {
                Instance = this;
            }

            Score = 0;
            SpawnedBiscuits.Clear();

            if (OnScoreChanged == null)
                OnScoreChanged = new UnityEvent<int, int>();

            OnScoreChanged?.Invoke(Score, 0);
        }

        public void Update()
        {
            if (isGamePaused)
                return;

            float deltaTime = Time.deltaTime;
            // Update all biscuits
            foreach (var biscuit in SpawnedBiscuits)
            {
                biscuit.Tick(deltaTime);
            }

            // Update all registered ITick objects
            foreach (var obj in TickRegisteredObjects)
            {
                obj.Tick(deltaTime);
            }

            // Remove objects that are marked for removal
            if (TickObjectsToRemove.Count > 0)
            {
                foreach (var obj in TickObjectsToRemove)
                {
                    TickRegisteredObjects.Remove(obj);
                }
                TickObjectsToRemove.Clear();
            }
        }

        public void AddScore(int add)
        {
            Score += add;
            OnScoreChanged?.Invoke(Score, add);
        }

        public IReadOnlyCollection<Biscuit> GetSpawnedBiscuits()
        {
            return SpawnedBiscuits.AsReadOnly();
        }

        public void RegisterBiscuit(Biscuit biscuit)
        {
            if (!SpawnedBiscuits.Contains(biscuit))
                SpawnedBiscuits.Add(biscuit);

            OnBiscuitSpawned?.Invoke(biscuit);
        }

        public void DestroyBisquit(Biscuit biscuit)
        {
            // Call biscuit`s event before destroying
            biscuit.Destroy();

            if (SpawnedBiscuits.Contains(biscuit))
            {
                SpawnedBiscuits.Remove(biscuit);
                OnBiscuitDespawned?.Invoke(biscuit);
                Destroy(biscuit.gameObject);
            }
            else
            {
                Debug.LogWarning("Attempted to destroy a biscuit that is not registered.");
                Destroy(biscuit.gameObject);
            }
        }

        public void PauseGame(bool pause)
        {
            isGamePaused = pause;
            OnGamePause?.Invoke(isGamePaused);
        }

        public void ReloadScene()
        {
            // Handle game over logic here
            var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.name);
        }

        public void RegisterTickObject(ITick obj)
        {
            if (!TickRegisteredObjects.Contains(obj))
            {
                obj.OnDestroy.AddListener(() => TickObjectsToRemove.Add(obj));
                TickRegisteredObjects.Add(obj);
            }
        }
    }
}