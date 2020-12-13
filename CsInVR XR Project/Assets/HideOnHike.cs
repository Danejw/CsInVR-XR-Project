using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    public class HideOnHike : MonoBehaviour
    {
        private void OnEnable()
        {
            HikeBall.onHike += Hide;
        }

        private void OnDisable()
        {
            HikeBall.onHike -= Hide;

        }

        private void Hide()
        {
            if (gameObject.activeSelf) gameObject.SetActive(false);
        }

    }
}
