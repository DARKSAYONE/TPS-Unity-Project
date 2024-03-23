using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStatLogical : MonoBehaviour
{
    [Header("Mod")]
    [SerializeField] public float ModMaxHealth = 1;
    [SerializeField] public float ModMoveSpeed = 1;
    [Header("Main Stats")]
    [SerializeField] public float Health;
    [SerializeField] public float MaxHealth;
    [SerializeField] public float MoveSpeed;
    [SerializeField] public bool isAlive = true;
    void Start()
    {
        Health = MaxHealth;
        ApplyMod();
    }


    public void ApplyMod()
    {
        MaxHealth = MaxHealth * ModMaxHealth;
        MoveSpeed = MoveSpeed * ModMoveSpeed;
    }
    


    public void TakeDamage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            isAlive = false;
        }
    }

    public void Death()
    {

    }
    void Update()
    {
        
      
    }
}
