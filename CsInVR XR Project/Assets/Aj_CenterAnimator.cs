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


        private void Push()
        {
            // blend parameter
            anim.SetFloat("EmotionBlend", 1);
        }

        private void Idle()
        {
            // blend parameter
            anim.SetFloat("EmotionBlend", 0);
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
            anim.SetFloat("EmotionBlend", 2);
        }

        private void Defeat()
        {
            // blend parameter
            anim.SetFloat("EmotionBlend", 4);
        }

        private void Victory()
        {
            // blend parameter
            anim.SetFloat("EmotionBlend", 3);
        }
    }
}
