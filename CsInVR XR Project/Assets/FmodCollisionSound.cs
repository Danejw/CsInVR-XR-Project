using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

namespace CSInVR.FMOD
{
    public class FmodCollisionSound : MonoBehaviour
    {
        [SerializeField] private string eventPath;


        private void OnCollisionEnter(Collision collision)
        {
            RuntimeManager.PlayOneShotAttached(eventPath, this.gameObject);
        }
    }
}
