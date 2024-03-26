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
    [SerializeField] public PlayerAudioScript _Audio;
    [SerializeField] public GameObject CastingEffect;
    [Header("First Skill")]
    [SerializeField] public GameObject FSkill;
    [SerializeField] public float FSkillManaCost;
    [SerializeField] public float FSkillCooldownTime;
    [SerializeField] public bool FSkillonCooldown = false;
    [SerializeField] public bool FSkillisCasting = false;
    [SerializeField] public float FSkillTimeToCast = 5.0f;
    [SerializeField] public bool FSkillAnimCompele = false;
    [Header("Q Skill")]
    [SerializeField] public bool PlayerGetQSkill = true;
    [SerializeField] public GameObject QSkill;
    [SerializeField] public float QSkillManaCost;
    [SerializeField] public float QSkillCooldownTime;
    [SerializeField] public bool QSkillOnCooldown = false;
    [SerializeField] public bool QSkillisCasting = false;
    [SerializeField] public bool QSkillAnimComplete = false;
    [SerializeField] public float QSkillTimeToCast = 3.0f;
    [Header("E Skill")]
    [SerializeField] public bool PlayerGetESkill = true;
    [SerializeField] public GameObject ESkill;
    [SerializeField] public float ESkillManaCost;
    [SerializeField] public float ESkillCooldownTime;
    [SerializeField] public bool ESkillOnCooldown = false;
    [SerializeField] public bool ESkillisCasting = false;
    [SerializeField] public bool ESkillAnimComplete = false;
    [SerializeField] public float ESkillTimeToCast = 5.0f;


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
        if (Stats.isAlive)
        {

            if (Input.GetMouseButtonDown(0))
            {
                CastFSkill();
            }
           
            if (Input.GetKey(KeyCode.Q) && PlayerGetQSkill)
            {
                CastQSkill();
            }
            if(Input.GetKey(KeyCode.E) && PlayerGetESkill)
            {
                CastESkill();
            }
            
        }

        if(isCasting)
        {
            CastingEffect.SetActive(true);
        }
        else
        {
            CastingEffect.SetActive(false);
        }

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
    //Legacy
    /*void FSkillAnimCastCheck()
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
    */
    void CastFSkill()
    {
        AnimatorStateInfo animInfo = _AnimControl._Animator.GetCurrentAnimatorStateInfo(0);

        if (!FSkillonCooldown && Stats.Mana >= FSkillManaCost && !FSkillisCasting && !isCasting)
        {
            StartCoroutine(CoroutineCastFSkill());
            isCasting = true;
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
    public IEnumerator CoroutineCastFSkill()
    {
        FSkillisCasting = true;
        _Movement.CanMove = false;
        Stats.Mana -= FSkillManaCost;
        _Audio.AudioFireballCastingSound();
        yield return new WaitForSeconds(FSkillTimeToCast);
        UseFSkill();
        FSkillisCasting = false;
        _Movement.CanMove = true;
        FSkillonCooldown = true;
        isCasting = false;
        StartCoroutine(FSkillCooldownTimer());
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
    //LEGACY
    /*void QSkillAnimCastCheck()
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
    */
    void CastQSkill()
    {
        AnimatorStateInfo animInfo = _AnimControl._Animator.GetCurrentAnimatorStateInfo(0);
        if(!QSkillOnCooldown && Stats.Mana >= QSkillManaCost && !QSkillisCasting && !isCasting)
        {
            StartCoroutine(CoroutineCastQSkill());
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
    public IEnumerator CoroutineCastQSkill()
    {
        _AnimControl._Animator.Play("GrenadeCast", 0, 0);
        isCasting = true;
        Stats.Mana -= QSkillManaCost;
        _Movement.CanMove = false;
        QSkillisCasting = true;
        yield return new WaitForSeconds(QSkillTimeToCast);
        QSkillisCasting = false;
        _Movement.CanMove = true;
        isCasting = false;
        UseQSkill();
        QSkillOnCooldown = true;
        StartCoroutine(QSKillCooldownTimer());

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

    //------------------------------------------------<E SKILL LOGIC>----------------------------------------------------------------------------

    void CastESkill()
    {
        if (!ESkillOnCooldown && Stats.Mana >= ESkillManaCost && !ESkillisCasting && !isCasting)
        {
            StartCoroutine(CoroutineCastQSkill());
        }
        else if (ESkillOnCooldown)
        {
            Debug.Log("Spell on KD");
        }
        else if (!ESkillOnCooldown && Stats.Mana < ESkillManaCost)
        {
            Debug.Log("No mana");
        }
    }
}
