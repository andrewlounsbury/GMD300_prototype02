using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthSystem : MonoBehaviour
{
    //public variables 
    public int Health = 5;
    public string SceneName;
    public GameObject DangerCube; 

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider DangerCube)
    {
        Health--;
        Debug.Log(Health);
    }
    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            SceneManager.LoadScene(SceneName); 
        }
    }
}
