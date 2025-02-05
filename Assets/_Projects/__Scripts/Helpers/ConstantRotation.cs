using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]
public class ConstantRotation : MonoBehaviour
{
    public Vector3 rotationSpeed;
    public bool startRotation;
    public bool localRotate = false;

    void Update()
    {
        if (startRotation)
            transform.Rotate(rotationSpeed * Time.deltaTime, localRotate ? Space.Self : Space.World);

    }
}
