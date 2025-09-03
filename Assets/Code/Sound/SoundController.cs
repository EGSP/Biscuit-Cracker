using UnityEngine;

namespace Assets.Code.Sound
{
    public class SoundController : MonoBehaviour
    {
        public SoundConfig SoundConfig { get; set; }

        public float TimeFromStart { get; set; }

        private AudioSource _audioSource;

        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Update()
        {
            TimeFromStart += Time.deltaTime;
        }

        public void StopSound() => _audioSource.Stop();

        public void PlaySound()
        {
            if (SoundConfig == null) return;

            TimeFromStart = 0;
            _audioSource.clip = SoundConfig.AudioClip;
            _audioSource.Play();
        }
    }
}