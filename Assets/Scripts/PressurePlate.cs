using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressurePlate : DefaultInteraction
{
    public override void Interact(Transform agent, bool callEvent = true)
    {
        base.Interact(agent, callEvent);
    }
    public GameObject plateButton;
    private Transform plateButtonTransform;
    private MeshRenderer plateButtonRenderer;

    [Header("Button States")]
    public bool is_one_shot = false;
    public bool is_triggered{get; private set;}
    public int numberOfObjects = 0;

    [Header("Color Settings")]
    public Color triggeredColor = Color.red;  // Color to change to when triggered
    public Color triggeredColorOneShot = Color.green;  // Color to change to when triggered
    public Color defaultColor = Color.black; // Default color



    // Start is called before the first frame update
    void Start()
    {
        plateButtonTransform = plateButton.GetComponent<Transform>();
        plateButtonRenderer = plateButton.GetComponent<MeshRenderer>();

        plateButtonRenderer.material.color = defaultColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            numberOfObjects += 1;
            if (is_triggered == false)
            {
                is_triggered = true;
                plateButtonTransform.Translate(new Vector3(0.0f, -0.08f, 0.0f));
                if (is_one_shot) plateButtonRenderer.material.color = triggeredColorOneShot;
                else plateButtonRenderer.material.color = triggeredColor;
                Interact(transform);
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            numberOfObjects -= 1;
            if (is_triggered == true && is_one_shot == false && numberOfObjects <= 0)
            {
                is_triggered = false;
                numberOfObjects = 0;
                plateButtonTransform.Translate(new Vector3(0.0f, 0.08f, 0.0f));
                plateButtonRenderer.material.color = defaultColor;
                Interact(transform);
            }
            
        }
    }
}
