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

        private float yardage;
        [SerializeField] private bool isMovingFirstDownMarker = false;
        [SerializeField] private float firstdownMarkSpeed = 1;
        private Vector3 markerMoveTo;

        private bool isTouchdown;
        private GameObject catchingReciever;

        [SerializeField] private float distTillFirstdown;
        [SerializeField] private Vector3 firstDownIncrement = new Vector3(0, 0, 10);

        public GameObject[] recievers;
        public GameObject player;
        public GameObject firstdownMark;

        //events
        public delegate void OnFirstDown();
        public static event OnFirstDown onFirstdown;


        private void Awake()
        {
            if (debug && startingPosition == null) Debug.Log("The starting position was NOT assigned");
            if (debug && !ball) Debug.Log("The ball was NOT assigned");
            if (debug && !player) Debug.Log("The player was NOT assigned");
            if (debug && !firstdownMark) Debug.Log("The firstdown Mark was NOT assigned");

        }

        private void OnEnable()
        {
            HikeBall.onMissedCatch += MissedCatchEvent;
            Reciever.onCatch += CatchEvent;
            Goal.onGoal += Touchdown;
        }

        private void OnDisable()
        {
            HikeBall.onMissedCatch -= MissedCatchEvent;
            Reciever.onCatch -= CatchEvent;
            Goal.onGoal -= Touchdown;
        }

        // Game Loop
        private void Start()
        {
            InitGame(startingPosition);
        }


        private void Update()
        {
            distTillFirstdown = firstdownMark.transform.position.z - hikePosition.z;

            if (isMovingFirstDownMarker)
                moveFirstDownMarkers(markerMoveTo);



            if (currentDown <= maxDown || isTouchdown)
            {
                if (ball.GetBallIsActive() && ball.hasHiked)
                {
                    print("The ball has been hiked and is active");
                }                 
            }
            
            
        }

        // Functions
        private void InitGame(Vector3 position)
        {
            if (debug) Debug.Log("Initializing game...");

            setPositions(position);
            setFirstDown(position);
            ResetBall();

            isTouchdown = false;
            currentDown = 1;
        }

        private void setPositions(Vector3 position)
        {
            if (debug) Debug.Log("Setting positions");

            // sets the balls position
            ball.transform.position = position + new Vector3(0, 0.2f, 0);
            hikePosition = position + new Vector3(0, 0.2f, 0);

            // sets the recievers positions
            if (recievers != null)
                foreach (GameObject rec in recievers)
                    rec.transform.position = position + new Vector3(Random.Range(0.2f, 2), 0, 0);

            // sets the player pocket position
            if (player) player.transform.position = position + new Vector3(0, 0, -3);
        }

        private void setFirstDown(Vector3 position)
        {
            if (debug) Debug.Log("Setting the first down mark");

            // sets the current down to zero
            currentDown = 1;
            // moves new firstdown to 'x' units plus caught position
            markerMoveTo = position + firstDownIncrement;
            isMovingFirstDownMarker = true;
            // send off firstdown event
            onFirstdown?.Invoke();
        }

        // Moves the firstdown marker in the z direction
        private void moveFirstDownMarkers(Vector3 position)
        {
            Vector3 moveTo = new Vector3(firstdownMark.transform.position.x, firstdownMark.transform.position.y, position.z);

            if (distTillFirstdown < 0.1f)
            {
                isMovingFirstDownMarker = false;
            }
            else
                firstdownMark.transform.position = Vector3.MoveTowards(firstdownMark.transform.position, moveTo, Time.deltaTime * firstdownMarkSpeed);

            if (debug) Debug.Log("Moving along " + firstdownMark.name + "'s route to " + moveTo);
        }

        private void GameOver()
        {
            if (debug) Debug.Log("The Game is Over!");

            // happens when the firstdown or goal is not met
            // sends off an event to show UI to either restart game or to go back to the main menu
        }

        public void NextPlay()
        {
            if (debug) Debug.Log("Getting ready to play the next play");
         
            // re-initializes the game into thw starting position
            if (distTillFirstdown < 0)
            {
                setFirstDown(caughtPosition);
                //setPositions(new Vector3(0, 0, caughtPosition.z));
                ResetBall();
            }
            else
            {
                //setPositions(caughtPosition);
                ResetBall();
            }           
        }




        private void ResetBall()
        {
            if (debug) Debug.Log("Reseting the ball");

            // reset the ball     
            ball.ResetBall(hikePosition);
        }


        // Events
        private void Touchdown()
        {
            if (debug) Debug.Log("Touchdown!");

            isTouchdown = true;

            // show UI to restart the game or to go to the main menu
        }

        private void MissedCatchEvent()
        {
            currentDown += 1;
        }

        private void CatchEvent(GameObject reciever)
        {
            catchingReciever = reciever;
            caughtPosition = reciever.transform.position;

            // calculate the yardage
            yardage = calcPlayYardage(caughtPosition, hikePosition);
            markerMoveTo = new Vector3(firstdownMark.transform.position.x, firstdownMark.transform.position.y, caughtPosition.z -yardage);
            // move the firstdown markers
            isMovingFirstDownMarker = true;

            if (debug) Debug.Log(distTillFirstdown + " yards until the next firstdown");

            currentDown += 1;
        }





        //uitls
        public float calcPlayYardage(Vector3 position1, Vector3 position2)
        {
            if (debug) Debug.Log("The catch was at " + position1 + " from " + position2);

            float distance = Mathf.Round( Mathf.Sqrt( Mathf.Pow(position1.z, 2) - Mathf.Pow(position2.z, 2) ) );
                  
            if (debug) Debug.Log("The distance on the play was " + distance);

            return distance;
        }

        public float GetDistanceTillFirstdown()
        {
            return distTillFirstdown;
        }
    }
}
