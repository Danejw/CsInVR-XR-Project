using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR.Football
{
    public class FootballGame : MonoBehaviour
    {
        public bool debug;

        public int currentDown = 1;
        public int maxDown = 4;

        public HikeBall ball;

        private Vector3 caughtPosition;
        private Vector3 hikePosition;
        public Vector3 startingPosition;

        private bool isTouchdown;

        private float distTillFirstdown;

        public GameObject reciever;
        public GameObject player;
        public GameObject firstdownMark;

        //events
        public delegate void OnFirstDown();
        public static event OnFirstDown onFirstdown;

        public delegate void OnTouchdown();
        public static event OnTouchdown onTouchdown;

        private void Awake()
        {
            if (debug && startingPosition == null) Debug.Log("The starting position was NOT assigned");
            if (debug && !ball) Debug.Log("The ball was NOT assigned");
            if (debug && !player) Debug.Log("The player was NOT assigned");
            if (debug && !firstdownMark) Debug.Log("The firstdown Mark was NOT assigned");

        }

        // Game Loop
        private void Start()
        {
            InitGame(startingPosition);
        }


        private void Update()
        {        
            if (currentDown <= maxDown || isTouchdown)
            {
                if (ball.hasHiked)
                {
                    if (!ball.GetBallIsActive())
                    {
                        ResetBall(hikePosition);
                    }

                    else if (ball.GetIsCaught()) // if the ball is caught
                    {
                        caughtPosition = reciever.transform.position;

                        distTillFirstdown = calcPlayYardage(caughtPosition, hikePosition);

                        setPositions(caughtPosition);

                        currentDown += 1;

                        if (distTillFirstdown < 0) // if the firstdown is reached
                        {
                            setFirstDown(caughtPosition);
                        }

                        if (ball.GetIsCaught() && isTouchdown) // if the ball is caught in the touchdown
                        {
                            Touchdown();
                        }
                    }

                    else
                        currentDown += 1;
                }
            }
            
        }





        // Functions
        private void InitGame(Vector3 position)
        {
            if (debug) Debug.Log("Initializing game...");

            setPositions(position);
            setFirstDown(position);
            ResetBall(position);

            isTouchdown = false;
            currentDown = 1;
        }

        private void setPositions(Vector3 position)
        {
            if (debug) Debug.Log("Setting positions");

            // sets the balls position
            ball.transform.position = position + new Vector3(0, 0.5f, 0);
            // sets the recievers positions
            if (reciever) reciever.transform.position = position + new Vector3(Random.Range(0.2f,2), 0, 0);
            // sets the player pocket position
            if (player) player.transform.position = position + new Vector3(0, 0, -3);
        }

        private void GameOver()
        {
            if (debug) Debug.Log("The Game is Over!");

            // happens when the firstdown or goal is not met
            // sends off an event to show UI to either restart game or to go back to the main menu
        }

        public void Restart()
        {
            if (debug) Debug.Log("Restarting");

            // re-initializes the game into thw starting position

            InitGame(startingPosition);
        }

        private void setFirstDown(Vector3 position)
        {
            if (debug) Debug.Log("Setting the first down mark");

            // sets the current down to zero
            currentDown = 0;
            // sets new firstdown to 'x' units plus caught position
            if (ball.GetIsCaught())
            {
                firstdownMark.transform.position = position + new Vector3(0, 0, 10);
            }
            // send off firstdown event
            onFirstdown?.Invoke();
            // reset the distance till the next firstdown
            distTillFirstdown = 10;
        }

        private void Touchdown()
        {
            if (debug) Debug.Log("Touchdown!");

            // send off touchdown event
            onTouchdown?.Invoke();
            isTouchdown = true;

            // show UI to restart the game or to go to the main menu
        }

        private void ResetBall(Vector3 position)
        {
            if (debug) Debug.Log("Reseting the ball");

            // reset the ball
            //ball.transform.parent = null;
            //ball.transform.position = position;
            ball.hasHiked = false;
        }

        public float calcPlayYardage(Vector3 position1, Vector3 position2)
        {
            if (debug) Debug.Log("Calculating the yardage");

            return position1.z - position2.z;
        }

    }
}
