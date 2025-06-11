using System;
using System.Collections;
using UnityEngine;

public class TurnMeOff : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Fast());
    }

    private IEnumerator Fast()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
}
