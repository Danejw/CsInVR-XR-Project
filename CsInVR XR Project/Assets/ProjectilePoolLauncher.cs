using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;


namespace CSInVR
{
    public class ProjectilePoolLauncher : MonoBehaviour
    {
        public bool debug;

        public delegate void OnShoot();
        public static event OnShoot onShoot;


        /// <summary>
        /// Launch this from the 
        /// </summary>
        [SerializeField]
        private ProjectilePool projectilePool;

        public float ProjectileForce = 15f;

        public AudioClip LaunchSound;

        // public ParticleSystem LaunchParticles;

        public bool shoot;

        /// <summary>
        /// Where the projectile will launch from
        /// </summary>
        public Transform MuzzleTransform;

        private float _initialProjectileForce;


        // Start is called before the first frame update
        void Start()
        {
            // Setup initial velocity for launcher so we can modify it later
            _initialProjectileForce = ProjectileForce;

            if (!projectilePool) projectilePool = ProjectilePool.Instance;
        }

        private void FixedUpdate()
        {
            if (shoot == true)
            {
                ShootProjectile();

                // send shoot event
                onShoot.Invoke(); ;

                shoot = false;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShootProjectile();

                // send shoot event
                onShoot.Invoke(); ;

                shoot = false;
            }
        }


        /// <summary>
        /// Returns the object that was shot
        /// </summary>
        public GameObject ShootProjectile(float projectileForce)
        {
            if (MuzzleTransform && projectilePool)
            {
                GameObject launched = projectilePool?.RequestPooledObject();

                if (launched)
                {
                    launched.SetActive(true);
                    launched.transform.position = MuzzleTransform.transform.position;
                    launched.transform.rotation = MuzzleTransform.transform.rotation;

                    launched.GetComponentInChildren<Rigidbody>().AddForce(MuzzleTransform.forward * projectileForce, ForceMode.VelocityChange);

                    if (LaunchSound) VRUtils.Instance.PlaySpatialClipAt(LaunchSound, launched.transform.position, 1f);

                    /*
                    if (LaunchParticles)
                    {
                        LaunchParticles.Play();

                        if (LaunchParticles.GetComponentsInChildren<ParticleSystem>() != null)
                        {
                            foreach (ParticleSystem particle in launched.GetComponentsInChildren<ParticleSystem>())
                            {
                                particle.Play();

                                if (debug) Debug.Log("Played particles in children " + transform.position);
                            }
                        }

                        if (debug) Debug.Log("Played particle " + launched.name);
                    }
                    */

                    if (debug) Debug.Log("Launched " + launched.name);

                    return launched;
                }
            }
            return null;
        }

        public void ShootProjectile()
        {
            ShootProjectile(ProjectileForce);
        }

        public void SetForce(float force)
        {
            ProjectileForce = force;
        }

        public float GetInitialProjectileForce()
        {
            return _initialProjectileForce;
        }
    }
}

