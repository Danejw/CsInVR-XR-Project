using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR.Football
{
    public class Goal : MonoBehaviour
    {
        public bool debug;

        public delegate void OnGoal();
        public static event OnGoal onGoal;

        public delegate void OnFirstDown();
        public static event OnFirstDown onFirstDown;

        // [SerializeField] private bool goalMade = false;
        // private bool madeFirstdown = false;

        public int amtOfFirstdowns = 0;
        public int minFirstdowns = 3;


        public void MadeGoal()
        {
            onGoal?.Invoke();

            if (debug) Debug.Log("The Player has made a goal");
        }

        public void MadeFirstDown()
        {
            onFirstDown?.Invoke();

            if (debug) Debug.Log("The reciever has made a firstdown");
        }
    }
}
