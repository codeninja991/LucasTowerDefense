using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    RaycastHit hit;
    public GameObject ActualTower;
    public LayerMask gridLayer, groundLayer;
    public Vector3 offset;
    public bool canSpawn;
    public bool closePlacement;
    public static event Action<GameObject> towerPlaced;
    public Material red;
    public Material bluePrint;

    public int towerCost;

    // Start is called before the first frame update
    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 50000.0f, gridLayer))
        {
            transform.position = hit.point;
        }

        canSpawn = true;
        closePlacement = true;
        GameManager.instance.canBuy = true;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 50000.0f, gridLayer))
        {
            transform.position = new Vector3((int)hit.point.x, (int)hit.point.y, (int)hit.point.z) + offset;

            if (Input.GetMouseButton(0) && canSpawn == true && GameManager.instance.canBuy == true && GameManager.instance.coinAmount >= towerCost && closePlacement == true)
            {
                GameManager.instance.TowerBuy(towerCost);
                Instantiate(ActualTower, transform.position, transform.rotation);
                towerPlaced?.Invoke(ActualTower); //"Broadcast" event 
                Destroy(gameObject);
            }
            else if (Input.GetMouseButton(0) && canSpawn == false || Input.GetMouseButton(0) && GameManager.instance.canBuy == false || Input.GetMouseButton(0) && GameManager.instance.coinAmount < towerCost || Input.GetMouseButton(0) && closePlacement == false)
            {
                Destroy(gameObject);
            }

            if (Input.GetMouseButton(0) && GameManager.instance.HandSanitizersPlaced.Count >= 1)
            {
                Destroy(gameObject);
            }

            if (canSpawn == false || closePlacement == false)
            {   
                 gameObject.GetComponentInChildren<Body>().GetComponent<MeshRenderer>().material = red;
            }
            else if(canSpawn == true && closePlacement == true)
            {
                gameObject.GetComponentInChildren<Body>().GetComponent<MeshRenderer>().material = bluePrint;
            }

            if (Input.GetMouseButton(1))
            {
                Destroy(gameObject);
            }
        }

        if (Physics.Raycast(ray, out hit, 50000.0f, groundLayer))
        {
            canSpawn = false;
        }
        else if (Physics.Raycast(ray, out hit, 50000.0f, gridLayer))
        {
            canSpawn = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            closePlacement = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            closePlacement = true;
        }
    }
}