using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;


namespace CSInVR.Football
{
    public class BallVelocitySound : MonoBehaviour
    {
        public bool debug;

        [SerializeField] [EventRef] private string eventPath;

        private EventInstance eventInstance;

        private void OnEnable()
        {
            HikeBall.onHike += StartBallSound;
            HikeBall.onMissedCatch += StopBallSound;
        }

        private void OnDisable()
        {
            HikeBall.onHike -= StartBallSound;
            HikeBall.onMissedCatch -= StopBallSound;
        }

        private void Start()
        {
            if (eventPath != null)
            {
                eventInstance = RuntimeManager.CreateInstance(eventPath);
                RuntimeManager.AttachInstanceToGameObject(eventInstance, transform, GetComponent<Rigidbody>());
            }
            else
                Debug.LogError("The event path is Not assigned");
        }

        private void StartBallSound()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.start();

            if (debug) Debug.Log("Ball sound is playing");
        }

        private void StopBallSound()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            if (debug) Debug.Log("Ball sound stopped playing");
        }
    }
}
