using Assets.Code;
using System;
using Unity.Mathematics;
using UnityEngine;

public class NoiseDecayUpgrade : MonoBehaviour
{
    public float BaseCost = 1;
    public float CostMultiplier = 1.75f;
    public float DecayMultiplier = 1.15f;

    public float Level { get; set; } = 0;

    public void AddModifier(BiscuitNoiseManager noiseManager)
    {
        noiseManager.DecayRateModifiers.Enqueue(GetModifiedDecayRate);
    }

    public float GetModifiedDecayRate(float baseDecay)
    {
        return baseDecay * CalculateDecayRate();
    }

    private float CalculateDecayRate()
    {
        return Mathf.Pow(DecayMultiplier, Level);
    }

    public float GetCost()
    {
        var cost = BaseCost * Mathf.Pow(CostMultiplier, Level);
        Debug.Log(cost);
        return cost;
    }

    public string GetUpgradeSummury()
    {
        var decayPercent = (CalculateDecayRate()-1)*100;
        return $"Noise decay rate +{decayPercent}%";
    }
}
