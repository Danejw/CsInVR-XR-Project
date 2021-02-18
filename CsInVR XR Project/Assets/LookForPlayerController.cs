using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;


namespace CSInVR.Tutorial
{
    [RequireComponent(typeof(RotateWithHMD))]

    public class LookForPlayerController : MonoBehaviour
    {

        BNGPlayerController playerControl;


        private void Start()
        {
            playerControl = FindObjectOfType<BNGPlayerController>();

            SetPlayerControl();
        }

        private void OnEnable()
        {
            playerControl = FindObjectOfType<BNGPlayerController>();

            SetPlayerControl();
        }


        // Update is called once per frame
        void Update()
        {
            if (playerControl != GetComponent<RotateWithHMD>().Character)
            {
                if (playerControl)
                {
                    // set the player chracter
                    GetComponent<RotateWithHMD>().Character = playerControl.GetComponent<CharacterController>();
                }
                else
                {
                    //find the player controller
                    playerControl = FindObjectOfType<BNGPlayerController>();
                }
            }
        }

        private void SetPlayerControl()
        {
            if (playerControl)
            {
                // set the player chracter
                GetComponent<RotateWithHMD>().Character = playerControl.GetComponent<CharacterController>();
            }
        }
    }
}
