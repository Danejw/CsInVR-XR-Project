using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

namespace CSInVR
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public bool debug;

        // player location and rotation
        [SerializeField] private Transform player;
        [SerializeField] private Vector3 playerPosition;
        [SerializeField] private Quaternion playerRotation;

        // screen fader reference instance, used to transition
        [SerializeField] public ScreenFader screenFader;



        private void OnEnable()
        {
            FindPlayer();
            FindScreenFader();
        }

        private void Update()
        {
            if (!player) FindPlayer();
            if (!screenFader) FindScreenFader();
        }

        private void FixedUpdate()
        {
            // getting an updated version of the player position and rotation
            if (player) playerPosition = player.GetComponent<Transform>().position;
            if (player) playerRotation = player.GetComponent<Transform>().rotation;
        }

        private void FindPlayer()
        {
            if (debug) Debug.Log("Attempting to find player");

            player = FindObjectOfType<BNGPlayerController>().transform;

            if (player) if (debug) Debug.Log("Found player " + player.name);
        }

        private void FindScreenFader()
        {
            if (debug) Debug.Log("Attempting to find the screen fader");

            screenFader = FindObjectOfType<ScreenFader>();

            if (screenFader) if (debug) Debug.Log("Found screen fader " + screenFader.name);
        }

        public Vector3 getPlayerPosition()
        {
            return playerPosition;
        }

        public Quaternion getPlayerRotation()
        {
            return playerRotation;
        }
    }
}
