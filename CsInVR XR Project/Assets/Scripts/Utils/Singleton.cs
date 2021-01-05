using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T> // T stands for Type, you need to pass in the GameObject type
    {
        private static T instance;

        public static T Instance
        { // accessor
            get { return instance; }
        }

        public static bool isInitialized
        {  // accessor
            get { return instance != null; }
        }

        protected virtual void Awake()
        {  // virtual means it can be overridden
            if (instance != null)
                Debug.LogError("[Singleton] trying to instantiate a second instance of a singleton class.");
            else
                instance = (T)this;
        }

        protected virtual void OnDestroy()
        { // protected means this method can be called by methods that extend this class
            if (instance == this) instance = null;
        }
    }
}

