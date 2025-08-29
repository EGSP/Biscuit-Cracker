using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Assets.Code
{
    public class PlayerHand : MonoBehaviour
    {
        public float ClickRadius = 0.5f;

        public UnityEvent<Biscuit> OnBiscuitClicked = new UnityEvent<Biscuit>();

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            var clicked = InputSystem.actions.FindAction("Primary Click").triggered;
            var mouseScreenPosition = InputSystem.actions.FindAction("Mouse Position").ReadValue<Vector2>();

            // if clicked, get mouse world position
            if (clicked)
            {
                Debug.Log("Primary Click detected");
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
                ProceedClick(mousePos);
            }
        }

        private void ProceedClick(Vector3 mousePos)
        {
            var biscuitsToDespawn = new List<Biscuit>();
            foreach (Biscuit biscuit in GameManager.Instance.GetSpawnedBiscuits())
            {
                if(biscuit == null) continue;
                var clampedMousePos = new Vector2(mousePos.x, mousePos.z);
                var clampedBiscuitPos = new Vector2(biscuit.transform.position.x, biscuit.transform.position.z);

                // check if within click radius
                if (Vector2.Distance(clampedMousePos, clampedBiscuitPos) < ClickRadius)
                {
                    // Handle biscuit click
                    biscuit.ClickedPoints++;
                    OnBiscuitClicked?.Invoke(biscuit);

                    Debug.Log($"Biscuit clicked! Total clicks: {biscuit.ClickedPoints}");
                    if(biscuit.ClickedPoints >= biscuit.ClickPoints)
                    {
                        Debug.Log("Biscuit fully clicked!");
                        // Add score logic here
                        ProceedValue(biscuit);
                        biscuitsToDespawn.Add(biscuit);
                    }
                }
            }

            foreach (var biscuit in biscuitsToDespawn)
            {
                GameManager.Instance.DestroyBisquit(biscuit);
            }
        }

        private void ProceedValue(Biscuit biscuit)
        {
            int value = biscuit.ClickPoints;

            GameManager.Instance.AddScore(value);

            Debug.Log("Collected biscuit worth " + value + " points!");
        }

        // draw click radius gizmo
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.beige;
            Gizmos.DrawWireSphere(transform.position, ClickRadius);
        }
    }
}