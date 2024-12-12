using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultInteraction : MonoBehaviour
{
    public bool isInteractable = true;
    [SerializeField]
    DefaultInteraction[] interactionTriggers;


    private void Start()
    {
        foreach (DefaultInteraction defaultInteraction in interactionTriggers)
        {
            defaultInteraction.Interaction() += Interaction();
        }
    }

    public virtual void Interact(Transform agent)
    {
        if (!isInteractable)
        {
            Debug.Log("Cannot interact with " + name);
            return;
        }
        Debug.Log("Interacting with " + name);
    }

    //This is only for Triggering Interactions from list
    public virtual void Interaction()
    {
        Interact(transform);
    }

    
}
