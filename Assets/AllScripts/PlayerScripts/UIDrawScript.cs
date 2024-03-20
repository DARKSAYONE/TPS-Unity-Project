using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDrawScript : MonoBehaviour
{
    [SerializeField] public PlayerStats Stat;
    [SerializeField] public TextMeshProUGUI HealthText;
    [SerializeField] public TextMeshProUGUI ManaText;
    [SerializeField] public RectTransform HealthvalueRect;
    [SerializeField] public RectTransform ManavalueRect;


    void Start()
    {
        Stat = GetComponentInParent<PlayerStats>();
        if (Stat == null)
            Debug.LogError("Player Stats not found");
    }

    // Update is called once per frame
    void Update()
    {
        DrawUI();
    }

    void DrawUI()
    {
        HealthText.SetText(Stat.Health.ToString()+"/"+Stat.MaxHealth);
        HealthvalueRect.anchorMax = new Vector2(Stat.Health / Stat.MaxHealth, 1);
        ManaText.SetText(Stat.Mana.ToString()+"/"+Stat.MaxMana);
        ManavalueRect.anchorMax = new Vector2(Stat.Mana/ Stat.MaxMana, 1);
        
    }
}
