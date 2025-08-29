using Assets.Code;
using Assets.Code.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BiscuitSpawner : MonoBehaviour, ITick
{
    public Biscuit BiscuitPrefab;
    public float SpawnIntervalSec = 2f;

    public GameObject SpawnPoint;

    private float _timeSinceLastSpawn = 0f;

    private int _spawnedCount = 0;

    public UnityEvent OnDestroy { get; } = new UnityEvent();

    private void Awake()
    {
        GameManager.Instance.RegisterTickObject(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (BiscuitPrefab == null)
        {
            Debug.LogError("Biscuit prefab is not assigned.");
        }
    }

    // Update is called once per frame
    public void Tick(float deltaTime)
    {
        _timeSinceLastSpawn += deltaTime;
        // spawn if time since last spawn is greater than spawn interval
        if (_timeSinceLastSpawn > SpawnIntervalSec)
        {
            var spawned = SpawnBiscuit();
            if (!spawned)
            {
                Debug.LogError("Failed to spawn biscuit.");
                // Prevent continuous error logging by resetting the timer
                _timeSinceLastSpawn = 0f;
            }
            else
            {
                _timeSinceLastSpawn = 0f;
            }
        }
    }

    public bool SpawnBiscuit()
    {
        if(BiscuitPrefab == null)
        {
            Debug.LogError("Biscuit prefab is not assigned.");
            return false;
        }
        var spawnPosition = SpawnPoint != null ? SpawnPoint.transform.position : transform.position;

        var biscuit = Instantiate(BiscuitPrefab, spawnPosition, Quaternion.identity);
        biscuit.name = $"Biscuit_{_spawnedCount}";
        biscuit.ClickPoints = 1 + (_spawnedCount / 3); // Increase click points every 10 biscuits
        GameManager.Instance.RegisterBiscuit(biscuit);
        _spawnedCount++;


        return true;
    }

    public void Destroy()
    {
        OnDestroy?.Invoke();
    }
}
