using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class FollowObjectWithEase : MonoBehaviour
{

    [SerializeField] Transform followObject;
    [SerializeField] bool followRotation;
    private float followSpeed = 6;
    private float rotationSpeed = 6;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, followObject.position, Time.deltaTime * followSpeed);
        if (followRotation)
            transform.rotation = Quaternion.Lerp(transform.rotation, followObject.rotation, Time.deltaTime * rotationSpeed);
    }
}
