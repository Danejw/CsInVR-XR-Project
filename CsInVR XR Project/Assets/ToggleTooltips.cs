using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    // get the children tooltips
    // toggles the active state on or off
    public class ToggleTooltips : MonoBehaviour
    {
        private Transform[] children;


        private void Start()
        {
            children = GetComponentsInChildren<Transform>();
        }
   
        public void Toggletips()
        {
            if (children != null)
            {
                foreach (Transform child in children)
                {
                    if (child.gameObject.activeSelf)
                        child.gameObject.SetActive(false);
                    else if (!child.gameObject.activeSelf)
                        child.gameObject.SetActive(true);
                }
            }
        }
    }
}
