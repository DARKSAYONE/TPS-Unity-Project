using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{

    [SerializeField] private Transform TelePos;
    [SerializeField] public bool isTeleported = false;
    [SerializeField] public GameObject Player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Movement>().enabled = false;
            other.gameObject.transform.position = TelePos.position;
            Player = other.gameObject;
            StartCoroutine(MovementOn());
            
            
        }
    }

    public IEnumerator MovementOn()
    {
        yield return new WaitForSeconds(0.3f);
        Player.GetComponent<Movement>().enabled = true;
        isTeleported = true;
    }
}
