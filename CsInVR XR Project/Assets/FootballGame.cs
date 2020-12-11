using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private bool isTouchdown;
        private GameObject catchingReciever;
        private GameObject blockingBlocker;
        private bool isGameOver;

        [SerializeField] private float distTillFirstdown;
        [SerializeField] private Vector3 firstDownIncrement = new Vector3(0, 0, 10);
        [SerializeField] private Vector3 goalIncrement = new Vector3(0, 0, 50);

        public GameObject[] recievers;
        public GameObject player;
        public FirstdownMarker firstdownMark;
        public GameObject firstdownMarkChains;
        public GoalMarker goalMarker;
        public Goal goal;

        //events
        public delegate void OnFirstDown();
        public static event OnFirstDown onFirstdown;

        public delegate void OnReadyToStart();
        public static event OnReadyToStart onReadyToStart;

        public delegate void OnGameOver();
        public static event OnGameOver onGameOver;


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
            //Goal.onGoal += TouchdownEvent;
            Goal.onFirstDown += FirstdownEvent;
            Blocker.onBlock += BlockEvent;
        }

        private void OnDisable()
        {
            HikeBall.onMissedCatch -= MissedCatchEvent;
            Reciever.onCatch -= CatchEvent;
            //Goal.onGoal -= TouchdownEvent;
            Goal.onFirstDown -= FirstdownEvent;
            Blocker.onBlock -= BlockEvent;
        }

        // Game Loop
        private void Start()
        {
            InitGame(startingPosition);
        }


        private void Update()
        {
            distTillFirstdown = firstdownMark.transform.position.z - hikePosition.z;

            if (currentDown <= maxDown || isTouchdown)
            {
                if (ball.GetBallIsActive() && ball.hasHiked)
                {
                    print("The ball has been hiked and is active");
                }

                // set firstdown marker on or off
                if (calcDistanceYardage(goal.transform.position, hikePosition) < calcDistanceYardage(firstdownMark.transform.position, hikePosition))
                    if (firstdownMarkChains.activeSelf) firstdownMarkChains.SetActive(false);
                if (calcDistanceYardage(goal.transform.position, hikePosition) > calcDistanceYardage(firstdownMark.transform.position, hikePosition))
                    if (!firstdownMarkChains.activeSelf) firstdownMarkChains.SetActive(true);

                // Touchdown
                if (!isTouchdown)
                {
                    if (goalMarker.transform.position.z - hikePosition.z <= 0)
                    {
                        TouchdownEvent();
                    }
                }
            }
            else
                GameOver();


        }

        // Functions
        private void InitGame(Vector3 position)
        {
            if (debug) Debug.Log("Initializing game...");

            setPositions(position);
            setGoal(position);
            setFirstDown(position);
            ResetBall();

            isTouchdown = false;
            isGameOver = false;
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

        private void setGoal(Vector3 position)
        {
            if (debug) Debug.Log("Setting the goal mark");

            // moves new firstdown to 'x' units plus caught position
            goalMarker.markerMoveTo = position + goalIncrement;
            goalMarker.isMovingGoalMarker = true;
        }

        private void setFirstDown(Vector3 position)
        {
            if (debug) Debug.Log("Setting the first down mark");

            // sets the current down to zero
            currentDown = 1;
            // moves new firstdown to 'x' units plus caught position
            firstdownMark.markerMoveTo = position + firstDownIncrement;
            firstdownMark.isMovingFirstDownMarker = true;
            // send off firstdown event
            onFirstdown?.Invoke();
        }

        private void GameOver()
        {
            StartCoroutine(EndGame());

            if (debug) Debug.Log("The Game is Over!");

            isGameOver = true;

            onGameOver?.Invoke();

            // happens when the firstdown or goal is not met
            // sends off an event to show UI to either restart game or to go back to the main menu
        }

        IEnumerator EndGame()
        {
            yield return new WaitForSeconds(10);
            SceneManager.LoadScene(0);
        }

        public void NextPlay()
        {
            if (debug) Debug.Log("Getting ready to play the next play");

            if (firstdownMark.transform.position.z <= 0)
            {
                if (debug) Debug.Log("FirstDown, reseting to new firstdown position " + (firstdownMark.transform.position.z - yardage));

                setFirstDown(hikePosition);
                onReadyToStart?.Invoke();
                //setPositions(new Vector3(0, 0, caughtPosition.z));
                ResetBall();
            }
            else
            {
                if (debug) Debug.Log("Firstdown position " + (firstdownMark.transform.position.z - yardage));

                onReadyToStart?.Invoke();
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
        private void TouchdownEvent()
        {
            if (debug) Debug.Log("Touchdown!");

            isTouchdown = true;

            goal.MadeGoal();

            StartCoroutine(DelayedInitGame(10));

            // show UI to restart the game or to go to the main menu
        }

        private void FirstdownEvent()
        {
            if (debug) Debug.Log("Firstdown!");

            setFirstDown(hikePosition);
        }

        private void MissedCatchEvent()
        {
            currentDown += 1;
        }

        private void CatchEvent(GameObject reciever)
        {
            catchingReciever = reciever;
            caughtPosition = reciever.transform.position;

            if (debug) Debug.Log("The ball was caught by " + catchingReciever.name);

            // calculate the yardage
            yardage = calcDistanceYardage(caughtPosition, hikePosition);

            if (calcDistanceYardage(firstdownMark.transform.position, hikePosition) > yardage)
            {
                // move the firstdown markers
                firstdownMark.markerMoveTo = new Vector3(firstdownMark.transform.position.x, firstdownMark.transform.position.y, firstdownMark.transform.position.z - yardage);
                firstdownMark.isMovingFirstDownMarker = true;

                currentDown++;

                // move goal markers
                goalMarker.markerMoveTo = new Vector3(goalMarker.transform.position.x, goalMarker.transform.position.y, goalMarker.transform.position.z - yardage);
                goalMarker.isMovingGoalMarker = true;
            }
            else
                FirstdownEvent();


            if (debug) Debug.Log(firstdownMark.transform.position.z - yardage + " yards until the next firstdown");
        }

        private void BlockEvent(GameObject blocker)
        {
            if (debug) Debug.Log("The ball was blocked by " + blocker.name);

            currentDown += 1;
        }


        //enumerators
        private IEnumerator DelayedInitGame(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            InitGame(startingPosition);
        }


        //uitls
        public float calcDistanceYardage(Vector3 position1, Vector3 position2)
        {
            if (debug) Debug.Log("The yardage is " + position1 + " from " + position2);

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
