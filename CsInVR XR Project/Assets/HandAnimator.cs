using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

namespace CSInVR
{
    public class HandAnimator : MonoBehaviour
    {
        private enum handside
        {
            left,
            right
        }

        [SerializeField]
        private handside side;

        public Animator anim;


        void Update()
        {
            if (side == handside.left)
            {
                anim.SetFloat("LeftTrigger", InputBridge.Instance.LeftTrigger);
                anim.SetFloat("LeftGrip", InputBridge.Instance.LeftGrip);
            }
            else
            {
                anim.SetFloat("RightTrigger", InputBridge.Instance.RightTrigger);
                anim.SetFloat("RightGrip", InputBridge.Instance.RightGrip);
            }

        }
    }
}
