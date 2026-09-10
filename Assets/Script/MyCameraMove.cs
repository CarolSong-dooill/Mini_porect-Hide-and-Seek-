using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public partial class MyCameraMove : MonoBehaviour
{
    public enum ECamera
    {
        FirstPerson,
        ThirdPerson,
        
    }


    [SerializeField] private ECamera _ecameraMode = ECamera.FirstPerson;
    [SerializeField] private bool _chageMode = true;
    [SerializeField] private Transform _camera;
    

    [SerializeField] GameObject _playerObject;


    private Transform _personCamera;
    private Player _player;
    private ECamera _mode;

    [Header("마우스 회전 시점")]
    [SerializeField] private float _mouseSensitivity = 2.0f;
    [SerializeField] private float _pitchMin = -30.0f;
    [SerializeField] private float _pitchMax = 70.0f;

    private float _yaw;
    private float _pitch;


    private void Start()
    {
        if(_playerObject == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        }
        if(_playerObject != null)
        {
            _player = _playerObject.GetComponent<Player>();
            _yaw = _playerObject.transform.eulerAngles.y;
        }

        if(_camera != null)
        {
            _personCamera = _camera.GetComponent<Transform>();

        }

        SetMode(_ecameraMode, true);


    }


    private void Update()
    {
        

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            SetMode(ECamera.FirstPerson,_chageMode);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
         
            SetMode(ECamera.ThirdPerson, _chageMode);
        }

        MouseRotation();
        
    }

    private void LateUpdate()
    {
        if(_camera == null || _player == null)
        {
            return;
        }

        switch (_mode)
        {
            case ECamera.FirstPerson:
                FollowFirstPose();
                break;


            case ECamera.ThirdPerson:
                FollowThirdPose();
                break;
         
        }

    }


    private void MouseRotation()
    {
        if(!Input.GetMouseButton(1))
        {
            return;
        }

        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        _yaw += mx * _mouseSensitivity;
        _pitch -= my * _mouseSensitivity;
        _pitch = Mathf.Clamp(_pitch, _pitchMin, _pitchMax);

    }



    private void ApplyPose(Vector3 desiredPos, Quaternion desiredRot, float sharpness, bool snap)
    {
        if (snap)
        {
            _personCamera.position = desiredPos;
            _personCamera.rotation = desiredRot;
        }

        float t = 1f - Mathf.Exp(-sharpness * Time.deltaTime);

        _personCamera.position = Vector3.Lerp(_personCamera.position, desiredPos, t);
        _personCamera.rotation = Quaternion.Slerp(_personCamera.rotation, desiredRot, t);


    }



    private void SetMode(ECamera MODE, bool snap)
    {
        _mode = MODE;

        if (_ecameraMode == _mode)
        {
            return;
        }



        Debug.Log($"카메라 모드 설정{_mode}");

        switch (_mode)
        {
            case ECamera.FirstPerson:
                
                _ecameraMode = ECamera.FirstPerson;

                InitFirstPose(snap);


                break;
           
            case ECamera.ThirdPerson:

                _ecameraMode = ECamera.ThirdPerson;

                InitThirdPose(snap);
                break;
           
        }

    }

}








