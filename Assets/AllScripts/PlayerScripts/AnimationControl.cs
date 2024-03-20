using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] public Animator _Animator;
    [SerializeField] public Movement _Movement;
    [SerializeField] public int Moving = 0;
    
    void Start()
    {
        _Animator = GetComponent<Animator>();
        if (_Animator == null)
            Debug.LogError("Animator not found");
        _Movement = GetComponentInParent<Movement>();
        if (_Movement == null)
            Debug.LogError("Movement Component not found");
    }

    void Update()
    {
        AnimatorControl();
    }

    public void AnimatorControl()
    {
        _Animator.SetInteger("Run", Moving);
    }
}
