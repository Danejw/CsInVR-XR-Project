using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider))]

    public class Reciever : MonoBehaviour
    {
        public bool debug;

        [SerializeField] private Transform caughtPosition;

        public delegate void OnCatch();
        public static event OnCatch onCatch;

        public bool hasCaught;

        public Vector3 startingPosition;

        [HideInInspector] public Animator anim;


        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            HikeBall.onHike += Run;
            hasCaught = false;

            startingPosition = this.transform.position;
        }

        private void OnDisable()
        {
            HikeBall.onHike -= Run;
        }


        public void Run()
        {
            anim.SetBool("Run", true);

            if (debug) Debug.Log("The Reciever started to run");        
        }

        private void CatchBall()
        {
            onCatch?.Invoke();
            hasCaught = true;

            if (debug) Debug.Log("The ball has been caught");
        }

        private void MoveBallToCaughtPosition(Ball ballObject, Transform moveTo)
        {
            Rigidbody ballRig = ballObject.GetComponent<Rigidbody>();

            // move the ball into the caught position
            ballObject.transform.position = moveTo.position;

            // Set the ball rigidbody state, to keep the ball with the reciever
            ballRig.isKinematic = true;
            ballRig.velocity = new Vector3(0,0,0);
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
                    if (!hasCaught)
                    {
                        if (caughtPosition) MoveBallToCaughtPosition(ball, caughtPosition);
                        CatchBall();
                    }
                }
            }
            else
                hasCaught = false;
        }
    }
}

