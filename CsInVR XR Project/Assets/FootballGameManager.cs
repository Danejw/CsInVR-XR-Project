using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR.Football
{
    [RequireComponent(typeof(AudioSource))]

    public class FootballGameManager : MonoBehaviour
    {
        public bool debug;

        // message to signal that the game is ready to start
        public delegate void ReadyToStart();
        public static event ReadyToStart readyToStart;

        // player
        [SerializeField] private GameObject player;
        // ball
        [SerializeField] private HikeBall ball;
        // recievers array
        [SerializeField] private Reciever[] recievers;



        // text list


        private void Start()
        {

        }


        // reset
        public void ResetPositions()
        {
            player.transform.position = new Vector3(0, 0, 0);

            ball.transform.position = ball.startingPosition;
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            ball.GetComponent<Rigidbody>().isKinematic = false;
            ball.transform.parent = null;

            foreach (Reciever rec in recievers)
            {
                rec.anim.SetBool("Run", false);
                rec.transform.position = rec.startingPosition;
            }

            if (debug) Debug.Log("Reseting Positions");

        }

        // initialize
        public void InitializeGame()
        {
            // init ball
            ball.SetBallIsActive(true);
            ball.SetIsHiked(false);
            ball.hasHiked = false;

            if (debug) Debug.Log("Initializing Ball");


            // init player

            if (debug) Debug.Log("Initializing Player");



            // init recievers
            foreach (Reciever rec in recievers)
            {
                rec.hasCaught = false;

                if (debug) Debug.Log("Initializing Reciever: " + rec.name);
            }

            if (debug) Debug.Log("Initialized Game");

        }


        public void StartGame()
        {
            ResetPositions();
            InitializeGame();

            readyToStart?.Invoke();
        }

        // end game


        // Subscribe to Events
        private void OnEnable()
        {
            HikeBall.onHike += HikeEvent;
            Reciever.onCatch += CatchEvent;
            Goal.onGoal += GoalEvent;
            HikeBall.onMissedCatch += MissedCatchEvent;
        }

        // OnHike Event
        private void HikeEvent()
        {
            
        }

        // OnCatch Event
        private void CatchEvent()
        {

        }

        // OnGoal Event
        private void GoalEvent()
        {

        }

        // OnMissedCatch Event
        private void MissedCatchEvent()
        {

        }

        // Un-Subscribe to Events
        private void OnDisable()
        {
            HikeBall.onHike -= HikeEvent;
            Reciever.onCatch -= CatchEvent;
            Goal.onGoal -= GoalEvent;
            HikeBall.onMissedCatch -= MissedCatchEvent;
        }


        // Sounds
        // Referee

        // particles

    }
}
