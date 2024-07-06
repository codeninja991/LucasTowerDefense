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

    //public Dictionary<int, int> towerDictionary = new Dictionary<int, int>();

    // Start is called before the first frame update
    void Start()
    {
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
        //print(towerLevel);
        //print(upgradeAmounts[towerLevel]);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Germ"))
        {
            if (enemyShot == true)
            {
                if (enemyShooting = null)
                {
                    enemyShot = false;
                }
                transform.LookAt(other.transform.position);
                if (Physics.Raycast(ray.origin, ray.direction, out hit, distance, enemylayer))
                {
                    if (shooting == false && enemyShot == true)
                    {
                        StartCoroutine(DealDamage());
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Germ"))
        {
            enemyShooting = other.gameObject;
            if(enemyShot == false)
            {
                enemyShot = true;
            }
        }
    }
    
    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Germ"))
        {
            if (enemyShot == true)
            {
                enemyShot = false;
                enemyShooting = null;
                shooting = false;
            }
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
        if(towerlvl == 0)
        {
            if (GameManager.instance.coinAmount >= 100)
            {
                GameManager.instance.coinAmount -= 100;
                GameManager.instance.upgradeText.text = "200";
                GameObject temp = Instantiate(towerList[1], transform.position, transform.rotation);
                GameManager.instance.currentTower = temp.GetComponent<TowerScript>();
                gameObject.SetActive(false);
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
                GameManager.instance.upgradeText.text = "950";
                GameObject temp = Instantiate(towerList[4], transform.position, transform.rotation);
                GameManager.instance.currentTower = temp.GetComponent<TowerScript>();
                gameObject.SetActive(false);
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
            if (GameManager.instance.coinAmount >= 950)
            {
                GameManager.instance.coinAmount -= 950;
                GameManager.instance.upgradeText.text = "MAX";
                towerLevel++;
                GameObject temp = Instantiate(towerList[4], transform.position, transform.rotation);
                GameManager.instance.currentTower = temp.GetComponent<TowerScript>();
                gameObject.SetActive(false);
                return false;
            }
            else
            {
                GameManager.instance.upgradeText.text = "950";
                return false;
            }
        }
        else if (towerlvl == 5)
        {
            GameManager.instance.upgradeText.text = "MAX";
            return false;
        }
        towerLevel = Mathf.Clamp(towerLevel + 1, 0, 5);
        return false;
    }
}
