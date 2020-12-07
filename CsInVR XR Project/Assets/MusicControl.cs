using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;


namespace CSInVR.Football.Fmod
{
    [RequireComponent(typeof(Rigidbody))]

    public class MusicControl : MonoBehaviour
    {
        public bool debug;
        public bool test;

        [SerializeField] [EventRef] private string eventPath;
        private EventInstance eventInstance;


        private void OnEnable()
        {
            if (eventPath != null)
            {
                eventInstance = RuntimeManager.CreateInstance(eventPath);
                RuntimeManager.AttachInstanceToGameObject(eventInstance, transform, GetComponent<Rigidbody>());
                eventInstance.start();
            }
            else
                Debug.LogError("The event path is Not assigned");
        }

        private void OnDisable()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
