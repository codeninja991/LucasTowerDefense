using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject[] TutPages;
    public int currentPage;
    public GameObject previousPage;
    public GameObject tutorial;
    public bool pauseOn;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);
        tutorial = GameManager.instance.tutorial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        if(pauseOn == false)
        {
            pauseOn = true;
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
        }
        else
        {
            pauseOn = false;
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            tutorial.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Tutorial()
    {
        tutorial.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void ExitTutorial()
    {
        tutorial.SetActive(false);
        PauseMenu.SetActive(true);
    }
}
