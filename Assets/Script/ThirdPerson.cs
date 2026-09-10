using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyCameraMove
{
 
    private float _thirdLookAtHeight = 1.8f;
    private Vector3 _thirdCameraOffset = new Vector3(0, 2.0f, -3.0f);

    private float _thirdSharpness = 15f;

    [Header("¿Àºø ¿É¼Ç")]

    private bool _useOrbit = true;

    private float _orbitShrpness = 5.0f;

    private float _orbitPitchMin = -10.0f;
    private float _orbitPitchMax = 25.0f;

    private float _orbitYaw;
    private float _orbitPitch;


    private void InitThirdPose(bool snap)
    {
        _orbitYaw = _playerObject.transform.eulerAngles.y;
        _orbitPitch = 12.0f;

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
        if (_useOrbit && Input.GetMouseButton(1))
        {
            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");

            _orbitYaw = mx * _orbitShrpness;
            _orbitPitch = my * _orbitShrpness;

            _orbitPitch = Mathf.Clamp(_orbitPitch, _orbitPitchMin, _orbitPitchMax);

        }

        if (_useOrbit)
        {
            Quaternion orbitRot = Quaternion.Euler(_orbitPitch, _orbitYaw, 0f);

            desiredPos = _playerObject.transform.position + (orbitRot * _thirdCameraOffset);

            Vector3 lookPos = _playerObject.transform.position + Vector3.up * _thirdLookAtHeight;

            desiredRot = Quaternion.LookRotation(lookPos - desiredPos, Vector3.up);
        }

        else
        {
            desiredPos = _playerObject.transform.position + (_player.transform.rotation * _thirdCameraOffset);

            Vector3 lookPos = _playerObject.transform.position + Vector3.up * _thirdLookAtHeight;
            desiredRot = Quaternion.LookRotation(lookPos - desiredPos, Vector3.up);


        }

    }



}
