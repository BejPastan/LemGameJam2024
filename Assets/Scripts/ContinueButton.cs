using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class COntinueButton : MonoBehaviour
{
    [SerializeField]
    float timeToWait = 5.0f;
public string sceneName;
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

	public void LoadScene()
{
	SceneManager.LoadScene(sceneName);
}
}
