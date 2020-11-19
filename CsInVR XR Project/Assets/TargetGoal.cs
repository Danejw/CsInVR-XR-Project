using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Collider))]

    public class TargetGoal : MonoBehaviour
    {
        // Play Goal effects if trigger is hit
        AudioSource sound;
        public ParticleSystem particle;

        private void Awake()
        {
            sound = GetComponent<AudioSource>();
            if (particle) particle = GetComponent<ParticleSystem>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Ball")
            {
                // Play particle
                if (particle & !particle.isPlaying)
                    particle.Play();

                // Play Sound
                if (!sound.isPlaying)
                    sound.Play();

                Debug.Log("TargetGoal Hit!");
            }
        }
    }
}
