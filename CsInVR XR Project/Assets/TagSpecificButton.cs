using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

namespace CSInVR
{

    public class TagSpecificButton : Button
    {
        public override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
                base.OnTriggerEnter(other);
        }

        public override void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
                base.OnTriggerExit(other);
        }
    }
}
