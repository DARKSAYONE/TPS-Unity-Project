using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    [Header("Core")]
    [SerializeField] public NavMeshAgent _Agent;
    [SerializeField] public MobStatLogical Stat;
    [SerializeField] public GameObject Player;
    [SerializeField] public AIAnimator _Animator;
    [Header("For Logic")]
    [SerializeField] public float _DistanceToPlayer;
    [SerializeField] public float AttackRange = 10f;
    void Start()
    {
        _Agent = GetComponent<NavMeshAgent>();
        if (_Agent == null)
            Debug.Log("NavmesshAgent not found");
        Stat = GetComponent<MobStatLogical>();
        if (Stat == null)
            Debug.Log("MobStat not found");
        Player = GameObject.FindWithTag("Player");
        _Animator = GetComponentInChildren<AIAnimator>();
        if (_Animator == null)
            Debug.LogError("AIAnimator not found");
    }

    void FixedUpdate()
    {
        if(Stat.isAlive)
        {
            _DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            CheckRange(_DistanceToPlayer);
        }
        else
        {
            Stat.Death();
        }
    }

    void CheckRange(float Distance)
    {
        if(Distance > AttackRange)
        {
            GoToPlayer();
            _Animator.CheckAnimStat(true);
        }
        else if(Distance < AttackRange)
        {
            AttackPlayer();
            _Animator.CheckAnimStat(false);
        }
    }

    void GoToPlayer()
    {
        _Agent.isStopped = false;
        _Agent.SetDestination(Player.transform.position);
    }
    
    void AttackPlayer()
    {
        _Agent.isStopped = true;
    }

    
}
