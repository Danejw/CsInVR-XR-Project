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

        // sound list
        private AudioSource audioSource;

        [SerializeField] AudioClip catchClip;


        [SerializeField] private CrowdSoundController crowdControl;

        // text list


        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
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
            ball.isActive = true;
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
            // On Hike Sound Effect

            // crowd cheer change
            if (crowdControl) crowdControl.PlayCheeringCrowd();
        }

        // OnCatch Event
        private void CatchEvent()
        {
            // OnCatch Sound Effect
            if (catchClip && !audioSource.isPlaying)
            {
                audioSource.clip = catchClip;
                audioSource.Play();
            }

            // crowd cheer change
            if (crowdControl) crowdControl.PlayHypedCrowd();
        }

        // OnGoal Event
        private void GoalEvent()
        {
            // Goal Sound Effect

            // crowd cheer change
            if (crowdControl) crowdControl.PlayTouchdownCrowd();

        }

        // OnMissedCatch Event
        private void MissedCatchEvent()
        {
            // Missed Catch sound effect


            // Crowd cheer chane
            if (crowdControl) crowdControl.PlayMissedOpportunity();
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
        // Crowd
        // Referee

        // particles

    }
}
