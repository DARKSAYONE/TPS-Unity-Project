using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{

    [SerializeField] public GameObject ShopMenu;
    [SerializeField] public bool ShopOpen = false;
    [SerializeField] public Caster PlayerCaster;
    [SerializeField] public PlayerStats PlayerStats;
    [SerializeField] public Movement _Move;
    [SerializeField] public CameraControl _CameraControl;
    [SerializeField] public TextMeshProUGUI BuyPointsUI;
    [SerializeField] private GameObject KeyUI;
    [Header("FireballUpgrade")]
    [SerializeField] public int FireballUpgradeCost = 1;
    [SerializeField] public GameObject FireballUpgradeButton;
    [Header("QSkill")]
    [SerializeField] public int QSkillCost = 1;
    [SerializeField] public GameObject QSkillButton;
    [Header("QSkill")]
    [SerializeField] public int ESkillCost = 1;
    [SerializeField] public GameObject ESkillButton;
    [Header("HealthUpgrade")]
    [SerializeField] public int HealthUpgradeCost = 2;
    [Header("ManaUpgrade")]
    [SerializeField] public int ManaUpgradeCost = 1;

    void Start()
    {
        
    }

    
    void Update()
    {
        BuyPointsUI.SetText("Buypoints: " + PlayerStats.BuyPoints);
    }


    private void OnTriggerStay(Collider other)
    {
        
        if(ShopOpen)
            KeyUI.SetActive(false);
        else if(!ShopOpen)
            KeyUI.SetActive(true);

        if (Input.GetKeyDown(KeyCode.M))
        {
            if(!ShopOpen)
            {
                Cursor.visible = true;
                
                ShopMenu.SetActive(true);
                ShopOpen = true;
                PlayerCaster.CanCast = false;
                _Move.CanMove = false;
                _CameraControl.CanMove = false;
                

            }
            else
            {
                ShopMenu.SetActive(false);
                ShopOpen = false;
                Cursor.visible = false;
                PlayerCaster.CanCast = true;
                _Move.CanMove = true;
                _CameraControl.CanMove= true;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        KeyUI.SetActive(false);
    }

    public void BuyUprgradeForFireball()
    {
        var _BuyPoints = PlayerStats.BuyPoints;
        if(_BuyPoints >= FireballUpgradeCost)
        {
            FireballUpgradeButton.SetActive(false);
            PlayerCaster.FSkillDamageMod = 2;
            PlayerStats.BuyPoints -= 1;
        }
        else
        {
            Debug.Log("Dont enough money");
        }
    }

    public void BuyQSkill()
    {
        if(PlayerStats.BuyPoints >=  QSkillCost)
        {
            QSkillButton.SetActive(false);
            PlayerCaster.PlayerGetQSkill = true;
            PlayerStats.BuyPoints -= 1;
        }
        else
        {
            Debug.Log("Dont enough money");
        }
    }

    public void BuyESkill()
    {
        if (PlayerStats.BuyPoints >= QSkillCost)
        {
            ESkillButton.SetActive(false);
            PlayerCaster.PlayerGetESkill = true;
            PlayerStats.BuyPoints -= 1;
        }
        else
        {
            Debug.Log("Dont enough money");
        }
    }

    public void BuyHealthUpgrade()
    {
        if (PlayerStats.BuyPoints >= HealthUpgradeCost)
        {
            PlayerStats.MaxHealth = PlayerStats.MaxHealth + 100;
            PlayerStats.BuyPoints -= HealthUpgradeCost;
        }
        else
        {
            Debug.Log("Dont enough money");
        }
    }

    public void BuyManaUpgrade()
    {
        if (PlayerStats.BuyPoints >= ManaUpgradeCost)
        {
            PlayerStats.MaxMana = PlayerStats.MaxMana + 100;
            PlayerStats.BuyPoints -= ManaUpgradeCost;
        }
        else
        {
            Debug.Log("Dont enough money");
        }
    }
}
