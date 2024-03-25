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
        _Animator.Casting(true);
        yield return new WaitForSeconds(TimeToCast);
        Debug.Log("Couruntine over");
        UseSkill();
        CastingIsStarted = false;
        _Agent.isStopped = false;
        _Animator.Casting(false);


    }

    void UseSkill()
    {

        Debug.Log("FIRE");
        var Skill = Instantiate(Spell, _SourcePoint.position, _SourcePoint.rotation);

        // Определяем направление к игроку
        Vector3 direction = (Player.transform.position - _SourcePoint.position).normalized;

        // Добавляем некоторое смещение к направлению (например, вверх по Y оси)
        float yOffset = 0.1f; // Настройте значение смещения по вашему усмотрению
        direction += Vector3.up * yOffset;

        // Устанавливаем направление объекта Fireball
        Skill.transform.rotation = Quaternion.LookRotation(direction);
    }

    
}
