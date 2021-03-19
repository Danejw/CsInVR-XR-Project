using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    [RequireComponent(typeof(Animator))]

    public class Aj_CenterAnimator : MonoBehaviour
    {
        public bool debug;

        [SerializeField] private GameObject parent;
        private Rigidbody parentRig;

        private Animator anim;

        [SerializeField] private string objTag = "Blocker";
        [SerializeField] private bool foundBlocker = false;

        [SerializeField] private int currentState;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            HikeBall.onHike += Push;
            Reciever.onCatch += Catch;
            Agent_CenterAttacker.onBlock += Block;
            HikeBall.onMissedCatch += MissedCatch;
            FootballGame.onReadyToStart += Idle;
            FootballGame.onGameOver += Defeat;
            Goal.onGoal += Victory;

            if (parent)
                parentRig = parent.GetComponent<Rigidbody>();
        }

        private void OnDisable()
        {
            HikeBall.onHike -= Push;
            Reciever.onCatch -= Catch;
            Agent_CenterAttacker.onBlock -= Block;
            HikeBall.onMissedCatch -= MissedCatch;
            FootballGame.onReadyToStart -= Idle;
            FootballGame.onGameOver -= Defeat;
            Goal.onGoal -= Victory;
        }

        /*
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == objTag && !foundBlocker)
            {
                foundBlocker = true;
                Push();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == objTag && !foundBlocker)
            {
                foundBlocker = true;
                Push();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == objTag && foundBlocker)
            {
                Idle();
                foundBlocker = false;
            }
        }

        */


        private void Walk()
        {
            // blend parameter
            anim.SetFloat("EmotionBlend", Mathf.Lerp(currentState,5,1));
            currentState = 5;
        }

        private void Push()
        {
            // blend parameter
            anim.SetFloat("EmotionBlend", Mathf.Lerp(currentState, 1, 1));
            currentState = 1;
        }

        private void Idle()
        {
            // blend parameter
            anim.SetFloat("EmotionBlend", Mathf.Lerp(currentState, 0, 1));
            currentState = 0;
        }

        private void Catch(GameObject reciever)
        {
            Victory();
        }

        private void Block(GameObject Blocker)
        {
            Defeat();
        }

        private void MissedCatch()
        {
            // blend parameter
            anim.SetFloat("EmotionBlend", Mathf.Lerp(currentState, 2, 1));
            currentState = 2;
        }

        private void Defeat()
        {
            // blend parameter
            anim.SetFloat("EmotionBlend", Mathf.Lerp(currentState, 4, 1));
            currentState = 4;
        }

        private void Victory()
        {
            // blend parameter
            anim.SetFloat("EmotionBlend", Mathf.Lerp(currentState, 3, 1));
            currentState = 3;
        }
    }
}
