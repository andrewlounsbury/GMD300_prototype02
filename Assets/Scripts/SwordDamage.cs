using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider Enemy)
    {
        var EnemyHealth = Enemy.gameObject.GetComponent<EnemyHealth>();
        

        if (EnemyHealth != null)
        {
            EnemyHealth.TakeDamage();
        }
    }
}
