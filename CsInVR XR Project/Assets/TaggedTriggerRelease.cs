using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

namespace CSInVR
{
    public class TaggedTriggerRelease : TriggerRelease
    {
        public string tagName;

        public override void Update()
        {
            if (grabber.HeldGrabbable && grabber.HeldGrabbable.tag == tagName)
                base.Update();
        }
    }
}
