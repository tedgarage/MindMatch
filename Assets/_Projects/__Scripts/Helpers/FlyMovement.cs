using UnityEngine;
using DG.Tweening;
//[ExecuteInEditMode]
public class FlyMovement : MonoBehaviour
{
    public float minSpeed = 1;
    public float maxSpeed = 2;

    public Vector3 movementBounds = new Vector3(5, 5, 5);

    public AnimationCurve xCurve;
    public AnimationCurve yCurve;
    public AnimationCurve zCurve;
    public bool isLocalMove = false;
    public bool playOnAwake = false;

    [HideInInspector]
    public bool objectMove = true;

    private Vector3 defaultPosition;

    private float targetX;
    private float targetY;
    private float targetZ;
    private float startX;
    private float startY;
    private float startZ;
    private float curveTimeX;
    private float curveTimeY;
    private float curveTimeZ;
    private float speedForX;
    private float speedForY;
    private float speedForZ;

    void Start()
    {
        if (playOnAwake)
        {
            Init();
            StartMovement();
        }
    }

    void Update()
    {
        if (objectMove)
            SetPosition(new Vector3(GetUpdatedXPos(), GetUpdatedYPos(), GetUpdatedZPos()));
    }

    public void Init(bool _fromZero = false)
    {

        defaultPosition = _fromZero ? Vector3.zero : GetPosition();
        if (_fromZero) transform.localPosition = Vector3.zero;
    }

    public void StartMovement()
    {
        ChooseNewTargetX();
        ChooseNewTargetY();
        ChooseNewTargetZ();
        objectMove = true;
    }
    public void StopAndReset(bool _animation)
    {
        objectMove = false;
        if (isLocalMove)
        {
            if (_animation)
                transform.DOLocalMove(defaultPosition, 0.1f);
            else
                transform.localPosition = defaultPosition;
        }
        else
        {
            if (_animation)
                transform.DOMove(defaultPosition, 0.1f);
            else
                transform.position = defaultPosition;
        }
    }
    public void Stop()
    {
        objectMove = false;
    }
    float GetUpdatedXPos()
    {
        curveTimeX += Time.deltaTime * speedForX;
        if (curveTimeX > 1)
        {
            curveTimeX = 1;
            float currentTarget = targetX;
            ChooseNewTargetX();
            return currentTarget;

        }
        return Mathf.Lerp(startX, targetX, xCurve.Evaluate(curveTimeX));
    }
    float GetUpdatedYPos()
    {
        curveTimeY += Time.deltaTime * speedForY;
        if (curveTimeY > 1)
        {
            curveTimeY = 1;
            float currentTarget = targetY;
            ChooseNewTargetY();
            return currentTarget;

        }
        return Mathf.Lerp(startY, targetY, yCurve.Evaluate(curveTimeY));
    }
    float GetUpdatedZPos()
    {
        curveTimeZ += Time.deltaTime * speedForZ;
        if (curveTimeZ > 1)
        {
            curveTimeZ = 1;
            float currentTarget = targetZ;
            ChooseNewTargetZ();
            return currentTarget;

        }
        return Mathf.Lerp(startZ, targetZ, zCurve.Evaluate(curveTimeZ));
    }

    void ChooseNewTargetX()
    {
        curveTimeX = 0;
        targetX = defaultPosition.x + Random.Range(-movementBounds.x, movementBounds.x);
        startX = GetPosition().x;
        speedForX = Random.Range(minSpeed, maxSpeed);

    }
    void ChooseNewTargetY()
    {
        curveTimeY = 0;
        targetY = defaultPosition.y + Random.Range(-movementBounds.y, movementBounds.y);
        startY = GetPosition().y;
        speedForY = Random.Range(minSpeed, maxSpeed);

    }

    void ChooseNewTargetZ()
    {
        curveTimeZ = 0;
        targetZ = defaultPosition.z + Random.Range(-movementBounds.z, movementBounds.z);
        startZ = GetPosition().z;
        speedForZ = Random.Range(minSpeed, maxSpeed);
    }


    void SetPosition(Vector3 pos)
    {
        if (isLocalMove)
        {
            transform.localPosition = pos;
        }
        else
        {
            transform.position = pos;
        }
    }
    Vector3 GetPosition()
    {
        if (isLocalMove)
        {
            return transform.localPosition;
        }
        else
        {
            return transform.position;
        }
    }
}
//public float speed = 1.0f;
//public float changeInterval = 1.0f;
//public Vector3 movementBounds = new Vector3(5, 5, 5);

//public AnimationCurve xCurve;
//public AnimationCurve yCurve;
//public AnimationCurve zCurve;

//private Vector3 defaultPosition;  // The starting position of the fly
//private Vector3 startingPosition; // Where the fly started when moving towards the new target
//private Vector3 currentTarget;
//private float x;
//private float y;
//private float z;
//private float timer;
//private float curveTime = 0f;

//void Start()
//{
//    defaultPosition = transform.position;  // Store the starting position
//    startingPosition = defaultPosition;
//    ChooseNewTarget();
//}

//void Update()
//{
//    timer += Time.deltaTime;
//    curveTime += Time.deltaTime;

//    if (timer > changeInterval)
//    {
//        ChooseNewTarget();
//        timer = 0;
//        curveTime = 0; // Reset curve time when choosing a new target
//    }

//    float x = Mathf.Lerp(startingPosition.x, currentTarget.x, xCurve.Evaluate(curveTime));
//    float y = Mathf.Lerp(startingPosition.y, currentTarget.y, yCurve.Evaluate(curveTime));
//    float z = Mathf.Lerp(startingPosition.z, currentTarget.z, zCurve.Evaluate(curveTime));

//    transform.position = new Vector3(x, y, z);
//}

//void ChooseNewTarget()
//{
//    x = defaultPosition.x + Random.Range(-movementBounds.x, movementBounds.x);
//    y = defaultPosition.y + Random.Range(-movementBounds.y, movementBounds.y);
//    z = defaultPosition.z + Random.Range(-movementBounds.z, movementBounds.z);

//    startingPosition = transform.position;
//    currentTarget = new Vector3(x, y, z);
//}