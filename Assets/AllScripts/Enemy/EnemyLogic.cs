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
    [SerializeField] public Transform _SourcePoint;
    [SerializeField] public GameObject Spell;
    [Header("For Logic")]
    [SerializeField] public float _DistanceToPlayer;
    [SerializeField] public float AttackRange = 10f;
    [SerializeField] public bool Casting = false;
    [SerializeField] public bool CastingIsStarted = false;
    [SerializeField] public float TimeToCast = 5.0f;
    [SerializeField] private float turnSpeed = 500f;
    [SerializeField] private GameObject Effect;
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

    void Update()
    {
        if(Stat.isAlive)
        {
            _DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            CheckRange(_DistanceToPlayer);
            TurnToPlayer();
        }
        else
        {
            if(CastingIsStarted)
            {
                StopAllCoroutines();
            }
            Effect.SetActive(false);
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
        else if(Distance < AttackRange && !CastingIsStarted)
        {
            _Agent.isStopped = true;
            StartCoroutine(StartCast());
            _Animator.CheckAnimStat(false);
        }
    }

    void GoToPlayer()
    {
        _Agent.SetDestination(Player.transform.position);
    }
    
    public IEnumerator StartCast()
    {
        Debug.Log("Couruntine started");
        CastingIsStarted = true;
        Effect.SetActive(true);
        _Animator.Casting(true);
        yield return new WaitForSeconds(TimeToCast);
        Debug.Log("Couruntine over");
        UseSkill();
        CastingIsStarted = false;
        _Agent.isStopped = false;
        _Animator.Casting(false);
        Effect.SetActive(false);
    }

    void TurnToPlayer()
    {
        if (CastingIsStarted == true)
        {
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    void UseSkill()
    {

        Debug.Log("FIRE");
        var Skill = Instantiate(Spell, _SourcePoint.position, _SourcePoint.rotation);
        Vector3 direction = (Player.transform.position - _SourcePoint.position).normalized;
        float yOffset = 0.1f;
        direction += Vector3.up * yOffset;
        Skill.transform.rotation = Quaternion.LookRotation(direction);
    }

    
}
