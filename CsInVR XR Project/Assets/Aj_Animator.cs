using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    [RequireComponent(typeof(Animator))]

    public class Aj_Animator : MonoBehaviour
    {
        public bool debug;

        [SerializeField] private GameObject parent;

        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            HikeBall.onHike += Run;
            Reciever.onCatch += Catch;
            Blocker.onBlock += Block;
            HikeBall.onMissedCatch += MissedCatch;
            FootballGame.onReadyToStart += Idle;
        }

        private void OnDisable()
        {
            HikeBall.onHike -= Run;
            Reciever.onCatch -= Catch;
            Blocker.onBlock -= Block;
            HikeBall.onMissedCatch -= MissedCatch;
            FootballGame.onReadyToStart -= Idle;
        }


        private void Run()
        {
            // anim.SetTrigger("Run");

            // blend parameter
            anim.SetFloat("Run/Idle", 1);
        }

        private void Idle()
        {
            //anim.SetTrigger("Idle");

            // blend parameter
            anim.SetFloat("Run/Idle", 0);
        }

        private void Catch(GameObject reciever)
        {
            /*
            if (parent && reciever.name == parent.name)
                anim.SetTrigger("Victory");
            else
                anim.SetTrigger("Idle");
            */

            // blend parameter
            anim.SetFloat("Run/Idle", 0);
        }

        private void Block(GameObject Blocker)
        {
            // anim.SetTrigger("Sad");

            // blend parameter
            anim.SetFloat("Run/Idle", 0);
        }

        private void MissedCatch()
        {
            // anim.SetTrigger("Sad");

            // blend parameter
            anim.SetFloat("Run/Idle", 0);
        }
    }
}
