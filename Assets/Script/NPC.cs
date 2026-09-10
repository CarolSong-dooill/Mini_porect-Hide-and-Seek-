using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _hideNpc;
    [SerializeField] private Camera _nCamera;
    [SerializeField] private float _hp = 10f;
    [SerializeField] private int _spawnCount = 5;
    [SerializeField] private GameObject[] _posePrefab;



    private Spawn _spawnPoint;




     
    private void Start()
    {
     
        _spawnPoint = new Spawn();
        _spawnPoint.SetSpawnPoint();
        _spawnPoint.ShuffleSpawnPoint();
        _spawnPoint.SpawnRandomNpc(_spawnCount, _posePrefab);

        if (_nCamera == null)
        {
            GameObject camObj = GameObject.FindWithTag("nCamera");
            if (camObj != null)
            {
                _nCamera = camObj.GetComponent<Camera>();
            }

        }

        if (_nCamera == null)
        {
            Debug.Log("nCamera 태그를 가진 카메라 객체가 없음.");
            return;
        }


    }

    private void Update()
    {




    }

    private void DamagedNpc(float damage)
    {
        _hp -= damage;
        
        if(_hp <= 0)
        {
            _hp = 0;
            gameObject.SetActive(false);
        }
        
    } 

    public void LookAtPlayer()
    {
        if (_player == null)
        {
            Debug.Log(" 술래가 바라볼 플레이어가 없음.");
            return;
        }

        Vector3 viewPlayer = (_player.transform.position - _nCamera.transform.position);

        viewPlayer.y = 0.0f;

        _nCamera.transform.rotation = Quaternion.LookRotation(viewPlayer, Vector3.up);

    }


}
