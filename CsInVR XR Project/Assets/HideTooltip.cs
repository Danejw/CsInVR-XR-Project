using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    public class HideTooltip : MonoBehaviour
    {
        public void Hide()
        {
            if (gameObject.activeSelf) gameObject.SetActive(false);
        }

        public void Show()
        {
            if (!gameObject.activeSelf) gameObject.SetActive(true);
        }
    }
}
