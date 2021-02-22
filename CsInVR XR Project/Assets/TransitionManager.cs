using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace CSInVR
{
    public class TransitionManager : Singleton<TransitionManager>
    {
        public bool debug;

        public string nameOfSceneToLoad;
        public bool loadScene = false;

        List<AsyncOperation> _loadOperations;
        private string _currentSceneName = string.Empty;
        private string _previousSceneName = string.Empty;

        public string homeScene;

        


        private void OnEnable()
        {
            DontDestroyOnLoad(gameObject);

            // Football.FootballGame.onGameOver += LoadHomeScene;

            _loadOperations = new List<AsyncOperation>();
            _currentSceneName = SceneManager.GetActiveScene().name;
        }

        private void OnDisable()
        {
            // Football.FootballGame.onGameOver -= LoadHomeScene;
        }

        private void OnLoadOperationComplete(AsyncOperation ao)
        {
            if (debug) Debug.Log("Load Complete");

            if (_loadOperations.Contains(ao))
            {
                _loadOperations.Remove(ao);
                // dispatch messages
                // transition between scenes
            }
        }

        private void OnUnLoadOperationComplete(AsyncOperation ao)
        {
            if (debug) Debug.Log("UnLoad Complete");

            // loop the UI out
            UIManager.Instance?.DoInnerLoopOut();
        }

        public void LoadLevel(string sceneName)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            if (ao == null)
            {
                Debug.LogError("[GameManager] unable to load level " + sceneName);
                return;
            }

            if (sceneName != "Load" && sceneName != "Persistent Scene")
            {
                ao.completed += OnLoadOperationComplete;
                _loadOperations.Add(ao);

                _previousSceneName = _currentSceneName;
                _currentSceneName = sceneName;
            }
        }

        public void LoadLevelAdditive(string sceneName)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            if (ao == null)
            {
                Debug.LogError("[GameManager] unable to load level " + sceneName);
                return;
            }

            if (sceneName != "Load" || sceneName != "Persistent Scene")
            {
                ao.completed += OnLoadOperationComplete;
                _loadOperations.Add(ao);

                _previousSceneName = _currentSceneName;
                _currentSceneName = sceneName;
            }
        }

        public void UnloadLevel(string sceneName)
        {
            AsyncOperation ao = SceneManager.UnloadSceneAsync(sceneName);

            if (ao == null)
            {
                Debug.LogError("[GameManager] unable to load level " + sceneName);
                return;
            }

            if (sceneName != "Persistent Scene")
                ao.completed += OnUnLoadOperationComplete;
        }


        public void SceneLoadUnload(string sceneNameToLoad)
        {
            StartCoroutine(SceneLoadUnloadTransition(10f, sceneNameToLoad));
        }

        private IEnumerator SceneLoadUnloadTransition(float seconds, string sceneNameToLoad)
        {
            if (debug) Debug.Log("Transitioning scenes...");

            // wait unitl the UI has fully looped
            UIManager.Instance?.DoInnerLoopIn();
            yield return new WaitForSeconds(seconds / 4);

            // load the transition/loading scene in additively
            LoadLevelAdditive("Load");
            //yield return new WaitForSeconds(seconds/4);

            // unload the previous scene out
            UnloadLevel(_previousSceneName);
            if (debug) Debug.Log("unloading " + _previousSceneName);

            // load the new scene in
            LoadLevelAdditive(sceneNameToLoad);
            if (debug) Debug.Log("loading " + sceneNameToLoad);
            yield return new WaitForSeconds(seconds/4);

            // unload the transition/loading scene
            UnloadLevel("Load");
            yield return new WaitForSeconds(seconds / 4);

            // loop the UI out
            //UIManager.Instance?.DoInnerLoopOut();
        }

        public IEnumerator DelayLoadScene(float seconds, string sceneName)
        {
            yield return new WaitForSeconds(seconds);
            SceneLoadUnload(sceneName);
        }


        public void LoadHomeScene()
        {
            if (homeScene != null) StartCoroutine(DelayLoadScene(3f, homeScene));
        }






        // tests
        private void Update()
        {
            if (loadScene)
            {
                SceneLoadUnload(nameOfSceneToLoad);
                loadScene = false;
            }
        }
    }
}