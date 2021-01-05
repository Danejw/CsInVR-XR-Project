using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR.Football
{
    public class BallRotator : MonoBehaviour
    {
        public bool debug;

        Rigidbody rig;
        float minSpiralVelocity;

        // Start is called before the first frame update
        void Start()
        {
            rig = GetComponentInParent<Rigidbody>();
            minSpiralVelocity = GetComponentInParent<HikeBall>().minSpiralVelocity;
        }

        // Update is called once per frame
        void Update()
        {
            // if ball is moving faster than a minimal velocity
            if (rig)
                if  (rig.velocity.magnitude > minSpiralVelocity)
                    Spiral();
        }

        private void Spiral()
        {
            // rotate object
            transform.Rotate(Vector3.forward * Time.deltaTime * rig.velocity.magnitude, rig.velocity.magnitude);

            if (debug) Debug.Log("The ball is rotating");
        }
    }
}
