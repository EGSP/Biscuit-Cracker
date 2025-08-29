using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Assets.Code.Ui
{
    public class PauseMenu : MonoBehaviour
    {
        public UnityEvent<bool> OnMenuStateChanged = new UnityEvent<bool>();

        public GameObject MenuUi;

        public void Awake()
        {
            if(MenuUi.activeSelf)
            {
                MenuUi.SetActive(false);
            }
        }

        public void LateUpdate()
        {
            var escapePressed = InputSystem.actions.FindAction("Escape").triggered;
            if (escapePressed)
            {
                ToggleMenu();
            }
        }

        public void ToggleMenu()
        {
            MenuUi.SetActive(!MenuUi.activeSelf); // toogle exclamation mark
            OnMenuStateChanged?.Invoke(MenuUi.activeSelf); // if menu is active, game is paused (false), if menu is inactive, game is unpaused (true)
        }
    }
}