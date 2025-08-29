using Assets.Code.Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Code
{
    public class BiscuitNoiseManager : MonoBehaviour, ITick
    {
        public float Meter { get; private set; } = 0f; // Current noise level

        public float DecayRate = 2.5f; // Noise decay rate per second
        public float Max = 100f; // Maximum noise level

        public Vector2 ClickNoiseRange = new Vector2(5f, 10f); // Range of noise increase per biscuit click

        public UnityEvent<float,float> OnNoiseChanged = new UnityEvent<float,float>();
        public UnityEvent OnMaxNoiseReached = new UnityEvent();

        public UnityEvent OnDestroy { get; } = new UnityEvent();

        public void Awake()
        {
            GameManager.Instance.RegisterTickObject(this);
        }

        // Update is called once per frame
        public void Tick(float deltaTime)
        {
            UpdateNoise(deltaTime);
        }

        private void UpdateNoise(float deltaTime)
        {
            var decay = DecayRate * deltaTime;
            Meter -= decay;
            Meter = Mathf.Clamp(Meter, 0f, Max);
            OnNoiseChanged?.Invoke(Meter, decay);
        }

        public void PerformNoise(Biscuit biscuit)
        {
            var clickPoints = biscuit.ClickPoints;

            // Calculate noise increase based on click points
            var noiseIncrease = Random.Range(ClickNoiseRange.x, ClickNoiseRange.y) * clickPoints;
            Meter+= noiseIncrease;
            Meter = Mathf.Clamp(Meter, 0f, Max);
            OnNoiseChanged?.Invoke(Meter, Max);
            if (Meter >= Max)
            {
                OnMaxNoiseReached?.Invoke();
            }
        }

    }
}