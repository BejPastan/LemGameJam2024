using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : DefaultInteraction
{
    public int durability = 1;
    public override void Interact(Transform agent, bool callEvent = true)
    {
        base.Interact(agent, callEvent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            durability -= 1;
            Interact(transform);
            if(durability <= 0)
            {
                Invoke(nameof(DestroyWeb), 0.04f);
            }
        }
    }

    private void DestroyWeb()
    {
        gameObject.SetActive(false);
    }
}
