using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    public class VelocityParticles : MonoBehaviour
    {
        public ParticleSystem[] particles;
        public float minVelocity = 5;


        private void Start()
        {
            foreach (ParticleSystem particle in particles)
                particle.Stop();
        }

        private void LateUpdate()
        {
            if (GetComponentInParent<Rigidbody>().velocity.magnitude > minVelocity)
            {
                foreach (ParticleSystem particle in particles)
                {
                    if (!particle.isPlaying) particle.Play();
                }
            }
            else
                foreach (ParticleSystem particle in particles)
                {
                    if (particle.isPlaying) particle.Stop();
                }
        }
    }
}
