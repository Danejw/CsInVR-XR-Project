using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    public class NinjaEffect : MonoBehaviour
    {
        public bool debug;

        [SerializeField] private ParticleSystem[] particles;
        [SerializeField] private AudioSource sound;

        private bool isActive;

        private void OnEnable()
        {
            if (particles == null) particles = GetComponentsInChildren<ParticleSystem>();

            if (debug)
                foreach(ParticleSystem particle in particles)
                    Debug.Log(particle.name);

            PlayParticles();

            FootballGame.onReadyToStart += PlayFullEffect;
        }

        private void OnDisable()
        {
            FootballGame.onReadyToStart -= PlayFullEffect;
        }

        private void PlayFullEffect()
        {
            PlayParticles();
            PlaySoundEffect();
        }

        private void PlayParticles()
        {
            foreach (ParticleSystem particle in particles)
            {
                particle.Play();

                if (debug) Debug.Log(particle.name + " is playing");
            }
        }

        private void StopParticles()
        {
            foreach (ParticleSystem particle in particles)
            {
                particle.Stop();

                if (debug) Debug.Log(particle.name + " is stopping");
            }
        }

        private void PlaySoundEffect()
        {
            if (sound) sound.Play();
        }
    }
}
