using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToweronPath : MonoBehaviour
{
    public GameObject path;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("HandSanitizer") || other.gameObject.CompareTag("Sponge") || other.gameObject.CompareTag("Blueprint"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.transform.parent = GameManager.instance.trash;
        }
    }
}
