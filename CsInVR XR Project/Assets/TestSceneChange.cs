using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR
{
    public class TestSceneChange : Singleton<TestSceneChange>
    {
        public bool test;

        private void Update()
        {
            if (test)
            {
                if (Input.GetKeyDown(KeyCode.B))
                {
                    TransitionManager.Instance?.SceneLoadUnload("Football Home Scene");
                }

                if (Input.GetKeyDown(KeyCode.N))
                {
                    TransitionManager.Instance?.SceneLoadUnload("Football Quarterback Touchdown Challenge");
                }

                if (Input.GetKeyDown(KeyCode.M))
                {
                    TransitionManager.Instance?.SceneLoadUnload("Football Quarterback Precision Challenge");
                }

                if (Input.GetKeyDown(KeyCode.V))
                {
                    TransitionManager.Instance?.SceneLoadUnload("Persistent Scene");
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    TransitionManager.Instance?.SceneLoadUnload("CSInVR Assignment 7 Grabbable Gun");
                }


                if (Input.GetKeyDown(KeyCode.X))
                {
                    TransitionManager.Instance?.SceneLoadUnload("Football Tutorial Scene");
                }
            }
        }

        public void LoadHomeScene()
        {
            TransitionManager.Instance?.SceneLoadUnload("Football Home Scene");
        }

        public void LoadTouchdownChallenge()
        {
            TransitionManager.Instance?.SceneLoadUnload("Football Quarterback Touchdown Challenge");
        }

        public void LoadPrecisionChallenge()
        {
            TransitionManager.Instance?.SceneLoadUnload("Football Quarterback Precision Challenge");
        }

        public void LoadAssignment7()
        {
            TransitionManager.Instance?.SceneLoadUnload("CSInVR Assignment 7 Grabbable Gun");
        }

        public void LoadTutorialScene()
        {
            TransitionManager.Instance?.SceneLoadUnload("Football Tutorial Scene");
        }

    }
}