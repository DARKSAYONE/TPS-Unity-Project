using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomBottle : MonoBehaviour
{
    [SerializeField] public float Delay = 3.0f;
    [SerializeField] GameObject BoomPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Invoke("BoomBoom",Delay);
    }

    private void BoomBoom()
    {
        Destroy(gameObject);
        var Boom = Instantiate(BoomPrefab);
        Boom.transform.position = transform.position;

    }
}
