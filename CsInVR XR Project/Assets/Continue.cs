using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSInVR;

namespace CSInVR.Tutorial
{
    public class Continue : MonoBehaviour
    {
        public string sceneNameToLoad;
        public float delayTime = 3;

        public void LoadNextLevel()
        {
            if (sceneNameToLoad != null) StartCoroutine(RunNextScene(delayTime));
        }

        private IEnumerator RunNextScene(float delay)
        {
            yield return new WaitForSeconds(delay);
            TransitionManager.Instance?.SceneLoadUnload(sceneNameToLoad);
        }
    }
}
