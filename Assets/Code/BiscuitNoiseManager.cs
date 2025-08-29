using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Code
{
    public class BiscuitNoiseManager : MonoBehaviour
    {
        public float Meter { get; private set; } = 0f; // Current noise level
        public float DecayRate = 2.5f; // Noise decay rate per second
        public float Max = 100f; // Maximum noise level

        public Vector2 ClickNoiseRange = new Vector2(5f, 10f); // Range of noise increase per biscuit click

        public UnityEvent<float,float> OnNoiseChanged = new UnityEvent<float,float>();
        public UnityEvent OnMaxNoiseReached = new UnityEvent();

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            UpdateNoise();
        }

        private void UpdateNoise()
        {
            var decay = DecayRate * Time.deltaTime;
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