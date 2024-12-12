using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject plateButton;
    private Transform plateButtonTransform;
    private MeshRenderer plateButtonRenderer;

    private bool is_triggereed = false;

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
        Debug.Log("Body entered the trigger zone!");
        is_triggereed = true;
        plateButtonTransform.Translate(new Vector3(0.0f, -0.08f, 0.0f));
        plateButtonRenderer.material.color = triggeredColor;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Body exited the trigger zone!");
        is_triggereed = false;
        plateButtonTransform.Translate(new Vector3(0.0f, 0.08f, 0.0f));
        plateButtonRenderer.material.color = defaultColor;
    }
}
