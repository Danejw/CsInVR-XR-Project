using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using Cinemachine;


namespace CSInVR
{
    public class GrabCamera : MonoBehaviour
    {
        public bool debug;

        private Grabbable grabbable;
        private CinemachineVirtualCamera cam;




        private void OnEnable()
        {
            grabbable = GetComponent<Grabbable>();
            cam = this.GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            if (grabbable.BeingHeld && cam.enabled == true)
            {
                // turn off the camera follower settings
                cam.enabled = false;

                if (debug) Debug.Log("The camera is being held, so the camera is now off");
            }
            else if (!grabbable.BeingHeld && cam.enabled == false)
            {
                // turn on the camera follower settings
                cam.enabled = true;

                if (debug) Debug.Log("The camera is not being held, so the camera is now on");
            }
        }

    }
}
