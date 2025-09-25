using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void NextScene()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current + 1);
    }

    public void PreviousScene()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current - 1);
    }
}
