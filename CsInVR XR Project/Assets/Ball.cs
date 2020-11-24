using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR
{
    public class Ball : MonoBehaviour
    {
        public bool isActive;


        private void Start()
        {
            isActive = true;
        }

        private void OnEnable()
        {
            isActive = true;
        }

        private void OnDisable()
        {
            isActive = false;
        }
    }
}
