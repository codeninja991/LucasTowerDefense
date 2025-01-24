using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public float livesAmount;
    public Text livesText;
    public GameObject enemy;
    public bool germKill;

    // Start is called before the first frame update
    void Start()
    {
        livesAmount = 20;
        germKill = false;
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = livesAmount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Germ"))
        {
            germKill = true;
            if (germKill == true)
            {
                enemy = other.gameObject;
                if (livesAmount <= 0)
                {
                    GameManager.instance.LoseGame();
                }
                else
                {
                    enemy.gameObject.SetActive(false);
                    enemy.gameObject.transform.parent = GameManager.instance.trash;
                    livesAmount -= 1;
                    germKill = false;
                }
            }
        }
    }
}
