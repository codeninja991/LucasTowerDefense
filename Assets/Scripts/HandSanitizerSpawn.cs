using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandSanitizerSpawn : MonoBehaviour
{
    public GameObject handSanitizer;
    public int maxTowers;
    public GameObject sponge;
    private bool textAnimating;

    public void SpawnHandSanitizer()
    {
        if (GameManager.instance.HandSanitizersPlaced.Count >= maxTowers)
        {
            if (!textAnimating)
            {
                textAnimating = true;
                StartCoroutine(TextPopUp());
            }
        }
        else
        {
            Instantiate(handSanitizer);
        }
    }

    public void SpawnSponge()
    {
        if (GameManager.instance.SpongesPlaced.Count >= maxTowers)
        {
            if (!textAnimating)
            {
                textAnimating = true;
                StartCoroutine(TextPopUp());
            }
        }
        else
        {
            Instantiate(sponge);
        }
    }

    private IEnumerator TextPopUp()
    {
        GameManager.instance.denyPlaceText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        GameManager.instance.denyPlaceText.gameObject.SetActive(false);
        textAnimating = false;
    }
}
