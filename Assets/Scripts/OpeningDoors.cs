using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDoors : DefaultInteraction
{
    public bool isOpen = false;
    public float openAngle = 90.0f;
    public float openSpeed = 2.0f;

    public override void Interact(Transform transform, bool callEvent = true)
    {
        base.Interact(transform);
        if (isOpen)
        {
            StartCoroutine(CloseDoor());
        }
        else
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        float currentAngle = transform.localEulerAngles.y;
        float targetAngle = currentAngle + openAngle;
        while (currentAngle < targetAngle)
        {
            currentAngle += openSpeed;
            transform.localEulerAngles = new Vector3(0, currentAngle, 0);
            yield return null;
        }
        isOpen = true;
    }

    IEnumerator CloseDoor()
    {
        float currentAngle = transform.localEulerAngles.y;
        float targetAngle = currentAngle - openAngle;
        while (currentAngle > targetAngle)
        {
            currentAngle -= openSpeed;
            transform.localEulerAngles = new Vector3(0, currentAngle, 0);
            yield return null;
        }
        isOpen = false;
    }
}
