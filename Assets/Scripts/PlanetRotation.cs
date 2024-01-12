using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public float rotationSpeed = 1f;

    void FixedUpdate()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
