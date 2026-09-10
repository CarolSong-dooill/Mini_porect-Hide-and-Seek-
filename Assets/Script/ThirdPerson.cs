using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyCameraMove
{
 
    private float _thirdLookAtHeight = 1.8f;
    private Vector3 _thirdCameraOffset = new Vector3(0, 2.0f, -3.0f);
    private float _thirdSharpness = 30f;



    private void InitThirdPose(bool snap)
    {
        _yaw= _playerObject.transform.eulerAngles.y;
        _pitch = 12.0f;

        Vector3 desiredPos;
        Quaternion desiredRot;

        ThirdPose(out desiredPos, out desiredRot);

        ApplyPose(desiredPos, desiredRot, _thirdSharpness, snap);

    }

    private void FollowThirdPose()
    {
        Vector3 desiredPos;
        Quaternion desiredRot;

        ThirdPose(out desiredPos, out desiredRot);

        ApplyPose(desiredPos, desiredRot, _thirdSharpness, false);




    }

    private void ThirdPose(out Vector3 desiredPos, out Quaternion desiredRot)
    {
        Quaternion orbitRot = Quaternion.Euler(_pitch, _yaw, 0f);

        desiredPos = _playerObject.transform.position + (orbitRot * _thirdCameraOffset);

        Vector3 lookPos = _playerObject.transform.position + Vector3.up * _thirdLookAtHeight;
        desiredRot = Quaternion.LookRotation(lookPos - desiredPos, Vector3.up);
    }



}
