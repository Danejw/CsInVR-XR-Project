using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR.Football
{
    public class PhysicsFloat : MonoBehaviour
    {
        public float yOffset;
        public float amplitude = 0.5f;
        public float frequency = 0.1f;

        Rigidbody rb;
        bool initialConditionSet = false;

            
       // Start is called before the first frame update
        void Start()
        {
            rb = transform.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!initialConditionSet)
            {
                rb.MovePosition(rb.position + new Vector3(0, yOffset, 0));
                rb.velocity = new Vector3(0, amplitude * frequency, 0);
                initialConditionSet = true;
            }

            float freqSqrd = frequency * frequency;
            Vector3 desiredAccel = new Vector3(0, freqSqrd * (yOffset - rb.position.y), 0);
            rb.AddForce(desiredAccel, ForceMode.Acceleration);
        }
    }
}
