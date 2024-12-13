using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COntinueButton : MonoBehaviour
{
    [SerializeField]
    float timeToWait = 5.0f;

    [SerializeField]
    GameObject continueButton;

    private void Update()
    {
        timeToWait -= Time.deltaTime;
        if (timeToWait <= 0.0f)
        {
            continueButton.SetActive(true);
        }
    }
}
