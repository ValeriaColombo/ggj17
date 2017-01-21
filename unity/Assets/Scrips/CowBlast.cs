using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBlast : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerController>().TakeDamage();
        if (other.CompareTag("Cow"))
        {
            other.GetComponentInParent<Cow>().BlowUp();
        }            
    }
}
