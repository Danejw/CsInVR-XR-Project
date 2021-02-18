using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

namespace CSInVR.Football
{
    [RequireComponent(typeof(Rigidbody))]

    public class Target : MonoBehaviour
    {
        public bool debug;

        public delegate void OnTargetHit();
        public static event OnTargetHit onTargetHit;

        [SerializeField] [EventRef] private string eventPath;
        private EventInstance eventInstance;

        public ParticleSystem[] particles;


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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Ball" && other.GetComponent<Ball>().GetBallIsActive())
            {
                // do things if the trigger is entered

                // play sound
                TargetHitSoundEffect();

                // play visual effect
                PlayHitParticleEffect();

                // send off event
                onTargetHit?.Invoke();
            }
        }

        private void TargetHitSoundEffect()
        {
            // Referee Whistle
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            RuntimeManager.StudioSystem.setParameterByName("SoundEffect", 0);
            eventInstance.start();

            if (debug) Debug.Log("Target hit sound have played");
        }

        private void PlayHitParticleEffect()
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

            if (debug) Debug.Log("Target hit particles have played");
        }
    }
}
