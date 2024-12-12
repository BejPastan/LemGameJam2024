using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultInteraction : MonoBehaviour
{
    public bool isInteractable = true;
    [SerializeField]
    [Tooltip("These interaction can trigger this object interaction")]
    DefaultInteraction[] interactionTriggers;


    private void Start()
    {
        foreach (DefaultInteraction defaultInteraction in interactionTriggers)
        {
            defaultInteraction.OnInteraction += () => Interact(defaultInteraction.transform);
        }
    }

    public virtual void Interact(Transform agent, bool callEvent = true)
    {
        if (!isInteractable)
        {
            Debug.Log("Cannot interact with " + name);
            return;
        }
        Debug.Log("Interacting with " + name);
        if(callEvent)
        {
            OnInteraction?.Invoke();
        }
    }

    //event to subscribe
    public delegate void InteractionEvent();
    public event InteractionEvent OnInteraction;
}
