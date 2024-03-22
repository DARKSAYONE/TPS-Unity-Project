using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Caster : MonoBehaviour
{
    [Header("Core")]
    [SerializeField] public PlayerStats Stats;
    [SerializeField] public Movement _Movement;
    [SerializeField] public AnimationControl _AnimControl;
    [SerializeField] public Transform SourcePoint;
    [SerializeField] public bool isCasting = false;
    [Header("First Skill")]
    [SerializeField] public GameObject FSkill;
    [SerializeField] public float FSkillManaCost;
    [SerializeField] public float FSkillCooldownTime;
    [SerializeField] public bool FSkillonCooldown = false;
    [SerializeField] public bool FSkillisCasting = false;
    [SerializeField] public bool FSkillAnimCompele = false;


    void Start()
    {
        InitComponent();
    }

    void InitComponent()
    {
        Stats = GetComponent<PlayerStats>();
        _Movement = GetComponent<Movement>();
        _AnimControl = GetComponentInChildren<AnimationControl>();
        
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            CastFSkill();
        

        if(FSkillisCasting)
            FSkillAnimCastCheck();
       
    }

    //------------------------------------------------<FIRST SKILL LOGIC>----------------------------------------------------------------------------
    
    void FSkillAnimCastCheck()
    {
        AnimatorStateInfo animInfo = _AnimControl._Animator.GetCurrentAnimatorStateInfo(0);
        if (animInfo.normalizedTime >= 1.0f)
        {
            FSkillisCasting = false;
            _Movement.CanMove = true;
            isCasting = false;
            UseFSkill();
        }
    }
    void CastFSkill()
    {
        
        if (!FSkillonCooldown && Stats.Mana >= FSkillManaCost && !FSkillisCasting && !isCasting)
        {
            isCasting = true;
            FSkillisCasting = true;
            _Movement.CanMove = false;
            Stats.Mana -= FSkillManaCost;
           
        }
        else if(FSkillonCooldown)
        {
           Debug.Log("Spell on KD");
        }
        else if(!FSkillonCooldown && Stats.Mana < FSkillManaCost)
        {
            Debug.Log("No mana");

        }
        
    }
    void UseFSkill()
    {
        Debug.Log("CAST");
        Instantiate(FSkill, SourcePoint.position, transform.rotation);
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------
}
