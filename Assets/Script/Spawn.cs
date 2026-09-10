using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn
{

   

  
    public List<GameObject> _hidepoint = new List<GameObject>();



    public void SetSpawnPoint()
    {
        _hidepoint.Clear();
        GameObject[] _arrNpc = GameObject.FindGameObjectsWithTag("HidePoint");

        for (int i = 0; i < _arrNpc.Length; i++)
        {
            _hidepoint.Add(_arrNpc[i]);

        }

    }
    
    public void ShuffleSpawnPoint()
    { 
        for(int i = _hidepoint.Count - 1; i >= 0; i--)
        {
            int j = Random.Range(0,_hidepoint.Count);
            GameObject Index = _hidepoint[j];
            _hidepoint[j] = _hidepoint[i];
            _hidepoint[i] = Index;

        }

       
    }

    public void SpawnRandomNpc(int k, GameObject[] posePrefab)
    {

        if(posePrefab == null || posePrefab.Length == 0)
        {
            Debug.Log("포즈 프리팹이 비어 있음");
            return;

        }

        

        for(int i = 0; i < k; i++)
        {

            int randomPose = Random.Range(0, posePrefab.Length);

            GameObject selectedPose = posePrefab[randomPose];

            Transform hideNpcTr = _hidepoint[i].transform;
            Object.Instantiate(selectedPose, hideNpcTr.position, hideNpcTr.rotation);


        }


    }
}
