﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Tutorial
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Collider))]

    public class TargetGoal : MonoBehaviour
    {
        public bool debug;

        // Play Goal effects if trigger is hit
        AudioSource sound;
        public ParticleSystem particle;

        public delegate void OnTargetHit();
        public static event OnTargetHit onTargetHit;

        private bool targethit = false;

        private void Awake()
        {
            sound = GetComponent<AudioSource>();
            if (!particle) particle = GetComponent<ParticleSystem>();
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

                // send on target hit event once
                if (!targethit)
                {
                    onTargetHit.Invoke();
                    targethit = true;
                }

                if (debug) Debug.Log("TargetGoal Hit!");
            }
        }
    }
}
