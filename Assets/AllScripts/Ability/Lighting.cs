using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    [SerializeField] private float lifeTime = 10.0f;
    [SerializeField] public float SpellBaseDamage = 30.0f;
    [SerializeField] public GameObject Player;
    [SerializeField] public PlayerStats _PlayerStats;
    [SerializeField] public float modDamage;
    [SerializeField] private AudioSource Audio;
    [SerializeField] private bool _StartCoruintine = false;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        _PlayerStats = Player.GetComponent<PlayerStats>();
        Invoke("DestroySpell", lifeTime);
        modDamage = _PlayerStats.ModPowerForce;
    }

    
    void Update()
    {
        modDamage = _PlayerStats.PowerForce;
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mob") && !_StartCoruintine)
        {
            MobStatLogical mob = other.gameObject.GetComponent<MobStatLogical>();
            float Power = GameObject.FindWithTag("Player").GetComponent<PlayerStats>().PowerForce;
            if (mob.isAlive)
            {
                mob.TakeDamage(SpellBaseDamage * modDamage);
                Debug.Log("Mob hitted on " + SpellBaseDamage * modDamage + " damage");
                StartCoroutine(HitMobsTimer());
                _StartCoruintine = true;
            }
        }
    }
   */

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Mob") && !_StartCoruintine)
        {
            MobStatLogical mob = other.gameObject.GetComponent<MobStatLogical>();
            float Power = GameObject.FindWithTag("Player").GetComponent<PlayerStats>().PowerForce;
            if (mob.isAlive)
            {
                mob.TakeDamage(SpellBaseDamage * modDamage);
                Debug.Log("Mob hitted on " + SpellBaseDamage * modDamage + " damage");
                StartCoroutine(HitMobsTimer());
                _StartCoruintine = true;
            }
        }
    }

    private IEnumerator HitMobsTimer()
    {
        yield return new WaitForSeconds(3);
        _StartCoruintine = false;
    }




    private void DestroySpell()
    {
        Destroy(gameObject);
    }
}
