using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted,
    }

   [SerializeField] private Mode mode;

   private void LateUpdate() 
   {
        switch(mode)
        {
            case Mode.LookAt:
                transform.LookAt(Player.Instance.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirCamera = transform.position - Player.Instance.transform.position;
                transform.LookAt(dirCamera + transform.position);
                break;
            case Mode.CameraForward:
                transform.forward = Player.Instance.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Player.Instance.transform.forward;
                break;
        }
   }
}
