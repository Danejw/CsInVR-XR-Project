using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR
{
    public class OnActiveTrailParticles : MonoBehaviour
    {
        private ParticleSystem[] particles;

        private void OnEnable()
        {
            // get the particles
            particles = GetComponentsInChildren<ParticleSystem>();
        }

        private void Update()
        {
            if (GetComponentInParent<Ball>().GetBallIsActive())
            {
                foreach (ParticleSystem particle in particles)
                {
                    if (!particle.isPlaying) particle.Play();
                }
            }
            else
            {
                foreach (ParticleSystem particle in particles)
                {
                    if (particle.isPlaying) particle.Stop();
                }
            }
    }
    }
}
