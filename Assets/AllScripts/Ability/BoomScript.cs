using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomScript : MonoBehaviour
{
    [SerializeField] public float MaxSize = 5;
    [SerializeField] public float Damage = 100;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * Time.deltaTime*5;

        if(transform.localScale.x > MaxSize)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Mob"))
        {
            Debug.Log("Mob hit");
            MobStatLogical mob = other.gameObject.GetComponent<MobStatLogical>();
            if (mob.isAlive)
            {
                mob.TakeDamage(Damage);
                Debug.Log("Mob hitted on " + Damage + " damage");
            }
        }
        
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerStats Stats = other.gameObject.GetComponent<PlayerStats>();
            if(Stats.isAlive)
            {
                Stats.TakeDamage(Damage);
                Debug.Log("Player hitted on " + Damage + "damage");
            }
        }
    }
}
