using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDoors : DefaultInteraction
{
    public bool isOpen = false;
    public bool isOpening = false;
    public float openAngle = 90.0f;
    public float openSpeed = 2.0f;

    float angleChange = 0;

    public override void Interact(Transform transform, bool callEvent = true)
    {
        base.Interact(transform);
        if (isOpen || isOpening)
        {
            StopAllCoroutines();
            StartCoroutine(CloseDoor());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        isOpening = true;
        float targetAngle = openAngle;
        Debug.Log("Target open angle: " + targetAngle);
        while (angleChange < targetAngle)
        {
            angleChange += openSpeed * Time.deltaTime;
            transform.Rotate(0, openSpeed * Time.deltaTime, 0);
            yield return null;
        }
        isOpen = true;
    }

    IEnumerator CloseDoor()
    {
        isOpening = false;
        float currentAngle = transform.localEulerAngles.y;
        Debug.Log("Current angle: " + currentAngle);
        Debug.Log("Angle change: " + angleChange);
        float targetAngle = currentAngle - angleChange;
        
        Debug.Log("Target close angle: " + targetAngle);
        while (currentAngle > targetAngle)
        {
            currentAngle -= openSpeed * Time.deltaTime;
            angleChange -= openSpeed * Time.deltaTime;
            transform.Rotate(0, -openSpeed * Time.deltaTime, 0);
            yield return null;
        }
        isOpen = false;
    }
}
