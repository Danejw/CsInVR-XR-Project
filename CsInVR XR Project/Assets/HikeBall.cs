using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;


namespace CSInVR.Football
{
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
        public Vector3 startingPosition;


        private void Start()
        {
            grabbable = GetComponent<Grabbable>();
        }

        void OnEnable()
        {
            grabbable = GetComponent<Grabbable>();
            if (!GetBallIsActive()) SetBallIsActive(true);

            startingPosition = this.transform.position;
        }

        private void OnDisable()
        {
            if (GetBallIsActive()) SetBallIsActive(false);
        }


        // Update is called once per frame
        void Update()
        {
            if (!isHiked)
            {
                if (GetBallIsActive() && hasHiked)
                {
                    onHike?.Invoke();
                    hasHiked = true;

                    isCaught = false;

                    if (debug) Debug.Log("The ball has been hiked");

                    isHiked = true;
                }
            }


            if (grabbable)
            {
                if (grabbable.BeingHeld == true)
                {
                    hasHiked = true;
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
    }
}
