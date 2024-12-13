using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

public class DefaultInteraction : MonoBehaviour
{
    public bool isInteractable = true;
    [SerializeField]
    [Tooltip("These interaction can trigger this object interaction")]
    DefaultInteraction[] interactionTriggers;

    [SerializeField]
    [Tooltip("Conditions to be met before interaction can be triggered")]
    InteractionCondition[] conditions;
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
            throw new System.Exception("Cannot interact with " + name);
        }
        foreach (InteractionCondition condition in conditions)
        {
            Debug.Log("Checking condition");
            if (!condition.IsMet(agent))
            {
                throw new System.Exception("Condition not met");
            }
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

[System.Serializable]
public class InteractionCondition
{
    [SerializeField]
    InteractionType interactionType;

    [SerializeField]
    [Tooltip("Item to check for")]
    Transform[] item;

    public bool IsMet(Transform agent)
    {
        switch (interactionType)
        {
            case InteractionType.HaveItem:
                return HaveItem(agent);
        }
        return true;
    }

    public bool HaveItem(Transform agent)
    {
        try
        {
            InteractionInput pickedItem = agent.GetComponent<InteractionInput>();
            return item.Contains(pickedItem.pickedItem.transform);
        }
        catch
        {
            Debug.Log("No InteractionInput component found on " + agent.name);
            return false;
        }
    }
}

public enum InteractionType
{
    HaveItem
}