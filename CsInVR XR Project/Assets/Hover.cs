using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR.Football
{
    public class Hover : MonoBehaviour
    {
        //Inputs
        public float degreesPerSecond = 15;
        public float amplitude = .5f;
        public float frequency = 1;

        // position storage variables
        Vector3 posOffset = new Vector3();
        Vector3 tempPos = new Vector3();

        // use this for initialization
        private void Start()
        {
            posOffset = transform.position;
        }

        private void Update()
        {
            // spin object in the Y axis
            transform.Rotate(new Vector3(0, Time.deltaTime * degreesPerSecond, 0), Space.World);

            // float up/dpwn with Sin()
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
        }
    }
}
