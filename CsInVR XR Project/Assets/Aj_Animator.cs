﻿using System.Collections;
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
            Agent_CenterAttacker.onBlock += Block;
            HikeBall.onMissedCatch += MissedCatch;
            FootballGame.onReadyToStart += ReadyIdle;
            FootballGame.onGameOver += Defeat;
            Goal.onGoal += Victory;
        }

        private void OnDisable()
        {
            HikeBall.onHike -= Run;
            Reciever.onCatch -= Catch;
            Agent_CenterAttacker.onBlock -= Block;
            HikeBall.onMissedCatch -= MissedCatch;
            FootballGame.onReadyToStart -= ReadyIdle;
            FootballGame.onGameOver -= Defeat;
            Goal.onGoal -= Victory;
        }


        private void ReadyIdle()
        {
            // blend parameter
            anim.SetFloat("Blend", 0);
        }

        private void Run()
        {
            // blend parameter
            anim.SetFloat("Blend", 1);
        }

        private void Catch(GameObject reciever)
        {
            Victory();
        }

        private void Block(GameObject Blocker)
        {
            // blend parameter
            anim.SetFloat("Blend", 2);
        }

        private void MissedCatch()
        {
            // blend parameter
            anim.SetFloat("Blend", 2);
        }

        private void Victory()
        {
            // blend parameter
            anim.SetFloat("Blend", 4);
        }

        private void Defeat()
        {
            // blend parameter
            anim.SetFloat("Blend", 5);
        }


    }
}
