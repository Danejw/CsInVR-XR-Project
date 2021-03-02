using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BNG;

namespace CSInVR.Football
{
    public class InputManager : MonoBehaviour
    {
        public UnityEvent BButtonDown;
        public UnityEvent AButtonDown;
        public UnityEvent YButtonDown;
        public UnityEvent XButtonDown;


        private void Update()
        {
            if (InputBridge.Instance.BButtonDown)
                BButtonDown?.Invoke();
            if (InputBridge.Instance.AButtonDown)
                AButtonDown?.Invoke();
            if (InputBridge.Instance.YButtonDown)
                YButtonDown?.Invoke();
            if (InputBridge.Instance.XButtonDown)
                XButtonDown?.Invoke();
        }
    }
}
