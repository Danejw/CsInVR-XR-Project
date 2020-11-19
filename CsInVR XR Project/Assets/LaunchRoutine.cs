using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

namespace CSInVR
{
    [RequireComponent(typeof(ProjectilePoolLauncher))]

    public class LaunchRoutine : MonoBehaviour
    {
        public bool debug;

        public bool startRoutine = false;

        [SerializeField]
        private float timeBetweenShots = 5;
        private bool canShoot = true;

        private ProjectilePoolLauncher launcher;


        private void Awake()
        {
            launcher = GetComponent<ProjectilePoolLauncher>();
        }

        private void OnEnable()
        {
            //GameManager.GameStarted += StartRoutine;
            //GameManager.GameEnded += StopRoutine;
        }

        private void OnDisable()
        {
            //GameManager.GameStarted -= StartRoutine;
            //GameManager.GameEnded -= StopRoutine;
        }

        private void FixedUpdate()
        {
            if (startRoutine)
            {
                if (canShoot)
                {
                    canShoot = false;
                    StartCoroutine(BetweenShots(timeBetweenShots));
                }
            }
        }

        private void StartRoutine()
        {
            startRoutine = true;

            if (debug) Debug.Log("Starting Launcher Routine");
        }

        private void StopRoutine()
        {
            startRoutine = false;

            if (debug) Debug.Log("Stoping Launcher Routine");
        }

        private IEnumerator BetweenShots(float time)
        {
            yield return new WaitForSeconds(time);
            launcher.shoot = true;
            canShoot = true;
        }
    }
}
