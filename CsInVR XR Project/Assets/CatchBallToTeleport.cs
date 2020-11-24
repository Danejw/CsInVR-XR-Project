using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

namespace CSInVR
{
    // Teleport the player to the location to where the ball was caught

        [RequireComponent(typeof(Collider))]


    public class CatchBallToTeleport : PlayerTeleport
    {

        [SerializeField]
        public PlayerTeleport player;

        // Start is called before the first frame update
        void Start()
        {
            if (player)
                player = GetComponent<PlayerTeleport>();
        }

        // Update is called once per frame
        void Update()
        {

        
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Ball")
            {
                if (other.GetComponent<Ball>() && other.GetComponent<Ball>().isActive)
                {
                    // teleport the player
                    StartCoroutine( player.doTeleport(transform.position, transform.rotation, false) );
                }


            }
            else
            {
                // send a message to let the game know that the ball was missed
            }
        }
    }
}
