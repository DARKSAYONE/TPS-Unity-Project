using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [Header("Mod")]
    [SerializeField] public int ModMaxHealth = 1;
    [SerializeField] public int ModMoveSpeed = 1;
    [SerializeField] public float ModJumpForce = 1;
    [SerializeField] public float ModPowerForce = 1;
    [Header("Main Stats")]
    [SerializeField] public float Health;
    [SerializeField] public float MaxHealth;
    [SerializeField] public float MaxMana;
    [SerializeField] public float Mana;
    [SerializeField] public float ManaRegen = 0.001f;
    [SerializeField] public int Level;
    [SerializeField] public int EXP;
    [SerializeField] public int EXPForLevel;
    [SerializeField] public float MoveSpeed;
    [SerializeField] public float JumpForce;
    [SerializeField] public float PowerForce;
    [SerializeField] public bool isAlive = true;
    

    void Start()
    {
        Health = MaxHealth;
        Mana = MaxMana;
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

   
    void FixedUpdate()
    {
        ApplyMod();
        ManaHeal();
    }

    public void ApplyMod()
    {
        MaxHealth = MaxHealth * ModMaxHealth;
        MoveSpeed = MoveSpeed * ModMoveSpeed;
    }

    public void ManaHeal()
    {
        if(Mana < MaxMana)
        {
            Mana = Mana + ManaRegen;
        }
    }
}
