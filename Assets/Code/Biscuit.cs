using System.Collections;
using UnityEngine;

namespace Assets.Code
{
    public class Biscuit : MonoBehaviour
    {
        public float Speed = 1f;

        public int ClickPoints { get; set; } = 1;

        public int clickedPoints = 0;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }
    }
}