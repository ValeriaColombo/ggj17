using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBlast : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<PlayerController>().TakeDamage();
    }
}
