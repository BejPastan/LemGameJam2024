using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewSceneInteraction : DefaultInteraction
{
    [SerializeField]
    string sceneName;

    public override void Interact(Transform agent, bool callEvent = true)
    {
        base.Interact(agent, callEvent);
        SceneManager.LoadScene(sceneName);
    }
}
