using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Tutorial
{
    [RequireComponent(typeof(Collider))]

    public class ResetBallVolume : MonoBehaviour
    {
        public string tag;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == tag)
                Destroy(other.gameObject);
        }
    }
}
