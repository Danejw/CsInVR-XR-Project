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

        private bool isHiked;
        public bool hasHiked;

        // Send message that ball is hiked
        public delegate void OnHike();
        public static event OnHike onHike;

        // send messae that the ball has hit the ground
        public delegate void OnMissedCatch();
        public static event OnMissedCatch onMissedCatch;

        private Grabbable grabbable;

        private bool isCaught;

        [SerializeField] private FootballGame footballGame; 
        private Vector3 startingPosition;

        [SerializeField] private bool isGrabbed;


        private void Start()
        {
            grabbable = GetComponent<Grabbable>();
            if (footballGame) footballGame = footballGame.GetComponent<FootballGame>(); else Debug.Log("The FootballGame is Not assigned");
        }

        void OnEnable()
        {
            grabbable = GetComponent<Grabbable>();

            if (!GetBallIsActive()) SetBallIsActive(true);

            startingPosition = footballGame.startingPosition;
        }

        private void OnDisable()
        {
            if (GetBallIsActive()) SetBallIsActive(false);
        }


        // Update is called once per frame
        void Update()
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

                        if (debug) Debug.Log("The Ball is being held");
                    }
                }
                else if (isGrabbed)
                {
                    isGrabbed = false;
                    if (debug) Debug.Log("The Ball was let go");
                }
            }

        }

        private void OnCollisionEnter(Collision collision)
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

        public void ResetBall(Vector3 position)
        {
            transform.position = position;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().isKinematic = false;
            transform.parent = null;
            SetBallIsActive(true);
        }
    }

    
}
