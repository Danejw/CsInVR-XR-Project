using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR
{
    public class addForce : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Ball")
            {
                //add force to the other object. Velocity, Direction, Rotation
                other.GetComponent<Rigidbody>().AddForceAtPosition(-this.GetComponent<Rigidbody>().velocity, other.transform.position, ForceMode.Force);
            }
        }
    }
}
