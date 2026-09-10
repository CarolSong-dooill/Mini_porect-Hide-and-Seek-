using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyCameraMove 
{
    
    private Vector3 _firstCameraOffset = new Vector3(0f, 1.6f, 0.2f);
    private bool _usefirstViewrotation = true;


    private void InitFirstPose(bool snap)
    {
        Vector3 desiredPos;
        Quaternion desiredRot;

        desiredPos = _playerObject.transform.position + (_playerObject.transform.rotation * _firstCameraOffset);

        if (_usefirstViewrotation)
        {
            desiredRot = _playerObject.transform.rotation;
        }

        else
        {
            desiredRot = _camera.rotation;
        }

        ApplyPose(desiredPos, desiredRot, 15f, snap);
    }

    private void FollowFirstPose()
    {
        Vector3 desiredPos;
        Quaternion desiredRot;

        desiredPos = _camera.position + (_playerObject.transform.rotation * _firstCameraOffset);

        if (_usefirstViewrotation)
        {
            desiredRot = _playerObject.transform.rotation;
        }

        else
        {
            desiredRot = _camera.rotation;
        }

        ApplyPose(desiredPos, desiredRot, 15f, false);



    }

    private void MouseRotate()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");


    }
}

