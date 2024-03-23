using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] public Animator _Animator;
    [SerializeField] public Movement _Movement;
    [SerializeField] public Caster _Caster;
    [SerializeField] public int Moving = 0;
    
    void Start()
    {
        _Animator = GetComponent<Animator>();
        if (_Animator == null)
            Debug.LogError("Animator not found");
        _Movement = GetComponentInParent<Movement>();
        if (_Movement == null)
            Debug.LogError("Movement Component not found");
        _Caster = GetComponentInParent<Caster>();
        if (_Caster == null)
            Debug.LogError("Caster Component not found");
    }

    void Update()
    {
        AnimatorControl();
    }

    public void AnimatorControl()
    {
        _Animator.SetInteger("Run", Moving);
        _Animator.SetBool("Cast", _Caster.FSkillisCasting);
        _Animator.SetBool("QSkillCast", _Caster.QSkillisCasting);
        
    }

    public void OnFSkillAnimEnd()
    {
        _Caster.UseFSkill();
    }
}
