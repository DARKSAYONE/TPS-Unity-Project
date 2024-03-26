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
    [SerializeField] public TextMeshProUGUI EXPText;
    [SerializeField] public RectTransform EXPValueRect;
    [SerializeField] public TextMeshProUGUI LevelText;
    [Header("SkillsPanel")]
    [SerializeField] GameObject FSkillOnCooldownUI;
    [SerializeField] GameObject QSkillPanel;
    [SerializeField] GameObject QSkillOnCoolDownUI;
    [SerializeField] GameObject ESkillPanel;
    [SerializeField] GameObject ESkillOnCoolDownUI;
    [Header("GameOverUI")]
    [SerializeField] public GameObject GameOverUI;
    [Header("All UI objects")]
    [SerializeField] public GameObject SkillPanel;
    [SerializeField] public GameObject StatsPanel;
    [SerializeField] public GameObject FSkillPanel;



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
        QSkillUI(PCaster.PlayerGetQSkill);
        ESkillUI(PCaster.PlayerGetESkill);
    }

    void DrawUI()
    {
        HealthText.SetText(Stat.Health.ToString()+"/"+Stat.MaxHealth);
        HealthvalueRect.anchorMax = new Vector2(Stat.Health / Stat.MaxHealth, 1);
        ManaText.SetText(Stat.Mana.ToString()+"/"+Stat.MaxMana);
        ManavalueRect.anchorMax = new Vector2(Stat.Mana/ Stat.MaxMana, 1);
        EXPText.SetText(Stat.EXP.ToString() + "/" + Stat.EXPForLevel);
        EXPValueRect.anchorMax = new Vector2(Stat.EXP / Stat.EXPForLevel, 1);
        LevelText.SetText(Stat.Level.ToString());
        
    }

    public void FSkillOnCooldown(bool onCD)
    {
        FSkillOnCooldownUI.SetActive(onCD);
    }

    public void QSkillUI(bool State)
    {
        QSkillPanel.SetActive(State);
        QSkillOnCoolDownUI.SetActive(PCaster.QSkillOnCooldown);
    }

    public void ESkillUI(bool State)
    {
        ESkillPanel.SetActive(State);
        ESkillOnCoolDownUI.SetActive(PCaster.ESkillOnCooldown);
    }

    public void TurnOfAllPlayUI()
    {
        SkillPanel.SetActive(false);
        StatsPanel.SetActive(false);
        FSkillPanel.SetActive(false);
        QSkillPanel.SetActive(false);
        ESkillPanel.SetActive(false);
    }
}
