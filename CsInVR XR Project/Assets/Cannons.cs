using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

namespace CSInVR.Football
{
    [RequireComponent(typeof(Rigidbody))]

    public class Cannons : MonoBehaviour
    {
        public bool debug;
        public bool test;

        [SerializeField] [EventRef] private string eventPath;
        private EventInstance eventInstance;

        public ParticleSystem[] particles;


        // subscribe these functions to events
        private void OnEnable()
        {
            Target.onTargetHit += PlayCannonParticleEffect;
            Target.onTargetHit += PlayCannonSoundEffect;
        }

        private void OnDisable()
        {
            Target.onTargetHit -= PlayCannonParticleEffect;
            Target.onTargetHit -= PlayCannonSoundEffect;
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

        // test
        private void Update()
        {
            if (test)
            {
                PlayCannonSoundEffect();
                PlayCannonParticleEffect();
                test = false;
            }
        }

        private void PlayCannonSoundEffect()
            {
                // Referee Whistle
                eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                RuntimeManager.StudioSystem.setParameterByName("SoundEffect", 0);
                eventInstance.start();

                if (debug) Debug.Log("Cannon sound has played");
            }

        private void PlayCannonParticleEffect()
            {
                if (particles != null)
                    foreach (ParticleSystem particle in particles)
                    {
                        if (!particle.isPlaying) particle.Play();

                        ParticleSystem[] subParticles = GetComponentsInChildren<ParticleSystem>();

                        if (subParticles != null)
                            foreach (ParticleSystem sub in subParticles)
                                if (!particle.isPlaying) particle.Play();
                    }

                if (debug) Debug.Log("Cannon particles have played");
            }
    }
}
