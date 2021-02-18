using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR
{
    [RequireComponent(typeof(Animator))]

    // acccess' the animator and sets its trigger to loop in/out
    public class UILoop : MonoBehaviour
    {
        public bool debug;

        Animator animator;

        private void OnEnable()
        {
            animator = GetComponent<Animator>();

            LoopIn();
        }

        // animate loop in
        public void LoopIn()
        {
            if (animator) animator.SetTrigger("Loop In");
        }

        // animate loop out
        public void LoopOut()
        {
            if (animator) animator.SetTrigger("Loop Out");
        }

        private void Update()
        {
            if (debug)
            {
                // toggle outer loop
                if (Input.GetKeyDown(KeyCode.K))
                    LoopIn();
                if (Input.GetKeyDown(KeyCode.L))
                    LoopOut();
            }
        }
    }
}
