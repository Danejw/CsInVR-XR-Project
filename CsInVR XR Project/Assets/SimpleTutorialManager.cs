using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Tutorial
{
    public class SimpleTutorialManager : MonoBehaviour
    {
        public bool debug;

        private bool isTutorialComplete = false;

        // event to send if target is hit
        public delegate void OnTutorialEnd();
        public static event OnTutorialEnd onTutorialEnd;

        // collects a count of how many targets are in the scene
        private TargetGoal[] targetsInScene;

        // counts up every time a target is hit
        private int targetsHit = 0;

        
        private void Start()
        {
            targetsInScene = FindObjectsOfType<TargetGoal>();

            if (debug) Debug.Log("Amount of targets in scene: " + targetsInScene.Length);

            targetsHit = 0;
            isTutorialComplete = false;
        }

        private void Awake()
        {
            TargetGoal.onTargetHit += AddToTargetsHit;
        }

        private void OnDisable()
        {
            TargetGoal.onTargetHit -= AddToTargetsHit;
        }

        private void Update()
        {
            // send tutorial complete event once if the amount of targets hit is equal the amount of targets in the scene
            // determines when the tutorial ends
            if (!isTutorialComplete)
            {
                if (targetsHit >= targetsInScene.Length)
                {
                    onTutorialEnd?.Invoke();
                    isTutorialComplete = true;

                    if (debug) Debug.Log("The Tutorial has been completed");
                }
            }
        }


        private void AddToTargetsHit()
        {
            targetsHit += 1;

            if (debug) Debug.Log("Targets hit: " + targetsHit);
        }


        public void ResetTutorial()
        {
            foreach (TargetGoal target in targetsInScene)
            {
                target.targethit = false;
            }

            isTutorialComplete = false;
            targetsHit = 0;
        }

    }
}
