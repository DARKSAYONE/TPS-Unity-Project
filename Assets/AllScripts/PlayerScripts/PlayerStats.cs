using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [Header("Mod")]
    [SerializeField] public int ModMaxHealth = 1;
    [SerializeField] public int ModMoveSpeed = 1;
    [Header("Main Stats")]
    [SerializeField] public int Health;
    [SerializeField] public int MaxHealth;
    [SerializeField] public int Level;
    [SerializeField] public int EXP;
    [SerializeField] public int EXPForLevel;
    [SerializeField] public float MoveSpeed;
    

    void Start()
    {
        Health = MaxHealth;
    }

   
    void FixedUpdate()
    {
        ApplyMod();
    }

    public void ApplyMod()
    {
        MaxHealth = MaxHealth * ModMaxHealth;
        MoveSpeed = MoveSpeed * ModMoveSpeed;
    }
}
