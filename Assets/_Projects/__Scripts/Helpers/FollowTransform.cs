using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform followObject;
    public bool follow = true;
    public bool followRotation = false;

    [Header("Settings")]
    public bool useRelativePosition = true;

    public bool followX = true;
    public bool followY = true;
    public bool followZ = true;



    private Vector3 _initPosition;

    private Vector3 _initDifference;


    private void Awake()
    {
        _initPosition = transform.position;
        _initDifference = (transform.position - followObject.position);
    }

    void Update()
    {
        if (follow)
        {
            Vector3 targetPosition = followObject.position;

            Vector3 finalPosition = targetPosition + (_initDifference);

            if (!followX)
                finalPosition.x = useRelativePosition ? _initPosition.x : 0;

            if (!followY)
                finalPosition.y = useRelativePosition ? _initPosition.y : 0;

            if (!followZ)
                finalPosition.z = useRelativePosition ? _initPosition.z : 0;

            transform.position = finalPosition;

            if (followRotation)
                transform.rotation = followObject.rotation;
        }
    }
}
