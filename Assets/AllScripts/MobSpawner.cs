using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject MobPrefab;
    
    
    void Start()
    {
        
    }


    public void SpawnMobs(int MobCount)
    {
        Vector3 pos = transform.position;
        for (int i = 0; i < MobCount; i++)
        {
            var Mob = Instantiate(MobPrefab,pos,Quaternion.identity);
            gameManager.MobsInAction.Add(Mob); 
        }
    }


    void Update()
    {
        
    }
}
