using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject InsPanel;
    
    void Start()
    {
        menuPanel.SetActive(true);
        InsPanel.SetActive(false);
        
    }

    
    void Update()
    {
        
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void GoToLevel1()
    {
        SceneManager.LoadScene(1);
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

    public void Shop()
    {
        SceneManager.LoadScene(4);
        menuPanel.SetActive(false);
        InsPanel.SetActive(false);
    }
    public void InsButton()
    {
        menuPanel.SetActive(false);
        InsPanel.SetActive(true);
        

    }
    public void InsBack()
    {
        menuPanel.SetActive(true);
        InsPanel.SetActive(false);
        
    }

}
