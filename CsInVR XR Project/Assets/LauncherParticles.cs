using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR
{
    public class LauncherParticles : MonoBehaviour
    {
        public bool debug;

        private ParticleSystem particle;


        private void Start()
        {
            particle = GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            ProjectilePoolLauncher.onShoot += PlayParticles;

            if (debug) Debug.Log("Launcher particles subscribed to launcher onShoot event");
        }

        private void OnDisable()
        {
            ProjectilePoolLauncher.onShoot -= PlayParticles;

            if (debug) Debug.Log("Launcher particles un-subscribed to launcher onShoot event");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                PlayParticles();
        }

        private void PlayParticles()
        {
            particle.Play();

            if (debug) Debug.Log("Launcher particles have played");
        }


    }
}
