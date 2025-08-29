using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Code.Ui
{
    public class NoiseBadge : MonoBehaviour
    {
        public TMP_Text noiseText;

        public void UpdateNoise(float noise, float noiseAdded)
        {
            noise = MathF.Round(noise, 0);
            noiseText.text = noise.ToString();
        }
    }
}