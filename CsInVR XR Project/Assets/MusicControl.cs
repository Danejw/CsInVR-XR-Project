﻿using System.Collections;
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
                RuntimeManager.StudioSystem.setParameterByName("MusicScenes", 0);
                eventInstance.start();
            }
            else
                Debug.LogError("The event path is Not assigned");

            Tutorial.FootballTutorialEvents.onTutorialStart += PlayTutorialMusicScene;
        }

        private void OnDisable()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            Tutorial.FootballTutorialEvents.onTutorialStart -= PlayTutorialMusicScene;

        }


        public void PlayTutorialMusicScene()
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            RuntimeManager.StudioSystem.setParameterByName("MusicScenes", 1);
            eventInstance.start();

            if (debug) Debug.Log("New Music Scene is playing");
        }
    }
}
