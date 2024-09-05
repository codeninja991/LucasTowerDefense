using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public Button startGame;
    public Button startTutorial;
    public GameObject tutorial;
    public Text title;
    public Button exitTutorial;

    // Start is called before the first frame update
    void Start()
    {
        startGame.gameObject.SetActive(true);
        startTutorial.gameObject.SetActive(true);
        tutorial.SetActive(false);
        title.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sceneSwitch()
    {
        SceneManager.LoadScene(1);
    }

    public void howToPlay()
    {
        startGame.gameObject.SetActive(false);
        startTutorial.gameObject.SetActive(false);
        tutorial.SetActive(true);
    }

    public void exitButton()
    {
        tutorial.SetActive(false);
        startGame.gameObject.SetActive(true);
        startTutorial.gameObject.SetActive(true);
    }
}
