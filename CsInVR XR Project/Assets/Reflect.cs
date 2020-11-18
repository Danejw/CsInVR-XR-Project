using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using UnityEngine.XR;

namespace CSInVR
{
    [RequireComponent(typeof(Rigidbody))]

    public class Reflect : MonoBehaviour
    {
        public bool debug;

        public float velocityChangeMultiplier = 1;
        public float forceMultiplier = 1;

        private Rigidbody rig;
        private Rigidbody collidingObjectRig;


        private void Start()
        {
            rig = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Ball")
            {


                // add force into the opposite direction 
                collidingObjectRig = other.GetComponent<Rigidbody>();

                if (collidingObjectRig != null)
                {
                    collidingObjectRig.AddForce(-collidingObjectRig.velocity + transform.up * velocityChangeMultiplier, ForceMode.VelocityChange);
                    collidingObjectRig.AddForce(rig.velocity + transform.up  * forceMultiplier, ForceMode.Impulse);

                    if (debug) Debug.Log("Colliding object velocity: " + collidingObjectRig.velocity);
                    if (debug) Debug.Log("This object's velocity: " + rig.velocity);

                }
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Ball")
            {
                collidingObjectRig = null;
            }
        }

        private void PlaySound()
        {

        }


        private void PlayHptics()
        {

        }


    }
}
