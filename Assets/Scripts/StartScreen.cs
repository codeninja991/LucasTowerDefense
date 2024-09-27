using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class StartScreen : MonoBehaviour
{
    public Button startGame;
    public Button startTutorial;
    public GameObject tutorial;
    public Text title;
    public Button exitTutorial;

    public GameObject loadingScreen;
    public Slider loadingSlider;
    public Animator loadAnim;

    public void LoadGame(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        loadAnim = loadingSlider.GetComponent<Animator>();
        loadAnim.Play("loading");
        StartCoroutine(LoadGameAsync(sceneIndex));
    }

    IEnumerator LoadGameAsync(int sceneIndex)
    {
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(1);
    }
    // Start is called before the first frame update
    void Start()
    {
        startGame.gameObject.SetActive(true);
        startTutorial.gameObject.SetActive(true);
        tutorial.SetActive(false);
        title.gameObject.SetActive(true);
        loadingScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
