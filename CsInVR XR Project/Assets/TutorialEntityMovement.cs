using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Tutorial
{
    public class TutorialEntityMovement : MonoBehaviour
    {
        public Transform lookAtObject;
        public GameObject goToObject;

        public bool applyForceSidways;
        public enum Side
        {
            left,
            right
        }
        public Side side;

        public float minRadius = 3;
        public float maxRadius = 10;
        public float maxHeight = 10;
        public float minHeight = 1;

        [SerializeField] float forceAmount = 1;

        private void OnEnable()
        {
            // Find the player's camera
            if (!lookAtObject) lookAtObject = GameObject.Find("CenterEyeAnchor").transform;
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
                {
                    ApplyForceInDirectionOf(goToObject.transform);

                    if (applyForceSidways)
                    {
                        switch (side)
                        {
                            case Side.left:
                                ApplyForceLeft();
                                break;
                            case Side.right:
                                ApplyForceRight();
                                break;
                            default:
                                break;
                        }
                    }
                }
        }

        private void ApplyForceInDirectionOf(Transform position)
        {
            Vector3 forceDirection = transform.position - position.position;

            if (forceDirection.magnitude > minRadius)
                GetComponent<Rigidbody>().AddForce(-forceDirection * forceAmount, ForceMode.Force);
            else
                GetComponent<Rigidbody>().AddForce(forceDirection * forceAmount, ForceMode.Force);
        }

        // move up if below a certain height
        private void ApplyForceUpward()
        {
            GetComponent<Rigidbody>().AddForce(transform.up * forceAmount, ForceMode.Force);
        }

        // move dowwn if above a certain height
        private void ApplyForceDownward()
        {
            GetComponent<Rigidbody>().AddForce(-transform.up * forceAmount, ForceMode.Force);
        }

        // move to the right
        private void ApplyForceRight()
        {
            Vector3 forceDirection = transform.position - goToObject.transform.position;

            if (forceDirection.magnitude > minRadius && forceDirection.magnitude < maxRadius)
                GetComponent<Rigidbody>().AddForce(transform.right * forceAmount, ForceMode.Force);
        }

        // move to the right
        private void ApplyForceLeft()
        {
            Vector3 forceDirection = transform.position - goToObject.transform.position;

            if (forceDirection.magnitude > minRadius && forceDirection.magnitude < maxRadius)
                GetComponent<Rigidbody>().AddForce(-transform.right * forceAmount, ForceMode.Force);
        }
    }
}
