using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    // 룩포인트를 하나 만들어야 한다.. 이것을 어디에 넣어야 할까..?

    [SerializeField] private Transform _camera;
    [SerializeField] private Animator _anime;
    private CharacterController _controller;


    [Header("이동")]
    [SerializeField] private float _forwardSpeed = 8.0f;
    private float _runAccel = 1.8f;
    private float _rotatesharpness = 8.0f;

    [Header("점프")]
    private float _jumpHeight = 3.0f;
    private float _gravity = -9.81f;


    [Header("애니메이터 파라미터")]
    [SerializeField] private string _speed;
    [SerializeField] private string _run;
    [SerializeField] private string _jump;

    [Header("내부변수")]
    private float _zVel;
    private float _speedDamp = 0.12f;

    private int _takeSpeed;
    private int _takeJump;
    private int _takeRun;
    private int _takeAttack;

    private bool _takeRunParam;
    private bool _takeJumpParam;

    private NPC _npc;
    private List<GameObject> _npclist = new List<GameObject>();  

    private void Reset()
    {
        _controller = GetComponent<CharacterController>();
        _anime = GetComponent<Animator>();

    }


    private void Awake()
    {
        _speed = "fSpeed";
        _run = "bRun";
        _jump = "tJump";

        if (_controller == null)
        {
            _controller = GetComponent<CharacterController>();
        }

        if(_anime == null)
        {
            _anime = GetComponent<Animator>();
        }

        if(_camera == null && Camera.main != null)
        {
            _camera = Camera.main.transform;
        }

        _takeSpeed= Animator.StringToHash(_speed);
        _takeRunParam = !string.IsNullOrEmpty(_run);
        _takeJumpParam = !string.IsNullOrEmpty(_jump);

        if(_takeRunParam)
        {
            _takeRun = Animator.StringToHash(_run);
        }

        if(_takeJumpParam)
        {
            Debug.Log($"1. {_takeJump}");
            _takeJump = Animator.StringToHash(_jump);
            Debug.Log($"2. {_takeJump}");
        }

    }


    private void Start()
    {
        // NPC 리스트 받아오고.
        // 카메라 객체 퍼블릭
        // 카메라 거리 계산
        // 뎁스 올려서 보여주기.
        // 컴퓨터 카메라가 보여지는 동안 움직임 막고
        // 일정시간이 끝나면 다시 플레이어 화면보이고 플레이어 이동.

     
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(h, 0.0f, v);
        input = Vector3.ClampMagnitude(input.normalized, 1.0f);

        bool isRunkey = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool isJumpKey = Input.GetKeyDown(KeyCode.Space);
                
        Vector3 playerPos  = (input.magnitude > 0.001f) ? MoveDirection(input) : Vector3.zero;

        float runSpeed = _forwardSpeed * (isRunkey ? _runAccel : 1.0f); 
                
        bool jump = PlayerJump(isJumpKey);
        
        if(_takeJumpParam && jump)
        {
            _anime.SetTrigger(_takeJump);
            
        }

        Vector3 velocity = playerPos * runSpeed;
        velocity.y = _zVel;

        _controller.Move(velocity * Time.deltaTime);

        PlayerRotate(playerPos);

        float speed = playerPos.magnitude * (isRunkey ? 1.0f : 0.5f);
        _anime.SetFloat(_takeSpeed, speed, _speedDamp, Time.deltaTime);

        if(_takeRunParam)
        {
            _anime.SetBool(_takeRun, isRunkey && playerPos.sqrMagnitude > 0.001f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _anime.SetTrigger("tAttack");
        }



        if (Input.GetKeyDown(KeyCode.T))
        {
           //NPC.GetComponent<LookAtPlayer()>;
        }
    }





    private Vector3 MoveDirection(Vector3 playerPos)
    {
        if(_camera == null)
        {
            return playerPos.normalized;

        }

        Vector3 camF = Vector3.ProjectOnPlane(_camera.forward, Vector3.up).normalized;
        Vector3 camR = Vector3.ProjectOnPlane(_camera.right, Vector3.up).normalized;

        Vector3 dir = camF * playerPos.z + camR * playerPos.x;

        return dir;
    }

    private void PlayerRotate(Vector3 playerPos)
    {
        if(playerPos.sqrMagnitude < 0.0001f)
        {
            return;
        }

        Quaternion targetRot = Quaternion.LookRotation(playerPos, Vector3.up);

        transform.rotation = Quaternion.Slerp
            (
                transform.rotation,
                targetRot,
                1.0f - Mathf.Exp(-_rotatesharpness * Time.deltaTime)
            );

    }


    private bool PlayerJump(bool jumpkey)
    {
        bool isJump = false;
        if(_controller.isGrounded)
        {

            if(_zVel < 0.0f)
            {
                _zVel = -1.5f;
            }

            if (jumpkey)
            {
                _zVel = Mathf.Sqrt(_jumpHeight * -1.0f * _gravity);

                isJump = true;
                        
            }
        }

        _zVel += _gravity * Time.deltaTime;

        return isJump;

    }








}
