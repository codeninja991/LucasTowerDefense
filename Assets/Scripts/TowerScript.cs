using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
    public float distance,
        reloadTime = 1f;
    public int damage = 1;
    public LayerMask enemylayer;
    private bool shooting;
    Ray ray;
    public Text upgradeText;
    public int towerLevel;
    public GameObject[] towerList;
    RaycastHit hit;

    public GameObject enemyShooting;
    public bool enemyShot;

    public ParticleSystem shoot;
    public Animator shootingAnim;

    int waveA;
    int waveB;

    //public Dictionary<int, int> towerDictionary = new Dictionary<int, int>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(var t in towerList)
        {
            
        }

        //towerLevel = Mathf.Clamp(towerLevel, 0, GameManager.instance.upgradeAmounts.Length - 1);
        //upgradeText = GameManager.instance.upgradeText;
        //InitializeTowerDictionary();
        shootingAnim = gameObject.GetComponent<Animator>();
        shoot.gameObject.SetActive(false);
        enemyShot = false;
    }

    //private void InitializeTowerDictionary()
    //{
    //    for (int i = 0; i < upgradeAmounts.Length; i++)
    //    {
    //        towerDictionary[i] = upgradeAmounts[i];
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 3.4f, Color.red);
        waveA = GameManager.instance.waveNumber;
        if (waveA != waveB)
        {
            shooting = false;
            enemyShooting = null;
            enemyShot = false;
            waveB = waveA;
        }

        if (GameManager.instance.germsAlive == 0)
        {
            shooting = false;
            enemyShooting = null;
            enemyShot = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Germ"))
        {
            enemyShot = true;
            if (enemyShot == true)
            {
                transform.LookAt(other.transform.position);
                //RaycastHit hit;
                if (Physics.Raycast(ray.origin, ray.direction, out hit, distance, enemylayer))
                {
                    if (hit.collider.tag == ("Germ"))
                    {
                        enemyShooting = hit.transform.gameObject;
                        if (shooting == false && enemyShot == true)
                        {
                            StartCoroutine(DealDamage());
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(enemyShot == true)
        {
            enemyShot = false;
        }
    }
    
    private IEnumerator DealDamage()
    {
        shooting = true;
        while (shooting && hit.collider)
        {
            shoot.gameObject.SetActive(true);
            shoot.Play();
            hit.collider.GetComponent<Germ>().TakeDamage(damage);
            yield return new WaitForSeconds(reloadTime);
        }
        shoot.Stop();
        shooting = false;
    }

    public bool UpgradeTower()
    {
        int towerlvl = towerLevel;
        if (towerlvl == 0)
        {
            if (GameManager.instance.coinAmount >= 100)
            {
                GameManager.instance.coinAmount -= 100;
                GameManager.instance.upgradeText.text = "200";
                GameObject temp = Instantiate(towerList[1], transform.position, transform.rotation);
                GameManager.instance.currentTower = temp.GetComponent<TowerScript>();
                gameObject.SetActive(false);
                gameObject.transform.parent = GameManager.instance.trash;
                return true;
            } else
            {
                GameManager.instance.upgradeText.text = "100";
                return false;
            }
        }
        else if (towerlvl == 1)
        {
            if (GameManager.instance.coinAmount >= 200)
            {
                GameManager.instance.coinAmount -= 200;
                GameManager.instance.upgradeText.text = "350";
                GameObject temp = Instantiate(towerList[2], transform.position, transform.rotation);
                GameManager.instance.currentTower = temp.GetComponent<TowerScript>();
                gameObject.SetActive(false);
                gameObject.transform.parent = GameManager.instance.trash;
                return true;
            } else
            {
                GameManager.instance.upgradeText.text = "200";
                return false;
            }
        }
        else if (towerlvl == 2)
        {
            if (GameManager.instance.coinAmount >= 350)
            {
                GameManager.instance.coinAmount -= 350;
                GameManager.instance.upgradeText.text = "600";
                GameObject temp = Instantiate(towerList[3], transform.position, transform.rotation);
                GameManager.instance.currentTower = temp.GetComponent<TowerScript>();
                gameObject.SetActive(false);
                gameObject.transform.parent = GameManager.instance.trash;
                return true;
            }
            else
            {
                GameManager.instance.upgradeText.text = "350";
                return false;
            }
        }
        else if (towerlvl == 3)
        {
            if (GameManager.instance.coinAmount >= 600)
            {
                GameManager.instance.coinAmount -= 600;
                GameManager.instance.upgradeText.text = "MAX";
                GameObject temp = Instantiate(towerList[4], transform.position, transform.rotation);
                GameManager.instance.currentTower = temp.GetComponent<TowerScript>();
                gameObject.SetActive(false);
                gameObject.transform.parent = GameManager.instance.trash;
                return true;
            }
            else
            {
                GameManager.instance.upgradeText.text = "600";
                return false;
            }
        }
        else if (towerlvl == 4)
        {
            GameManager.instance.upgradeText.text = "MAX";
            return false;
        }
        towerLevel = Mathf.Clamp(towerLevel + 1, 0, 5);
        return false;
    }
}
