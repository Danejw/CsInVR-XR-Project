using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR
{
    public class AudioManager : Singleton<AudioManager>
    {
        public bool debug;

        public GameObject crowdArrayPrefab;
        public GameObject musicArrayPrefab;
        public GameObject announcerArrayPrefab;

        private GameObject crowdInstance;
        private GameObject musicInstance;
        private GameObject announcerInstance;

        private void OnEnable()
        {
            DontDestroyOnLoad(gameObject);

            // instantiate corwd array
            InstantiateCrowdArray();
            InstantiateMusicArray();
            InstantiateAnnouncerArray();
        }

        public void InstantiateCrowdArray()
        {
            if (crowdArrayPrefab && !crowdInstance)
            {
                crowdInstance = Instantiate(crowdArrayPrefab, transform);
                if (debug) Debug.Log("Instantiated Crowd Sound Array");
            }
            else 
                if (debug) Debug.Log("Crowd Sound Array is NOT assigned");


        }

        public void InstantiateMusicArray()
        {
            if (musicArrayPrefab && !musicInstance)
            {
                musicInstance = Instantiate(musicArrayPrefab, transform);
                if (debug) Debug.Log("Instantiated Music Speaker Array");
            }
            else 
                if (debug) Debug.Log("Music Speaker Array is NOT assigned");
        }

        public void InstantiateAnnouncerArray()
        {
            if (musicArrayPrefab && !announcerInstance)
            {
                announcerInstance = Instantiate(announcerArrayPrefab, transform);
                if (debug) Debug.Log("Instantiated Announcer Speaker Array");
            }
            else
                if (debug) Debug.Log("Announcer Speaker Array is NOT assigned");
        }

        public void DestroyCrowdArray()
        {
            if (crowdInstance)
            {
                Destroy(crowdInstance);
                if (debug) Debug.Log("Destroyed Music Speaker Array");
            }
        }

        public void DestroyMusicArray()
        {
            if (musicInstance)
            {
                Destroy(musicInstance);
                if (debug) Debug.Log("Destroyed Music Speaker Array");
            }
        }

        public void DestroyAnnouncerArray()
        {
            if (announcerInstance)
            {
                Destroy(announcerInstance);
                if (debug) Debug.Log("Destroyed Announcer Speaker Array");
            }
        }
    }
}
