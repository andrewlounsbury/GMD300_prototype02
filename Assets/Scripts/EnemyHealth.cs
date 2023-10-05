using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyHealth : MonoBehaviour
{
    public int EnemHealth = 5;
    public GameObject PlayerSword;
    

    public void TakeDamage(int damage = 1)
    {
        EnemHealth -= damage;
        Debug.Log("EnemyHealth = " + EnemHealth);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (EnemHealth <= 0)
        { 
            Destroy(gameObject); 
        }
    }

    
}
