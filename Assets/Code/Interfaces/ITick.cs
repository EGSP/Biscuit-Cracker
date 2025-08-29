using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Code.Interfaces
{
    public interface ITick
    {
        UnityEvent OnDestroy { get; }

        public void FixedTick(float deltaTime) { return; }
        public void Tick(float deltaTime) { return; }
        public void LateTick(float deltaTime) { return; }

        public void Destroy()
        {
            OnDestroy?.Invoke();
        }
    }
}