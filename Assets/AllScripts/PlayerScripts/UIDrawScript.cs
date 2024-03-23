using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class UIDrawScript : MonoBehaviour
{
    [SerializeField] public PlayerStats Stat;
    [SerializeField] public Caster PCaster;
    [SerializeField] public TextMeshProUGUI HealthText;
    [SerializeField] public TextMeshProUGUI ManaText;
    [SerializeField] public RectTransform HealthvalueRect;
    [SerializeField] public RectTransform ManavalueRect;
    [Header("SkillsPanel")]
    [SerializeField] GameObject FSkillOnCooldownUI;


    void Start()
    {
        Stat = GetComponentInParent<PlayerStats>();
        if (Stat == null)
            Debug.LogError("Player Stats not found");
        PCaster = GetComponentInParent<Caster>();
        if (PCaster == null)
            Debug.LogError("Caster not found");
    }

    // Update is called once per frame
    void Update()
    {
        DrawUI();
        FSkillOnCooldown(PCaster.FSkillonCooldown);
    }

    void DrawUI()
    {
        HealthText.SetText(Stat.Health.ToString()+"/"+Stat.MaxHealth);
        HealthvalueRect.anchorMax = new Vector2(Stat.Health / Stat.MaxHealth, 1);
        ManaText.SetText(Stat.Mana.ToString()+"/"+Stat.MaxMana);
        ManavalueRect.anchorMax = new Vector2(Stat.Mana/ Stat.MaxMana, 1);
        
    }

    public void FSkillOnCooldown(bool onCD)
    {
        if (onCD)
        {
            FSkillOnCooldownUI.SetActive(onCD);
        }
        else
        {
            FSkillOnCooldownUI.SetActive(onCD);
        }

    }
}
