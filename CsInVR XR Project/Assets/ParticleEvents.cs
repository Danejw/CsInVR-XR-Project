using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    public class ParticleEvents : MonoBehaviour
    {


        public ParticleSystem goalParticle;
        public ParticleSystem firstdownParticle;



        private void OnEnable()
        {
            //HikeBall.onHike += HikeSoundEvent;
            Reciever.onCatch += CatchParticleEvent;
            Goal.onGoal += GoalParticleEvent;
            //HikeBall.onMissedCatch += MissedCatchSoundEvent;
            FootballGame.onFirstdown += FirstdownParticleEvent;
            //FootballGame.onReadyToStart += ReadyToStartSoundEvent;
            Blocker.onBlock += BlockedParticleEvent;
            //FootballGame.onGameOver += GameOverEvent;
        }

        private void OnDisable()
        {
            Goal.onGoal -= GoalParticleEvent;
            Reciever.onCatch -= CatchParticleEvent;
            FootballGame.onFirstdown -= FirstdownParticleEvent;

        }


        private void GoalParticleEvent()
        {
            if (!goalParticle.isPlaying) goalParticle.Play();
        }

        private void FirstdownParticleEvent()
        {
            if (!firstdownParticle.isPlaying) firstdownParticle.Play();
        }

        private void CatchParticleEvent(GameObject reciever)
        {
            ParticleSystem[] particles = reciever.GetComponentsInChildren<ParticleSystem>();

            if (particles != null)
                foreach (ParticleSystem particle in particles)
                {
                    if (!particle.isPlaying) particle.Play();

                    ParticleSystem[] subParticles = particle.GetComponentsInChildren<ParticleSystem>();

                    if (subParticles != null)
                        foreach (ParticleSystem par in subParticles)
                            if (!par.isPlaying) par.Play();
                }
        }

        private void BlockedParticleEvent(GameObject blocker)
        {
            ParticleSystem[] particles = blocker.GetComponentsInChildren<ParticleSystem>();

            if (particles != null)
                foreach (ParticleSystem particle in particles)
                {
                    if (!particle.isPlaying) particle.Play();

                    ParticleSystem[] subParticles = particle.GetComponentsInChildren<ParticleSystem>();

                    if (subParticles != null)
                        foreach (ParticleSystem par in subParticles)
                            if (!par.isPlaying) par.Play();
                }
        }
    }
}
