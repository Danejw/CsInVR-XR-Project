using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

namespace CSInVR.Tutorial
{
    [RequireComponent(typeof(Rigidbody))]

    public class TutorialEntityAudio : MonoBehaviour
    {
        public bool debug;

        [SerializeField] [EventRef] private string eventPath;
        public EventInstance eventInstance;

        private void Start()
        {
            if (eventPath != null)
            {
                eventInstance = RuntimeManager.CreateInstance(eventPath);
                RuntimeManager.AttachInstanceToGameObject(eventInstance, transform, GetComponent<Rigidbody>());
            }
            else
            {
                Debug.LogError("The event path is Not assigned");
            }

            PlayStartAudio();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Ball")
                PlayHitSelfAudio();
        }

        private void OnCollisionEnter(Collision collision)
        {
            PlayHitSelfAudio();
        }

        public void PlayStartAudio()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.setParameterByName("Tutorial", 0);
            RuntimeManager.AttachInstanceToGameObject(eventInstance, transform, GetComponent<Rigidbody>());
            eventInstance.start();

            if (debug) Debug.Log("Playing Start audio");
        }

        public void PlayEndAudio()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.setParameterByName("Tutorial", 8);
            RuntimeManager.AttachInstanceToGameObject(eventInstance, transform, GetComponent<Rigidbody>());
            eventInstance.start();

            if (debug) Debug.Log("Playing End audio");
        }

        public void PlayGrabAudio()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.setParameterByName("Tutorial", 1);
            RuntimeManager.AttachInstanceToGameObject(eventInstance, transform, GetComponent<Rigidbody>());
            eventInstance.start();

            if (debug) Debug.Log("Playing Grab audio");
        }

        public void PlayHikeAudio()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.setParameterByName("Tutorial", 2);
            RuntimeManager.AttachInstanceToGameObject(eventInstance, transform, GetComponent<Rigidbody>());
            eventInstance.start();

            if (debug) Debug.Log("Playing Hike audio");
        }

        public void PlayThrownAudio()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.setParameterByName("Tutorial", 3);
            RuntimeManager.AttachInstanceToGameObject(eventInstance, transform, GetComponent<Rigidbody>());
            eventInstance.start();

            if (debug) Debug.Log("Playing Thrown audio");
        }

        public void PlaySpiralAudio()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.setParameterByName("Tutorial", 4);
            RuntimeManager.AttachInstanceToGameObject(eventInstance, transform, GetComponent<Rigidbody>());
            eventInstance.start();

            if (debug) Debug.Log("Playing Spiral audio");
        }

        public void PlayTargetAudio()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.setParameterByName("Tutorial", 5);
            RuntimeManager.AttachInstanceToGameObject(eventInstance, transform, GetComponent<Rigidbody>());
            eventInstance.start();

            if (debug) Debug.Log("Playing Target audio");
        }

        public void PlayMissedCatchAudio()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.setParameterByName("Tutorial", 6);
            RuntimeManager.AttachInstanceToGameObject(eventInstance, transform, GetComponent<Rigidbody>());
            eventInstance.start();

            if (debug) Debug.Log("Playing MissedCatch audio");
        }

        public void PlayHitSelfAudio()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.setParameterByName("Tutorial", 7);
            RuntimeManager.AttachInstanceToGameObject(eventInstance, transform, GetComponent<Rigidbody>());
            eventInstance.start();

            if (debug) Debug.Log("Playing Hit Self audio");
        }

    }
}
