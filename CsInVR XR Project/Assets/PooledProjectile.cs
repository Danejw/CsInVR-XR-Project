using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR
{
    public class PooledProjectile : MonoBehaviour
    {
        /*
         * This script is placed on the projectiles that are going to be pooled.
         * When the projectile needs to be pooled, 
         * this script will request the pool to collect this projectile, 
         * where it will reside unitil it's launched again.
         */

        public bool debug;

        [SerializeField]
        private ProjectilePool projectilePool;

        [SerializeField]
        private bool useTrigger;
        [SerializeField]
        private bool useCollision;

        [SerializeField]
        private float timeTillCollected = 5;

        [Tooltip("An Array of Tags to Ignore")]
        public string[] ignoreTags;
        [Tooltip("An Array of Tags to Include")]
        public string[] includeTags;


        private void Awake()
        {
            projectilePool = ProjectilePool.Instance;
        }

        private void OnEnable()
        {
            projectilePool?.CollectPooledObject(this.gameObject, timeTillCollected);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!useCollision) return;

            // ignore self
            if (collision.gameObject.tag == this.gameObject.tag) return;

            // ignore an array of tags
            if (ignoreTags.Length > 0)
            {
                foreach (string tag in ignoreTags)
                {
                    if (collision.gameObject.tag == tag) return;
                }
            }

            // explode if the colliding object has a tag within the array of includeTags
            if (includeTags.Length > 0)
            {
                foreach (string tag in includeTags)
                {
                    if (collision.gameObject.tag == tag)
                    {
                        projectilePool?.CollectPooledObject(gameObject, timeTillCollected);

                        if (debug) Debug.Log("Collecting " + gameObject.name);
                    }
                }
            }
            else
            {
                projectilePool?.CollectPooledObject(gameObject, timeTillCollected);

                if (debug) Debug.Log("Collecting " + gameObject.name);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!useTrigger) return;

            // ignore self
            if (other.gameObject.tag == this.gameObject.tag) return;

            // ignore an array of tags
            if (ignoreTags.Length > 0)
            {
                foreach (string tag in ignoreTags)
                {
                    if (other.gameObject.tag == tag) return;
                }
            }

            // explode if the colliding object has a tag within the array of includeTags
            if (includeTags.Length > 0)
            {
                foreach (string tag in includeTags)
                {
                    if (other.gameObject.tag == tag)
                    {
                        projectilePool?.CollectPooledObject(gameObject, timeTillCollected);

                        if (debug) Debug.Log("Collecting " + gameObject.name);
                    }
                }
            }
            else
            {
                projectilePool?.CollectPooledObject(gameObject, timeTillCollected);

                if (debug) Debug.Log("Collecting " + gameObject.name);
            }
        }
    }
}

