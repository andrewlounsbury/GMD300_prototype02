using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string SceneName; 
    public void SwitchScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}