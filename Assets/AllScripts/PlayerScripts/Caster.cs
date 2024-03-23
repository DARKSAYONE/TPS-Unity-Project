using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        {
            CastFSkill();
        }
        if(FSkillisCasting)
            FSkillAnimCastCheck();
       
    }

    Vector3 GetTargetPoint()
    {
        //ray to center of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.7f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return ray.GetPoint(100);
    }

    //------------------------------------------------<FIRST SKILL LOGIC>----------------------------------------------------------------------------

    void FSkillAnimCastCheck()
    {
        AnimatorStateInfo animInfo = _AnimControl._Animator.GetCurrentAnimatorStateInfo(0);
        
        
            
            if (animInfo.normalizedTime >= 0.6213f)
            {
                FSkillisCasting = false;
                _Movement.CanMove = true;
                isCasting = false;
                UseFSkill();
                FSkillonCooldown = true;
                StartCoroutine(FSkillCooldownTimer());
            }
        
        
    }
    void CastFSkill()
    {
        AnimatorStateInfo animInfo = _AnimControl._Animator.GetCurrentAnimatorStateInfo(0);

        if (!FSkillonCooldown && Stats.Mana >= FSkillManaCost && !FSkillisCasting && !isCasting)
        {
            if (_AnimControl._Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "SpellCast")
            {
                _AnimControl._Animator.Play("Cast", 0, 0);
                FSkillisCasting = true;
                _Movement.CanMove = false;
                isCasting = true;
                Stats.Mana -= FSkillManaCost;
                Debug.Log(_AnimControl._Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            }
            
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
    public void UseFSkill()
    {
        Debug.Log("CAST");
        var _FSkill = Instantiate(FSkill, SourcePoint.position, transform.rotation);
        _FSkill.transform.LookAt(GetTargetPoint());
        
    }

    public IEnumerator FSkillCooldownTimer()
    {
        yield return new WaitForSeconds(FSkillCooldownTime);
        FSkillonCooldown = false;
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------
}
