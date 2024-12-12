using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpInteraction : DefaultInteraction
{
    [SerializeField]
    float defaultForce = 10f;
    Rigidbody rb;

    Transform parent;

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
    }

    public void PickUp(Transform newParent)
    {
        Destroy(rb);
        transform.SetParent(newParent);
        transform.position = newParent.position;
        transform.rotation = newParent.rotation;
        transform.SetParent(newParent);
    }

    public void Drop(Vector3 direction, float force)
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(direction * force, ForceMode.Impulse);
        transform.SetParent(null);
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
