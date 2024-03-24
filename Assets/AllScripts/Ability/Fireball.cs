using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed = 300.0f;
    [SerializeField] private float lifeTime = 10.0f;
    [SerializeField] public float FireballBaseDamage = 30.0f;
    [SerializeField] public GameObject Player;
    [SerializeField] public PlayerStats _PlayerStats;
    [SerializeField] public float modDamage;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        _PlayerStats = Player.GetComponent<PlayerStats>();
        Invoke("DestroyFireball", lifeTime);
        modDamage = _PlayerStats.ModPowerForce;

    }

    private void Update()
    {
        modDamage = _PlayerStats.PowerForce;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Mob"))
        {
            MobStatLogical mob = collision.gameObject.GetComponent<MobStatLogical>();
            float Power = GameObject.FindWithTag("Player").GetComponent<PlayerStats>().PowerForce;
            if (mob.isAlive)
            {
                mob.TakeDamage(FireballBaseDamage * modDamage);
                Debug.Log("Mob hitted on "+ FireballBaseDamage * modDamage + " damage");
            }
            DestroyFireball();
        }
        Debug.Log("CollisionEnter");   
        
        DestroyFireball();
    }

    private void DestroyFireball()
    {
        Destroy(gameObject);
    }
}
