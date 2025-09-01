using System.Collections;
using UnityEngine;

namespace Assets.Code.Ui.Actions
{
    public class Exit : MonoBehaviour
    {
        public void Quit()
        {
            Application.Quit();
        }
    }
}