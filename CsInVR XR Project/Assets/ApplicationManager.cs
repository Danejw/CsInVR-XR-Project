using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR
{
    // creates and manages other persistent game system managers
    public class ApplicationManager : Singleton<ApplicationManager>
    {
        public string openingSceneName;

        public GameObject[] SystemPrefabs;
        private List<GameObject> _instancedSystemPrefabs;


        protected override void Awake()
        {
            base.Awake();
             
            DontDestroyOnLoad(gameObject);

            InstantiateSystemPrefabs();
        }

        private void Start()
        {
            if (openingSceneName != null) StartCoroutine(RunOpeningScene(5));
        }


        // creates an instance of each SystemPrefab and add them to the _instancedSystemPrefab list
        private void InstantiateSystemPrefabs()
        {
            _instancedSystemPrefabs = new List<GameObject>();

            GameObject prefabInstance;

	        for (int i = 0; i < SystemPrefabs.Length; i++)
            {
		        prefabInstance = Instantiate(SystemPrefabs[i], transform);
                _instancedSystemPrefabs.Add(prefabInstance);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            for (int i = 0; i < _instancedSystemPrefabs.Count; i++)
                Destroy(_instancedSystemPrefabs[i]);

            _instancedSystemPrefabs.Clear();
        }

        private IEnumerator RunOpeningScene(float delay)
        {
            yield return new WaitForSeconds(delay);
            TransitionManager.Instance?.SceneLoadUnload(openingSceneName);
        }
    }
}
