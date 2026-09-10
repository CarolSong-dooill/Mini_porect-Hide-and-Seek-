using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyCameraMove 
{
    
    private Vector3 _firstCameraOffset = new Vector3(0f, 1.6f, 0.2f);
    private float _fisrtSharpness = 30f;



    private void InitFirstPose(bool snap)
    {
        Vector3 desiredPos;
        Quaternion desiredRot;

        desiredPos = _playerObject.transform.position + (_playerObject.transform.rotation * _firstCameraOffset);
        desiredRot = _camera.rotation;

        FirstHeadPose(out desiredPos, out desiredRot);
        ApplyPose(desiredPos, desiredRot, _fisrtSharpness, snap);
    }

    private void FollowFirstPose()
    {
        Vector3 desiredPos;
        Quaternion desiredRot;

        desiredPos = _playerObject.transform.position + (_playerObject.transform.rotation * _firstCameraOffset);
        desiredRot = _camera.rotation;

        FirstHeadPose(out desiredPos, out desiredRot);
        ApplyPose(desiredPos, desiredRot, _fisrtSharpness, false);

    }

    private void FirstHeadPose(out Vector3 desiredPos, out Quaternion desiredRot)
    {
        desiredRot = Quaternion.Euler(_pitch, _yaw, 0f);

        desiredPos = _playerObject.transform.position + (desiredRot * _firstCameraOffset);

    }
}

