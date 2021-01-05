using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR
{
    public class SceneChange : MonoBehaviour
    {
        public string sceneName;

        public void GotoScene()
        {
            TransitionManager.Instance?.SceneLoadUnload(sceneName);

            Debug.Log("Scene changing");
        }
    }
}
