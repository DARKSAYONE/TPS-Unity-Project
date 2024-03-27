using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : MonoBehaviour
{
    [SerializeField] public float speed = 100.0f;
    [SerializeField] private float lifeTime = 10.0f;
    [SerializeField] public float FireballBaseDamage = 30.0f;
    [SerializeField] public static float modDamage = 1.0f;
    [SerializeField] public GameObject Player;
    [SerializeField] public PlayerStats _PlayerStats;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        _PlayerStats = Player.GetComponent<PlayerStats>();
        Invoke("DestroyFireball", lifeTime);
    }

    
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _PlayerStats.TakeDamage(FireballBaseDamage * modDamage);
            DestroyFireball();
        }
        DestroyFireball();
    }
    
    private void DestroyFireball()
    {
        Destroy(gameObject);
    }
}
