using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;


namespace CSInVR.Football.Fmod
{
    [RequireComponent(typeof(Rigidbody))]

    public class CrowdExcitementControl : MonoBehaviour
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
            {
                Debug.LogError("The event path is Not assigned");
            }

            LoopCrowdNormal();
        }

        private void OnEnable()
        {
            HikeBall.onHike += CrowdChants;
            Reciever.onCatch += CrowdCheers;
            Goal.onGoal += CrowdTouchdown;
            HikeBall.onMissedCatch += CrowdMissedOpportunity;
        }

        private void OnDisable()
        {
            HikeBall.onHike -= CrowdChants;
            Reciever.onCatch -= CrowdCheers;
            Goal.onGoal -= CrowdTouchdown;
            HikeBall.onMissedCatch -= CrowdMissedOpportunity;
        }


        private void CrowdMissedOpportunity()
        {
            // missed opporitunity crowd sound effect
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            RuntimeManager.StudioSystem.setParameterByName("Crowd Excitement", 0);
            eventInstance.start();

            if (debug) Debug.Log("The crowd is dissappointed");
        }

        private void CrowdNormal()
        {
            // normal crowd sound effect
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            RuntimeManager.StudioSystem.setParameterByName("Crowd Excitement", 1);
            eventInstance.start();

            if (debug) Debug.Log("The crowd is normal");
        }

        private void CrowdCheers()
        {
            // cheering crowd sound effect
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            RuntimeManager.StudioSystem.setParameterByName("Crowd Excitement", 2);
            eventInstance.start();

            if (debug) Debug.Log("The crowd is cheering");
        }

        private void CrowdChants()
        {
            // chanting crowd sound effect
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            RuntimeManager.StudioSystem.setParameterByName("Crowd Excitement", 3);
            eventInstance.start();

            if (debug) Debug.Log("The crowd is chanting");
        }

        private void CrowdTouchdown()
        {
            // crowd touchdown sound effect
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            RuntimeManager.StudioSystem.setParameterByName("Crowd Excitement", 4);
            eventInstance.start();

            if (debug) Debug.Log("The crowd goes wild");
        }

        private void LoopCrowdNormal()
        {
            eventInstance.start();

            if (debug) Debug.Log("Starting the normal crowd loop");
        }

        // test functionality
        private void Update()
        {
            if (test)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0))
                {
                    CrowdMissedOpportunity();
                }

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    CrowdNormal();
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    CrowdCheers();
                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    CrowdChants();
                }

                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    CrowdTouchdown();
                }
            }
        }
    }
}
