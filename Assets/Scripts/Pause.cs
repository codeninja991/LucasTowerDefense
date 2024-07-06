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

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);
        TutPages[0].SetActive(false);
        TutPages[1].SetActive(false);
        TutPages[2].SetActive(false);
        TutPages[3].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
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

    public void Controls()
    {
        PauseMenu.SetActive(false);
        currentPage = 0;
        PageActive();
    }

    public void NextPage()
    {
        previousPage = TutPages[currentPage];
        previousPage.SetActive(false);
        currentPage++;
        if (currentPage <= 4)
        {
            PageActive();
        }
    }

    public void PreviousPage()
    {
        previousPage = TutPages[currentPage];
        previousPage.SetActive(false);
        currentPage--;
        if(currentPage > -1)
        {
            PageActive();
        }
    }

    public void PageActive()
    {
        TutPages[currentPage].SetActive(true);
    }
}
