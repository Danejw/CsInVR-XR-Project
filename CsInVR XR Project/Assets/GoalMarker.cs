using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    public class GoalMarker : MonoBehaviour
    {
        public bool debug;

        public bool isMovingGoalMarker = false;
        public float goalMarkSpeed = 6;
        public Vector3 markerMoveTo;

        [SerializeField] private float precision = .01f;


        // Update is called once per frame
        void Update()
        {
            if (isMovingGoalMarker)
                moveGoalMarkers(markerMoveTo);
        }



        // Moves the firstdown marker in the z direction
        private void moveGoalMarkers(Vector3 position)
        {
            Vector3 moveTo = new Vector3(transform.position.x, transform.position.y, position.z);

            if (Vector3.Distance(transform.position, markerMoveTo) < precision)
            {
                isMovingGoalMarker = false;
            }
            else
                transform.position = Vector3.MoveTowards(transform.position, moveTo, Time.deltaTime * goalMarkSpeed);

            if (debug) Debug.Log("Moving along " + name + "'s route to " + moveTo);
        }
    }
}
