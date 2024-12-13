using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractionInput : MonoBehaviour
{
    public PickUpInteraction pickedItem { get; private set; }

    public KeyCode interactKey = KeyCode.E;
    public KeyCode dropKey = KeyCode.Q;

    public float interactionRange = 5f;

    [Tooltip("Layer masks to interact with")]
    public string[] layerMasks;
    private int layerMask;

    public static InteractionInput instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        pickedItem = GetComponent<PickUpInteraction>();
        layerMask = LayerMask.GetMask(layerMasks);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Interact();
        }

        if (Input.GetKeyDown(dropKey))
        {
            Drop();
        }
    }

    public void Interact()
    {
        Debug.LogWarning("Interacting");
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.red, 2f);
        if (Physics.Raycast(ray, out hit, interactionRange, layerMask))
        {
            Debug.LogWarning("Hit " + hit.transform.name);
            DefaultInteraction interaction = hit.transform.GetComponent<DefaultInteraction>();
            if (interaction)
            {
                Debug.LogWarning("Interacting with " + hit.transform.name);
                if (hit.transform.GetComponent<PickUpInteraction>() != null)
                {
                    if (pickedItem == null)
                    {
                        pickedItem = hit.transform.GetComponent<PickUpInteraction>();
                        interaction.Interact(transform);
                    }
                }
                else
                {
                    interaction.Interact(transform);
                    Debug.Log("No PickUpAction component found on " + hit.transform.name);
                }
            }
        }
    }

    public void Drop()
    {
        if (pickedItem == null)
        {
            return;
        }
        pickedItem.Drop();
        pickedItem = null;
    }
}
