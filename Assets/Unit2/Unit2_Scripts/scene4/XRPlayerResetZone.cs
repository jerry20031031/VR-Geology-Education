using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;   // <--- 必加這行

public class XRPlayerResetZone : MonoBehaviour
{
    [Header("指定安全回傳點")]
    public Transform safePoint; // 指定傳送回的位置

    private void OnTriggerEnter(Collider other)
    {
        // 尋找父物件是否有 XROrigin
        var xrOrigin = other.GetComponentInParent<XROrigin>();
        if (xrOrigin != null && safePoint != null)
        {
            // 傳送 XR Rig 到安全點
            xrOrigin.transform.position = safePoint.position;
            xrOrigin.transform.rotation = safePoint.rotation;

            // 如果有 CharacterController，需重啟
            var cc = xrOrigin.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
                cc.transform.position = safePoint.position;
                cc.enabled = true;
            }
        }
    }
}
