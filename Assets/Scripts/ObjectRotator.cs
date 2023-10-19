using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{

    public float RotationSpeed = 50f;
    private bool isRotating = true;

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
        }

    }
}
