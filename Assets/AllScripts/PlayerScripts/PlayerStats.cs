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
    [SerializeField] public float ModMaxMana = 1;
    [Header("Main Stats")]
    [SerializeField] public float Health;
    [SerializeField] public float MaxHealth;
    [SerializeField] public float MaxMana;
    [SerializeField] public float Mana;
    [SerializeField] public float ManaRegen = 10.0f;
    [SerializeField] public int Level;
    [SerializeField] public float EXP;
    [SerializeField] public float EXPForLevel;
    [SerializeField] public int BuyPoints = 0;
    [SerializeField] public float MoveSpeed;
    [SerializeField] public float JumpForce;
    [SerializeField] public float PowerForce;
    [SerializeField] public bool isAlive = true;
    [Header("Other")]
    [SerializeField] public PlayerAudioScript Audio;
    [SerializeField] private bool ManaTimerStart = false;
    

    void Start()
    {
        Health = MaxHealth;
        Mana = MaxMana;
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
        Audio.GetDamaged();
    }

   
    void FixedUpdate()
    {
        if (Mana < MaxMana && !ManaTimerStart)
            StartCoroutine(ManaHeal());
        LevelUP();
        if (Health <= 0)
            isAlive = false;
                
    }

    public void ApplyMod()
    {
        
    }

    public IEnumerator ManaHeal()
    {
        ManaTimerStart = true;
        yield return new WaitForSeconds(0.5f);
        Mana = Mana + ManaRegen;
        ManaTimerStart = false;
    }

    public void LevelUP()
    {
        if(EXP >= EXPForLevel)
        {
            ApplyMod();
            Level++;
            EXP = 0;
            PowerForce = PowerForce + (0.4f);
            MaxHealth = MaxHealth + (Level * 8);
            EXPForLevel = EXPForLevel + (Level * 30);
            MaxMana = MaxMana + (Level * 10);
            BuyPoints++;
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

    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
}
