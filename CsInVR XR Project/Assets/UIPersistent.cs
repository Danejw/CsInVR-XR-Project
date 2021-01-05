using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR
{
    public class UIPersistent : Singleton<UIPersistent>
    {
        public void ToggleUI()
        {
            if (gameObject.activeSelf) gameObject.SetActive(false);
            else gameObject.SetActive(true);
        }
    }
}
