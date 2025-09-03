
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Code.Sound
{
    public class SoundManager : MonoBehaviour
    {
        public List<SoundConfig> Sounds = new List<SoundConfig>();

        private List<SoundController> _soundControllers = new List<SoundController>();

        private void Update()
        {
            // В целом можно не удалять использованные контроллеры
        }

        public void PlaySound(string soundId)
        {
            var soundConfig = Sounds.Find(x => x.Id == soundId);
            if (soundConfig == null) return;

            var soundController = _soundControllers.FirstOrDefault(x => x.SoundConfig.Id == soundId);
            // Если уже есть контроллер
            if (soundController != null)
            {
                // Если нужно прервать текущий звук
                if (soundController.SoundConfig.InterruptSameId)
                {
                    // Если звук еще не преодолел минимальный порог звучания
                    if (soundController.TimeFromStart < soundConfig.InterruptThresholdSeconds) return;

                    soundController.StopSound();
                    soundController.PlaySound();
                    return;
                }

            }

            var gameObject = new GameObject($"Sound {soundId}", typeof(AudioSource), typeof(SoundController));
            gameObject.transform.parent = transform;
            soundController = gameObject.GetComponent<SoundController>();
            soundController.SoundConfig = soundConfig;
            _soundControllers.Add(soundController);

            soundController.PlaySound();

        }
    }
}