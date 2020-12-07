using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR
{
    public class ProjectilePool : MonoBehaviour
    {
        /*
        * At start, a pool of objects gets created
        * Another script can request for an pooled object, and if available given it
        * When that object has lived its life it needs to be collected
        */

        public bool debug;

        // Singleton
        private static ProjectilePool _instance;
        public static ProjectilePool Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProjectilePool();
                }
                return _instance;
            }
        }

        [SerializeField]
        private GameObject objToBePooled;
        [SerializeField]
        private int maxAmount = 3;

        // list of objects
        public List<GameObject> pooledObjects;


        private void Start()
        {
            if (!objToBePooled) Debug.LogError("ObjToBePooled is NULL");

            CreatePool();
        }

        private void Awake()
        {
            _instance = this;
        }

        private void CreatePool()
        {
            pooledObjects = new List<GameObject>();
            for (int i = 0; i < maxAmount; i++)
            {
                GameObject obj = Instantiate(objToBePooled);
                obj.SetActive(false);
                pooledObjects.Add(obj);
                obj.transform.position = this.transform.position;
                obj.transform.SetParent(this.transform);

                if (debug) Debug.Log("ObjectPool of " + objToBePooled.name + " created");
            }
        }

        // Finds the first unactive object in pool and returns it
        public GameObject RequestPooledObject()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    if (debug) Debug.Log(pooledObjects[i].name + " has been successfully requested");

                    return pooledObjects[i];
                }
            }

            if (debug) Debug.Log("There are NO available objects at the moment");

            return null;
        }

        public void CollectPooledObject(GameObject obj, float delay)
        {
            StartCoroutine(CollectObject(obj, delay));
        }

        private IEnumerator CollectObject(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            obj.SetActive(false);
            obj.transform.SetParent(this.transform);
            obj.transform.position = this.transform.position;
            obj.GetComponentInChildren<Rigidbody>().velocity = new Vector3(0, 0, 0);
            obj.GetComponentInChildren<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);

            if (debug) Debug.Log(obj.name + " has been successfully collected");
        }
    }
}
