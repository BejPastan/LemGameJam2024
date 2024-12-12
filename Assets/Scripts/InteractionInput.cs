using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractionInput : MonoBehaviour
{
    PickUpInteraction pickedItem;

    public KeyCode interactKey = KeyCode.E;
    public KeyCode dropKey = KeyCode.Q;

    public float interactionRange = 5f;

    [Tooltip("Layer masks to interact with")]
    public string[] layerMasks;
    private int layerMask;

    private void Awake()
    {
        pickedItem = GetComponent<PickUpInteraction>();
        layerMask = LayerMask.GetMask(layerMasks);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(interactKey))
        {
            Interact();
        }

        if(Input.GetKeyDown(dropKey))
        {
            pickedItem.Drop();
        }
    }

    public void Interact()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.red, 2f);
        if (Physics.Raycast(ray, out hit, interactionRange, layerMask))
        {
            DefaultInteraction interaction = hit.transform.GetComponent<DefaultInteraction>();
            if (interaction)
            {
                interaction.Interact(transform);
                try
                {
                    pickedItem = hit.transform.GetComponent<PickUpInteraction>();
                }
                catch
                {
                    Debug.Log("No PickUpAction component found on " + hit.transform.name);
                }
            }
        }
    }

    public void Drop()
    {
        pickedItem.Drop();
        pickedItem = null;
    }
}
