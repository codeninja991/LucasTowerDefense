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
    public GameObject nextWaveButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(GameManager.instance.germsAlive <= 0)
        {
            nextWaveButton.SetActive(true);
       }
       
       if(GameManager.instance.germsAlive > 0)
       {
            nextWaveButton.SetActive(false);
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
        GameManager.instance.coinAmount += GameManager.instance.waveNumber * 100;
        waveText.text = GameManager.instance.waveNumber.ToString();

        if (GameManager.instance.waveNumber % 5 == 0)
        {
            spawningGerm = germs[3];
            Instantiate(spawningGerm, transform.position, transform.rotation);
        }
    }
}
