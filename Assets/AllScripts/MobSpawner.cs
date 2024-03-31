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
            var MobMaxHealth = Mob.GetComponent<MobStatLogical>().MaxHealth;
            var MobCurrentHealth = Mob.GetComponent<MobStatLogical>().Health;
            Mob.GetComponent<MobStatLogical>().MaxHealth = MobMaxHealth + (gameManager.RoundCounter * 8.0f);
            Mob.GetComponent<MobStatLogical>().Health = Mob.GetComponent<MobStatLogical>().MaxHealth;
            gameManager.MobsInAction.Add(Mob); 
        }
    }


    void Update()
    {
        
    }
}
