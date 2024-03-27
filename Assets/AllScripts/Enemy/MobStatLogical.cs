using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStatLogical : MonoBehaviour
{
    [Header("Core")]
    [SerializeField] public EnemyLogic EAI;
    [Header("Mod")]
    [SerializeField] public float ModMaxHealth = 1;
    [SerializeField] public float ModMoveSpeed = 1;
    [Header("Main Stats")]
    [SerializeField] public float Health;
    [SerializeField] public float MaxHealth;
    [SerializeField] public float MoveSpeed;
    [SerializeField] public bool isAlive = true;
    [SerializeField] public bool GetEXP = false;
    void Start()
    {
        ApplyMod();
        Health = MaxHealth;
        EAI = GetComponent<EnemyLogic>();
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
        PlayerStats Player = EAI.Player.GetComponent<PlayerStats>();
        if (!GetEXP)
        {
            Player.GetEXP(50);
            GetEXP = true;
        }
        EAI._Agent.isStopped = true;
        EAI._Animator._Anim.SetBool("Death", true);
        //EAI._Animator._Anim.Play("")
        var rb = GetComponent<Rigidbody>();
        Destroy(rb);
        var colider = GetComponent<Collider>();
        Destroy(colider);
    }
    void Update()
    {
        
      
    }
}
