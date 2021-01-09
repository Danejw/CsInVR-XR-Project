using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] protected bool isActive;


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

        public void SetBallIsActive(bool value)
        {
            isActive = value;
        }
        
        public bool GetBallIsActive()
        {
            return isActive;
        }
    }
}
