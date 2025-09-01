using Assets.Code.Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Code
{
    public class Biscuit : MonoBehaviour, ITick
    {
        public float Speed = 1f;

        public int ClickPoints { get; set; } = 1;
        public int ClickedPoints
        {
            get => clickedPoints;
            set
            {
                var oldValue = clickedPoints;
                clickedPoints = value;
                OnClickedPointsChanged.Invoke(clickedPoints, clickedPoints - oldValue);
                OnClick.Invoke(this);
            }
        }



        private int clickedPoints = 0;

        public UnityEvent<int, int> OnClickedPointsChanged = new UnityEvent<int,int>();

        public UnityEvent<Biscuit> OnClick = new UnityEvent<Biscuit>();

        public UnityEvent OnDestroy { get; } = new UnityEvent();

        public void Tick(float deltaTime)
        {
            // Move the biscuit to the right over time
            transform.Translate(Vector3.right * Speed * deltaTime);
        }

        public void Destroy()
        {
            OnDestroy?.Invoke();
            Destroy(gameObject);
        }
    }
}