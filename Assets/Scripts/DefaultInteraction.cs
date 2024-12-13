using System.Linq;
using UnityEngine;


public class DefaultInteraction : MonoBehaviour
{
    public bool isInteractable = true;
    [SerializeField]
    [Tooltip("These interaction can trigger this object interaction")]
    protected DefaultInteraction[] interactionTriggers;

    [SerializeField]
    [Tooltip("Conditions to be met before interaction can be triggered")]
    InteractionCondition[] conditions;

    [Header("AudioFiles")]
    [SerializeField]
    AudioSource succesAudio;
    [SerializeField]
    AudioSource failAudio;
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
            failAudio.Play();
            throw new System.Exception("Cannot interact with " + name);
        }
        foreach (InteractionCondition condition in conditions)
        {
            Debug.Log("Checking condition");
            if (!condition.IsMet(agent))
            {
                if(failAudio != null)
                    failAudio.Play();
                throw new System.Exception("Condition not met");
            }
        }
        Debug.Log("Interacting with " + name);
        if(callEvent)
        {
            OnInteraction?.Invoke();
            if(succesAudio != null)
                succesAudio.Play();
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
            return item.Contains(InteractionInput.instance.pickedItem.transform);
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