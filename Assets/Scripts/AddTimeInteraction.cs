using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTimeInteraction : DefaultInteraction
{
    [SerializeField]
    float timeToAdd = 10;

    public override void Interact(Transform agent, bool callEvent = true)
    {
        base.Interact(agent, callEvent);
        ResetTimeout.AddTime(timeToAdd);
    }
}
