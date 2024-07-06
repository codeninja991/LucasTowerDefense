using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Germ : MonoBehaviour
{
    //public GameObject goal;
    [Header("GermAI")]
    private NavMeshAgent agent;

    [Header("GermValues")]
    public int health;
    public int coinAmount = 25;
       
    private void Awake()
    {

    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = GameObject.Find("GermDestination").transform.position;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.AddMoney(coinAmount);
        }
            
    }

    // Update is called once per frame
    void Update()
    {

    }
}
