using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Light))]

    public class LightVelocity : MonoBehaviour
    {
        public Rigidbody rigidbody;

        [SerializeField] private float min;
        [SerializeField] private float max;

        private float velocity;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            velocity = rigidbody.velocity.magnitude;
            GetComponent<Light>().intensity = velocity;
        }
    }
}
