using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GermSpawner : MonoBehaviour
{
    public GameObject[] germs;

    [Header("GermSpawner")]
    public GameObject germSpawn;
    public GameObject spawningGerm;
    public Text waveText;
    public Button nextWaveButton;
    public Text nextWaveText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(GameManager.instance.germsAlive <= 0)
        {
            if(GameManager.instance.waveNumber == 0)
            {
                nextWaveText.text = "Start Wave";
            } else
            {
                nextWaveText.text = "Next Wave";
            }
            nextWaveButton.gameObject.SetActive(true);
       }
       
       if(GameManager.instance.germsAlive > 0)
       {
            nextWaveButton.gameObject.SetActive(false);
       }
    }

    public void GermSpawn()
    {
        for (int i = 0; i < 4 + GameManager.instance.waveNumber * 5; i++)
        {
            if(GameManager.instance.waveNumber < 5)
            {
                spawningGerm = germs[Random.Range(0, 2)];
                Instantiate(spawningGerm, transform.position, transform.rotation);
            }
            else
            {
                spawningGerm = germs[Random.Range(0, 3)];
                Instantiate(spawningGerm, transform.position, transform.rotation);
            }
        }
        GameManager.instance.waveNumber += 1;
        GameManager.instance.coinAmount += GameManager.instance.waveNumber * 50;
        waveText.text = GameManager.instance.waveNumber.ToString();

        if (GameManager.instance.waveNumber % 5 == 0)
        {
            spawningGerm = germs[3];
            Instantiate(spawningGerm, transform.position, transform.rotation);
        }
    }
}
