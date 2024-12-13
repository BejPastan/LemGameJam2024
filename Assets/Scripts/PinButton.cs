using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinButton : DefaultInteraction
{
    bool isPressed = false;

    public override void Interact(Transform agent, bool callEvent = true)
    {
        base.Interact(agent, callEvent);
        isPressed = !isPressed;
    }

    public void ResetButton()
    {
        if (isPressed)
        {
            Debug.Log("Resetting button"+ transform.name);
            isPressed = false;
            GetComponent<AnimationStartInteraction>().StartAnimation();
        }
    }
}
