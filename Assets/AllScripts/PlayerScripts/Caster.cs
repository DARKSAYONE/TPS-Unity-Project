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
    [Header("Q Skill")]
    [SerializeField] public bool PlayerGetQSkill = true;
    [SerializeField] public GameObject QSkill;
    [SerializeField] public float QSkillManaCost;
    [SerializeField] public float QSkillCooldownTime;
    [SerializeField] public bool QSkillOnCooldown = false;
    [SerializeField] public bool QSkillisCasting = false;
    [SerializeField] public bool QSkillAnimComplete = false;


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
        if(Input.GetKey(KeyCode.Q) && PlayerGetQSkill)
        {
            CastQSkill();
        }
        if (QSkillisCasting)
            QSkillAnimCastCheck();
       
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
        var _FSkill = Instantiate(FSkill, SourcePoint.position, SourcePoint.rotation);
        _FSkill.transform.LookAt(GetTargetPoint());
    }

    public IEnumerator FSkillCooldownTimer()
    {
        yield return new WaitForSeconds(FSkillCooldownTime);
        FSkillonCooldown = false;
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------

    //------------------------------------------------<Q SKILL LOGIC>----------------------------------------------------------------------------

    void QSkillAnimCastCheck()
    {
        AnimatorStateInfo animInfo = _AnimControl._Animator.GetCurrentAnimatorStateInfo(0);
        if(animInfo.normalizedTime >= 0.4f)
        {
            QSkillisCasting = false;
            _Movement.CanMove = true;
            isCasting = false;
            UseQSkill();
            QSkillOnCooldown = true;
            StartCoroutine(QSKillCooldownTimer());
        }
    }
    void CastQSkill()
    {
        AnimatorStateInfo animInfo = _AnimControl._Animator.GetCurrentAnimatorStateInfo(0);
        if(!QSkillOnCooldown && Stats.Mana >= QSkillManaCost && !QSkillisCasting && !isCasting)
        {
            if (_AnimControl._Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "GrenadeCast")
            {
                _AnimControl._Animator.Play("GrenadeCast", 0, 0);
                QSkillisCasting=true;
                _Movement.CanMove = false;
                isCasting = true;
                Stats.Mana -= QSkillManaCost;
            }
        }
        else if(QSkillOnCooldown)
        {
            Debug.Log("Spell on KD");
        }
        else if(!QSkillOnCooldown && Stats.Mana < QSkillManaCost)
        {
            Debug.Log("No mana");
        }
    }
    public void UseQSkill()
    {
        Debug.Log("QCAST");
        var _QSkill = Instantiate(QSkill, SourcePoint.position, SourcePoint.rotation);
        _QSkill.transform.LookAt(GetTargetPoint());
        _QSkill.GetComponent<Rigidbody>().AddForce(SourcePoint.forward * 500);

    }
    public IEnumerator QSKillCooldownTimer()
    {
        yield return new WaitForSeconds(QSkillCooldownTime);
        QSkillOnCooldown = false;
    }

}
