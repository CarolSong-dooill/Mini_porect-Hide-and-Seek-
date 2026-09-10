using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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



    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        _personCamera = _camera.GetComponent<Transform>();


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








