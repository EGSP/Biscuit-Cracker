using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Scriptable Objects/Sound")]
public class SoundConfig : ScriptableObject
{
    public AudioClip AudioClip;
    public string Id;

    public bool InterruptSameId;
    public float InterruptThresholdSeconds = 0.1f;

}
