using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR
{
    public class UIManager : Singleton<UIManager>
    {
        public bool debug;

        [SerializeField] private GameObject innerLoop;
        [SerializeField] private GameObject outerLoop;

        UILoop inner;
        UILoop outer;

        // manages state of the ui
        [SerializeField] bool isInnerLoopActive;
        [SerializeField] bool isOuterLoopActive;


        private void OnEnable()
        {
            inner = innerLoop.GetComponent<UILoop>();
            outer = outerLoop.GetComponent<UILoop>();
        }

        // tests
        private void Update()
        {
            if (debug)
            {
                // toggle inner loop
                if (Input.GetKeyDown(KeyCode.I))
                    //ToggleInnerLoop(true);
                if (Input.GetKeyDown(KeyCode.O))
                    //ToggleInnerLoop(false);

                // toggle outer loop
                if (Input.GetKeyDown(KeyCode.K))
                    DoOuterLoopIn();
                if (Input.GetKeyDown(KeyCode.L))
                    DoOuterLoopOut();
            }
        }


        public void ToggleInnerLoop()
        {
            if (isInnerLoopActive) DoInnerLoopOut();
            else DoInnerLoopIn();
        }

        public void ToggleOuterLoop()
        {
            if (isOuterLoopActive) DoOuterLoopOut();
            else DoOuterLoopIn();
        }

        public void DoInnerLoopIn()
        {
            inner?.LoopIn();
            isInnerLoopActive = true;

            if (debug) Debug.Log("Looping the inner UI in");
        }

        public void DoInnerLoopOut()
        {
            inner?.LoopOut();
            isInnerLoopActive = false;

            if (debug) Debug.Log("Looping the inner UI out");
        }

        public void DoOuterLoopIn()
        {
            outer?.LoopIn();
            isOuterLoopActive = true;

            if (debug) Debug.Log("Looping the outer UI in");
        }

        public void DoOuterLoopOut()
        {
            outer?.LoopOut();
            isOuterLoopActive = false;

            if (debug) Debug.Log("Looping the outer UI out");
        }
    }
}
