using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

namespace CSInVR.Football.Fmod
{
    public class FmodCollisionSound : MonoBehaviour
    {
        public bool debug;

        [SerializeField][EventRef] private string eventPath;


        private void OnCollisionEnter(Collision collision)
        {
            if (eventPath != null)
                RuntimeManager.PlayOneShotAttached(eventPath, this.gameObject);
            else
                if (debug) Debug.Log("The eventPath is NOT assigned to the collision sound effect");
        }
    }
}
