using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;


namespace CSInVR.Football.Fmod
{
    [RequireComponent(typeof(Rigidbody))]

    public class SoundEffectControl : MonoBehaviour
    {
        public bool debug;
        public bool test;

        [SerializeField] [EventRef] private string eventPath;
        private EventInstance eventInstance;


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

        // Subscribe to Events
        private void OnEnable()
        {
            //FootballGameManager.readyToStart += ReadyToStartSoundEvent;
            HikeBall.onHike += HikeSoundEvent;
            Reciever.onCatch += CatchSoundEvent;
            Goal.onGoal += GoalSoundEvent;
            HikeBall.onMissedCatch += MissedCatchSoundEvent;
        }

        // Un-Subscribe to Events
        private void OnDisable()
        {
            //FootballGameManager.readyToStart -= ReadyToStartSoundEvent;
            HikeBall.onHike -= HikeSoundEvent;
            Reciever.onCatch -= CatchSoundEvent;
            Goal.onGoal -= GoalSoundEvent;
            HikeBall.onMissedCatch -= MissedCatchSoundEvent;
        }

        private void ReadyToStartSoundEvent()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            RuntimeManager.StudioSystem.setParameterByName("SoundEffect", 0);
            eventInstance.start();

            if (debug) Debug.Log("Ready to start sound has played");
        }

        private void HikeSoundEvent()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            RuntimeManager.StudioSystem.setParameterByName("SoundEffect", 1);
            eventInstance.start();

            if (debug) Debug.Log("Hike sound has played");
        }

        private void CatchSoundEvent(GameObject reviever)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            RuntimeManager.StudioSystem.setParameterByName("SoundEffect", 2);
            eventInstance.start();

            if (debug) Debug.Log("Catch sound has played");
        }

        private void MissedCatchSoundEvent()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            RuntimeManager.StudioSystem.setParameterByName("SoundEffect", 3);
            eventInstance.start();

            if (debug) Debug.Log("Missed catch sound has played");
        }

        private void GoalSoundEvent()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            RuntimeManager.StudioSystem.setParameterByName("SoundEffect", 4);
            eventInstance.start();

            if (debug) Debug.Log("Goal sound has played");
        }

        // test functionality
        private void Update()
        {
            if (test)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0))
                {
                    ReadyToStartSoundEvent();
                }

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    HikeSoundEvent();
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    CatchSoundEvent(null);
                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    MissedCatchSoundEvent();
                }

                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    GoalSoundEvent();
                }
            }
        }
    }
}
