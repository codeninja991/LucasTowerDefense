using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speed : MonoBehaviour
{
    public GameObject[] speedButtons;
    public int currentButton;
    public GameObject previousButton;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        speedButtons[0].SetActive(false);
        speedButtons[1].SetActive(false);
        speedButtons[2].SetActive(true);
        speedButtons[3].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void nextButton()
    {
        previousButton = speedButtons[currentButton];
        previousButton.SetActive(false);
        currentButton++;
        if (Time.timeScale == 1)
        {
            Time.timeScale = 2;
            ButtonActive();
        }
        else if(Time.timeScale == 2)
        {
            Time.timeScale = 3;
            currentButton = 0;
            ButtonActive();
        }
        else if(Time.timeScale == 3)
        {
            Time.timeScale = 0.5f;
            ButtonActive();
        }
        else if(Time.timeScale == 0.5f)
        {
            Time.timeScale = 1;
            ButtonActive();
        }
    }

    public void ButtonActive()
    {
        speedButtons[currentButton].SetActive(true);
    }
}
