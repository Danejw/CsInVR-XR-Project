using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR
{
    public class Rotator : MonoBehaviour
    {

        [SerializeField] private float speed;

        private void FixedUpdate()
        {
            transform.Rotate(transform.forward * speed * Time.fixedDeltaTime);
        }
    }
}

