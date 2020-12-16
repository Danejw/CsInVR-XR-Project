using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR.Football
{
    // place the marker move to around the scene an toggle isMovingFirstdownMarker to true, to move the firstdown marker
    public class FirstdownMarker : MonoBehaviour
    {
        public bool debug;

        public bool isMovingFirstDownMarker = false;
        public float firstdownMarkSpeed = 3;
        public Vector3 markerMoveTo;

        [SerializeField] private float precision = .01f;


        // Update is called once per frame
        void Update()
        {
            if (isMovingFirstDownMarker)
                moveFirstDownMarkers(markerMoveTo);
        }



        // Moves the firstdown marker in the z direction
        private void moveFirstDownMarkers(Vector3 position)
        {
            Vector3 moveTo = new Vector3(transform.position.x, transform.position.y, position.z);

            if (Vector3.Distance(transform.position, markerMoveTo) < precision)
            {
                isMovingFirstDownMarker = false;
            }
            else
                transform.position = Vector3.MoveTowards(transform.position, moveTo, Time.deltaTime * firstdownMarkSpeed);

            if (debug) Debug.Log("Moving along " + name + "'s route to " + moveTo);
        }
    }
}
