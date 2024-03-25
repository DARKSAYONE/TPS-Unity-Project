using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimator : MonoBehaviour
{
    [SerializeField] public Animator _Anim;
    [SerializeField] public MobStatLogical Stat;
    void Start()
    {
        _Anim = GetComponent<Animator>();
        Stat = GetComponentInParent<MobStatLogical>();
    }

    public void CheckAnimStat(bool State)
    {
        _Anim.SetBool("Run", State);
    }

    public void Casting(bool State)
    {
        _Anim.SetBool("Casting", State);
    }
    void FixedUpdate()
    {
 
    }
}
