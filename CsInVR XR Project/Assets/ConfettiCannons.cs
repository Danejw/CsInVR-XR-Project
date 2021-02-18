using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{

    // shoot the confetti cannons when the touchdown event occurs
    public class ConfettiCannons : MonoBehaviour
    {
        public bool debug;
        public ParticleSystem[] confetti;


        // sign up to events
        private void OnEnable()
        {
            Goal.onGoal += ShootConfetti;
            FootballGame.onReadyToStart += StopConfetti;
        }

        private void OnDisable()
        {
            Goal.onGoal -= ShootConfetti;
            FootballGame.onReadyToStart -= StopConfetti;
        }

        // play confetti particles and child particles one layer deep
        private void ShootConfetti()
        {
            if (confetti != null)
            {             
                foreach (ParticleSystem particle in confetti)
                {
                    ParticleSystem[] subParticles = particle.GetComponentsInChildren<ParticleSystem>();

                    foreach (ParticleSystem sub in subParticles)
                        if (!sub.isPlaying) sub.Play();

                    if (!particle.isPlaying) particle.Play();

                    if (debug) Debug.Log("Playing Confetti");
                }
            }
        }

        // stop confetti particles and child particles one layer deep
        private void StopConfetti()
        {
            if (confetti != null)
            {
                foreach (ParticleSystem particle in confetti)
                {
                    ParticleSystem[] subParticles = particle.GetComponentsInChildren<ParticleSystem>();

                    foreach (ParticleSystem sub in subParticles)
                        if (sub.isPlaying) sub.Stop();

                    if (particle.isPlaying) particle.Stop();

                    if (debug) Debug.Log("Stopped Confetti");
                }
            }
        }
    }
}
