using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class PlayerHealthSystem : MonoBehaviour
{
    //public variables 
    public int Health = 5;
    public string SceneName;
    public GameObject DangerCube;
    public Slider HealthSlider;
    public int MaxHealth = 5;
    public Image HealthFill;
    public Gradient HealthGradient; 

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider DangerCube)
    {
        Health--;
        //Debug.Log(Health);
    }
    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            SceneManager.LoadScene(SceneName); 
        }
    }

    public void SetMaxHealth()
    {
        HealthSlider.maxValue = MaxHealth;
        HealthFill.color = HealthGradient.Evaluate(1.0f);
    }

    public void SetHealth(int damage)
    {
        Health = Health - damage;
        HealthSlider.value = Health;
        HealthFill.color = HealthGradient.Evaluate(HealthSlider.normalizedValue);
    }



}
