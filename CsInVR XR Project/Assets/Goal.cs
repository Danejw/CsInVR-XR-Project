using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR.Football
{
    [RequireComponent(typeof(Collider))]

    public class Goal : MonoBehaviour
    {
        public bool debug;

        public delegate void OnGoal();
        public static event OnGoal onGoal;

        public delegate void OnFirstDown();
        public static event OnFirstDown onFirstDown;

        [SerializeField] private bool goalMade = false;
        private bool madeFirstdown = false;

        [SerializeField] private int amtOfFirstdowns = 0;
        public int minFirstdowns = 3;

        private void MadeGoal()
        {
            onGoal?.Invoke();

            if (debug) Debug.Log("The Player has made a goal");
        }

        private void MadeFirstDown()
        {
            onFirstDown?.Invoke();

            if (debug) Debug.Log("The reciever has made a firstdown");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (amtOfFirstdowns < minFirstdowns && !madeFirstdown)
                {
                    amtOfFirstdowns++;
                    MadeFirstDown();
                    madeFirstdown = true;
                }
                else if (amtOfFirstdowns >= minFirstdowns && !goalMade)
                {
                    MadeGoal();
                    goalMade = true;
                }       
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (madeFirstdown)
                    madeFirstdown = false;

                if (goalMade)
                {
                    goalMade = false;
                    amtOfFirstdowns = 0;
                }
            }
        }
    }
}
