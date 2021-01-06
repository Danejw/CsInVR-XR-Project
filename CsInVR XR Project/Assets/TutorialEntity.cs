using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Tutorial
{
    public class TutorialEntity : MonoBehaviour
    {
        public Transform lookAtObject;
        public GameObject goToObject;

        public float radius;
        public float maxHeight = 10;
        public float minHeight = 1;

        [SerializeField] float forceAmount = 1;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // look at the object 
            if (lookAtObject)
                this.transform.LookAt(lookAtObject);

                if (goToObject)
                    if (transform.position.y < minHeight)
                        ApplyForceUpward();
                    else if (transform.position.y > maxHeight)
                        ApplyForceDownward();
                    else
                        ApplyForceInDirectionOf(goToObject.transform);
        }

        private void ApplyForceInDirectionOf(Transform position)
        {
            Vector3 forceDirection = transform.position - position.position;
            if (forceDirection.magnitude > radius)
                GetComponent<Rigidbody>().AddForce(-forceDirection * forceAmount, ForceMode.Acceleration);
            else
                GetComponent<Rigidbody>().AddForce(forceDirection * forceAmount, ForceMode.Acceleration);
        }

        // move up if below a certain height
        private void ApplyForceUpward()
        {
            GetComponent<Rigidbody>().AddForce(transform.up * forceAmount, ForceMode.Acceleration);
        }

        // move dowwn if above a certain height
        private void ApplyForceDownward()
        {
            GetComponent<Rigidbody>().AddForce(-transform.up * forceAmount, ForceMode.Acceleration);
        }
    }
}
