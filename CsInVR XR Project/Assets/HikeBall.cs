using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

namespace CSInVR.Football
{
    [RequireComponent(typeof(Grabbable))]

    public class HikeBall : Ball
    {
        public bool debug;

        [SerializeField] private bool spiral = true;
        public float minSpiralVelocity = 5;
        [SerializeField] private float spiralAngleAmt = 180;
        [SerializeField] private float spiralSpinSpeed = 10;

        private bool isHiked;
        public bool hasHiked;

        // Send message that ball is hiked
        public delegate void OnHike();
        public static event OnHike onHike;

        // send messae that the ball has hit the ground
        public delegate void OnMissedCatch();
        public static event OnMissedCatch onMissedCatch;

        private Grabbable grabbable;
        protected Rigidbody rig;

        protected bool isCaught;

        [SerializeField] private FootballGame footballGame; 
        private Vector3 startingPosition;

        protected bool isGrabbed;



        protected virtual void Start()
        {
            grabbable = GetComponent<Grabbable>();
            if (footballGame) footballGame = footballGame.GetComponent<FootballGame>(); else Debug.Log("The FootballGame is Not assigned");

            if (!rig) rig = GetComponent<Rigidbody>();
        }

        protected virtual void OnEnable()
        {
            grabbable = GetComponent<Grabbable>();

            if (!rig) rig = GetComponent<Rigidbody>();

            if (GetBallIsActive()) SetBallIsActive(false);

            //startingPosition = footballGame.startingPosition;

            Reciever.onCatch += SetBallInActive;
            Agent_CenterAttacker.onBlock += SetBallInActive;
        }

        protected virtual void OnDisable()
        {
            if (GetBallIsActive()) SetBallIsActive(false);

            Reciever.onCatch -= SetBallInActive;
            Agent_CenterAttacker.onBlock -= SetBallInActive;
        }

        protected virtual void Update()
        {
            if (GetBallIsActive() && hasHiked)
            {
                isCaught = false;
                onHike?.Invoke();
                hasHiked = false;

                if (debug) Debug.Log("The ball has been hiked");
            }


            if (grabbable)
            {
                if (grabbable.BeingHeld == true)
                {    
                    if (!isGrabbed)
                    {
                        hasHiked = true;
                        isGrabbed = true;

                        if (!GetBallIsActive()) SetBallIsActive(true);

                        if (debug) Debug.Log("The Ball is being held");
                    }
                }
                else if (isGrabbed)
                {
                    isGrabbed = false;
                    if (debug) Debug.Log("The Ball was let go");
                }
            }

            // if ball is moving faster than a minimal velocity
            if (spiral && rig.velocity.magnitude > minSpiralVelocity)
                Spiral();


        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Ground" && GetBallIsActive())
            {
                SetBallIsActive(false);

                onMissedCatch?.Invoke();

                if (debug) Debug.Log("The Ball has hit the ground");
            }
        }



        public void SetIsHiked(bool value)
        {
            isHiked = value;
        }

        public bool GetIsCaught()
        {
            return isCaught;
        }

        public void SetBallInActive(GameObject obj)
        {
            SetBallIsActive(false);
        }

        public void ResetBall(Vector3 position)
        {
            transform.position = position;
            rig.velocity = new Vector3(0, 0, 0);
            rig.isKinematic = false;
            transform.parent = null;
            SetBallIsActive(false);
        }

        private void Spiral()
        {
            if (GetBallIsActive())
            {
                if (debug) Debug.Log("The ball is spiraling at " + rig.velocity.magnitude);

                // face in the direction of velocity
                transform.LookAt(transform.position + rig.velocity);
                // rotate object
            }
        }
    }

    
}
