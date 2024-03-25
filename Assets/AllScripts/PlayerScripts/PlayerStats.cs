using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{

    [Header("Mod")]
    [SerializeField] public float ModMaxHealth = 1;
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
    [SerializeField] public float EXP;
    [SerializeField] public float EXPForLevel;
    [SerializeField] public int BuyPoints = 0;
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
        ManaHeal();
        LevelUP();
        if (Health <= 0)
            isAlive = false;
                
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

    public void LevelUP()
    {
        if(EXP >= EXPForLevel)
        {
            ApplyMod();
            Level++;
            EXP = 0;
            PowerForce = PowerForce + (0.1f);
            ModMaxHealth = ModMaxHealth + (0.2f);
        }
    }

    public void GetEXP(float _EXP)
    {
        EXP = _EXP + EXP;
    }

    public void ReloadScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
        Time.timeScale = 1.0f;
    }
}
