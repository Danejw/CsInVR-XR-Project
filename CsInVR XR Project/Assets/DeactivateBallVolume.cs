using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    public class DeactivateBallVolume : MonoBehaviour
    {

        public string ballTag;

        private void OnTriggerEnter(Collider other)
        {
            if (ballTag != null && other.gameObject.tag == ballTag)
            {
                other.GetComponent<HikeBall>().SetBallInActive(null);
            }
        }
    }
}
