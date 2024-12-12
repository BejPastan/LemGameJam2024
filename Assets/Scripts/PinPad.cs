using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinPad : DefaultInteraction
{
    [SerializeField]
    [Tooltip("The correct code to unlock the door, place here buttons in order to unlock the door")]
    DefaultInteraction[] buttonsTriggers;

    int index = 0;

    public override void Interact(Transform agent, bool callEvent = true)
    {
        base.Interact(agent, PinPressed(agent));
    }

    public bool PinPressed(Transform caller)
    {
        Debug.Log(caller);
        if (caller.GetComponent<DefaultInteraction>() == buttonsTriggers[index])
        {
            index++;
            if (index == buttonsTriggers.Length)
            {
                Debug.Log("Door unlocked");
                return true;
            }
            return false;
        }
        else
        {
            Debug.Log("Wrong pin");
            index = 0;
            return false;
        }
    }
}