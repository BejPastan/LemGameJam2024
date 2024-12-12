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

    [Header("Color Settings")]
    public Color triggeredColor = Color.red;  // Color to change to when triggered
    public Color defaultColor = Color.black; // Default color



    // Start is called before the first frame update
    void Start()
    {
        plateButtonTransform = plateButton.GetComponent<Transform>();
        plateButtonRenderer = plateButton.GetComponent<MeshRenderer>();

        plateButtonRenderer.material.color = defaultColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Interactable")) && is_triggered == false)
        {
            is_triggered = true;
            plateButtonTransform.Translate(new Vector3(0.0f, -0.08f, 0.0f));
            plateButtonRenderer.material.color = triggeredColor;
            Interact(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Player") && is_one_shot == false) && is_triggered == true)
        {
            is_triggered = false;
            plateButtonTransform.Translate(new Vector3(0.0f, 0.08f, 0.0f));
            plateButtonRenderer.material.color = defaultColor;
            Interact(transform);
        }
    }
}
