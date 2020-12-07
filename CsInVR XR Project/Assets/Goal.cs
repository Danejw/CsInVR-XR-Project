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


        private void MadeGoal()
        {
            onGoal?.Invoke();

            if (debug) Debug.Log("The reciever has made a goal");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Reciever")
            {
                Reciever reciever = other.GetComponent<Reciever>();

                if (reciever && reciever.hasCaught)
                {
                    MadeGoal();
                }
            }
        }

    }
}
