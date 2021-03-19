using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    public class ParticleEvents : MonoBehaviour
    {
        public bool debug;

        public ParticleSystem goalParticle;
        public ParticleSystem firstdownParticle;

        [SerializeField] private string enabledParticleName = "NinjaEffect";
        [SerializeField] private string disabledParticleName = "NinjaEffect";
        [SerializeField] private string catchParticleName = "CatchEffect";



        private void OnEnable()
        {
            //HikeBall.onHike += HikeSoundEvent;
            Reciever.onCatch += CatchParticleEvent;
            Goal.onGoal += GoalParticleEvent;
            //HikeBall.onMissedCatch += MissedCatchSoundEvent;
            FootballGame.onFirstdown += FirstdownParticleEvent;
            //FootballGame.onReadyToStart += ReadyToStartSoundEvent;
            Agent_CenterAttacker.onBlock += BlockedParticleEvent;
            //FootballGame.onGameOver += GameOverEvent;

            Agent.onAgentEnabled += AgentEnabledParticleEvent;
            Agent.onAgentDisabled += AgentDisabledParticleEvent;
        }

        private void OnDisable()
        {
            Goal.onGoal -= GoalParticleEvent;
            Reciever.onCatch -= CatchParticleEvent;
            FootballGame.onFirstdown -= FirstdownParticleEvent;
            Agent_CenterAttacker.onBlock -= BlockedParticleEvent;

            Agent.onAgentEnabled -= AgentEnabledParticleEvent;
            Agent.onAgentDisabled -= AgentDisabledParticleEvent;
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
            ParticleSystem[] particles = reciever.transform.Find(catchParticleName).GetComponentsInChildren<ParticleSystem>();

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

        private void AgentEnabledParticleEvent(Transform agent)
        {
            ParticleSystem[] particles = agent.Find(enabledParticleName).GetComponentsInChildren<ParticleSystem>();

            if (debug) Debug.Log("Poof Enabled Particle Effect");

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

        private void AgentDisabledParticleEvent(Transform agent)
        {
            ParticleSystem[] particles = agent.Find(disabledParticleName).GetComponentsInChildren<ParticleSystem>();

            if (debug) Debug.Log("Poof Disabled Particle Effect");

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
