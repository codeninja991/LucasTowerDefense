using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject HandSanitizer;
    public GameObject Sponge;
    public Transform trash;

    [Header("Spawn Info")]
    public GameObject spawningTower;

    [Header("Camera Info")]
    public Camera cam;
    public Vector3 mousePos;
    public RaycastHit hit;

    [Header("Coin Info")]
    public Text coin;
    public int coinAmount;
    public bool canBuy = true;

    [Header("Germ Info")]
    public int germsAlive;

    [Header("Tower Info")]
    public List<GameObject> HandSanitizersPlaced = new List<GameObject>();
    public List<GameObject> SpongesPlaced = new List<GameObject>();
    public List<Vector2> TowerPositions = new List<Vector2>();
    public LayerMask towerLayer;
    public LayerMask towerBodyLayer;
    public GameObject towerMenus;

    [Header("UI Info")]
    public Text denyPlaceText;
    public GameObject pauseMenu;
    public Text LoseWaveText;
    public GameObject LoseMenu;
    public GameObject tutorial;

    [Header("Upgrade Info")]
    public GameObject upgradeMenu;
    public Vector3 upgradeOffset;
    public int[] upgradeAmounts;
    //public int towerLevel = 0;
    public int towerUpgrade;
    public Text upgradeText;
    public GameObject towerMenuOpen;

    [Header("Game Info")]
    public static GameManager instance;
    public TowerScript currentTower = null;
    public float speed;
    public int waveNumber;

    //public Dictionary<int , int > towerDictionary = new Dictionary<int, int>();

    private void OnEnable()
    {
        Blueprint.towerPlaced += SanitizersPlacedDown; //"Listen" to towerPlaced event
        //Blueprint.towerPlaced += SpongesPlacedDown;
    }

    private void OnDisable()
    {
        Blueprint.towerPlaced -= SanitizersPlacedDown; //Unsubscribe from event
        //Blueprint.towerPlaced -= SpongesPlacedDown;
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
        germsAlive = 0;
    }

    public void Start()
    {
        denyPlaceText.gameObject.SetActive(false);
        towerMenus.gameObject.SetActive(false);
        LoseMenu.gameObject.SetActive(false);
        //InitializeTowerDictionary();
        tutorial.SetActive(false);
    }

    public void ExitTutorial()
    {
        tutorial.SetActive(false);
    }

    //private void InitializeTowerDictionary()
    //{
    //    for(int i = 0; i < upgradeAmounts.Length; i++)
    //    {
    //        towerDictionary[i] = upgradeAmounts[i];
    //    }
    //}


    public void AddMoney(int amount)
    {
        coinAmount += amount;
    }

    public void TowerBuy(int amount)
    {
        if (coinAmount >= amount)
        {
            coinAmount -= amount;
        }

    }

    public void Update()
    {
        mousePos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

        if (Physics.Raycast(ray, out hit, 100f, towerBodyLayer))
        {
            if (Input.GetMouseButton(1))
            {
                Vector3 towerPos = cam.WorldToScreenPoint(hit.transform.position + upgradeOffset);
                towerMenus.gameObject.SetActive(true);
                towerMenus.transform.position = towerPos;
                towerMenuOpen = hit.collider.transform.parent.gameObject;
                currentTower = hit.collider.GetComponentInParent<TowerScript>();
                if (hit.collider.transform.parent.gameObject.TryGetComponent(out currentTower))
                {
                    SetCost(currentTower.towerLevel);
                }
            }
        }

        coin.text = coinAmount.ToString();

        if (coinAmount <= 0)
        {
            canBuy = false;
        }

        germsAlive = GameObject.FindGameObjectsWithTag("Germ").Length;

    }

    private void SetCost(int lvl)
    {
        switch (lvl)
        {
            case 0:
                upgradeText.text = "100";
                break;
            case 1:
                upgradeText.text = "200";
                break;
            case 2:
                upgradeText.text = "350";
                break;
            case 3:
                upgradeText.text = "600";
                break;
            case 4:
                upgradeText.text = "MAX";
                break;
        }
    }

    private void SanitizersPlacedDown(GameObject tower)
    {

        if (tower.CompareTag("HandSanitizer"))
        {
            HandSanitizersPlaced.Add(tower);
            tower.name = $"HandSanitizer_{HandSanitizersPlaced.Count}";
            TowerPositions.Add(new Vector2(tower.transform.position.x, tower.transform.position.z));
        }
        else if (tower.CompareTag("Sponge"))
        {
            SpongesPlaced.Add(tower);
            tower.name = $"Sponge_{SpongesPlaced.Count}";
            TowerPositions.Add(new Vector2(tower.transform.position.x, tower.transform.position.z));
        }
    }

    public void CloseUpgrade()
    {
        towerMenus.SetActive(false);
    }

    public void SellTower()
    {
        towerMenuOpen.SetActive(false);
        towerMenuOpen.transform.parent = trash;
        towerMenuOpen.transform.position = new Vector3(0, 0);
        if (towerMenuOpen.CompareTag("HandSanitizer") && currentTower.towerLevel == 0)
        {
            coinAmount += 25;
        }
        else if (towerMenuOpen.CompareTag("HandSanitizer") && currentTower.towerLevel == 1)
        {
            coinAmount += 50;
        }
        else if (towerMenuOpen.CompareTag("HandSanitizer") && currentTower.towerLevel == 2)
        {
            coinAmount += 100;
        }
        else if (towerMenuOpen.CompareTag("HandSanitizer") && currentTower.towerLevel == 3)
        {
            coinAmount += 150;
        }
        else if (towerMenuOpen.CompareTag("HandSanitizer") && currentTower.towerLevel == 4)
        {
            coinAmount += 250;
        }

        if (towerMenuOpen.CompareTag("Sponge") && currentTower.towerLevel == 0)
        {
            coinAmount += 50;
        }
        else if (towerMenuOpen.CompareTag("Sponge") && currentTower.towerLevel == 1)
        {
            coinAmount += 175;
        }
        else if (towerMenuOpen.CompareTag("Sponge") && currentTower.towerLevel == 2)
        {
            coinAmount += 125;
        }
        else if (towerMenuOpen.CompareTag("Sponge") && currentTower.towerLevel == 3)
        {
            coinAmount += 175;
        }
        else if (towerMenuOpen.CompareTag("Sponge") && currentTower.towerLevel == 4)
        {
            coinAmount += 250;
        }
        towerMenus.SetActive(false);
    }

    public void LevelUp()
    {
        upgradeText.text = "100";
        if (currentTower)
        {
            currentTower.UpgradeTower();
        }
    }

    public void LoseGame()
    {
        Time.timeScale = 0;
        LoseMenu.SetActive(true);
        LoseWaveText.text = waveNumber.ToString();
    }
}
