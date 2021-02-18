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
                eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                eventInstance.start();
            }
            else
                Debug.LogError("The event path is Not assigned");

            //Tutorial.FootballTutorialEvents.onTutorialStart += PlayTutorialMusicScene;
            //FootballGame.onGameStart += PlayFootballMusicScene;
        }

        private void OnDisable()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            //Tutorial.FootballTutorialEvents.onTutorialStart -= PlayTutorialMusicScene;
            //FootballGame.onGameStart -= PlayFootballMusicScene;
        }


        public void PlayTutorialMusicScene()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            RuntimeManager.StudioSystem.setParameterByName("MusicScenes", 1);
            eventInstance.start();

            if (debug) Debug.Log("New Tutorial Music Scene is playing");
        }

        public void PlayFootballMusicScene()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            RuntimeManager.StudioSystem.setParameterByName("MusicScenes", 0);
            eventInstance.start();

            if (debug) Debug.Log("New Football Music Scene is playing");
        }

    }
}
