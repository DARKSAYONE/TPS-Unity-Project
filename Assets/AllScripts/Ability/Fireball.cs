using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed = 300.0f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float lifeTime = 10.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("DestroyFireball", lifeTime);
    }

    private void Update()
    {
        
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
        /*if(collision.gameObject.CompareTag("Mob"))
        {
            Debug.Log("Hit the mob");
            DestroyFireball();
        }*/   
        DestroyFireball();
    }

    private void DestroyFireball()
    {
        Destroy(gameObject);
    }
}
