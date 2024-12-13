using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpInteraction : DefaultInteraction
{
    [SerializeField]
    float defaultForce = 10f;
    Rigidbody rb;
    Collider col;

    Transform parent;

    [SerializeField]
    AudioSource dropAudio;
    public override void Interact(Transform agent, bool callEvent = true)
    {
        base.Interact(agent);
        if (parent == null)
        {
            PickUp(agent);
        }
        else
        {
            Drop();
        }
    }

    //Call pick up action on this object

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void PickUp(Transform newParent)
    {
        Destroy(rb);
        transform.SetParent(newParent);
        transform.position = newParent.position;
        transform.rotation = newParent.rotation;
        transform.SetParent(newParent);
        col.enabled = false;
    }

    public void Drop(Vector3 direction, float force)
    {
        rb = gameObject.AddComponent<Rigidbody>();
        col.enabled = true;
        rb.useGravity = true;
        rb.AddForce(direction * force, ForceMode.Impulse);
        transform.SetParent(null);
        if(dropAudio != null)
        {
            dropAudio.Play();
        }
    }

    public void Drop(Vector3 direction)
    {
        Drop(direction, defaultForce);
    }

    public void Drop()
    {
        Debug.DrawRay(transform.position, transform.forward * 10, Color.red, 5f);
        Drop(transform.forward, defaultForce);
    }
}
