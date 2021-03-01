using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    [RequireComponent(typeof(Collider))]

    public class Agent_CenterAttacker : Agent
    {
        [SerializeField] private Transform caughtPosition;

        public delegate void OnBlock(GameObject blocker);
        public static event OnBlock onBlock;


        private bool hasBlocked;


        private void OnEnable()
        {
            hasBlocked = false;
        }

        private void BlockBall(GameObject blocker)
        {
            onBlock?.Invoke(blocker);
            hasBlocked = true;

            if (debug) Debug.Log("The ball has been caught");
        }

        private void MoveBallToCaughtPosition(Ball ballObject, Transform moveTo)
        {
            Rigidbody ballRig = ballObject.GetComponent<Rigidbody>();

            // move the ball into the caught position
            ballObject.transform.position = moveTo.position;

            // Set the ball rigidbody state, to keep the ball with the reciever
            ballRig.isKinematic = true;
            ballRig.velocity = new Vector3(0, 0, 0);
            ballObject.transform.SetParent(this.transform);

            if (debug) Debug.Log("The ball has been moved");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Ball")
            {
                Ball ball = other.gameObject.GetComponent<Ball>();

                if (ball && ball.GetBallIsActive())
                {
                    if (!hasBlocked)
                    {
                        if (caughtPosition) MoveBallToCaughtPosition(ball, caughtPosition);
                        BlockBall(this.gameObject);
                    }
                }
            }
            else
                hasBlocked = false;
        }


    }
}
