using System.Collections.Generic;
using System.Security.Principal;
using DG.Tweening;
using UnityEngine;

public static class CurvePath
{
    public static Vector3 CalculateBezierCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1f - t;
        float t2 = t * t;
        float u2 = u * u;
        return u2 * p0 + 2 * u * t * p1 + t2 * p2;

    }


    public static void CreatePathSeq(Vector3[] points, ref Sequence seq, ref float totalTime, float speed)
    {
        seq = DOTween.Sequence();
        totalTime = 0.0f;
        for (int i = 0; i < points.Length - 1; i++)
        {
            float time = Vector3.Distance(points[i], points[i + 1]) / (100 * speed);

            totalTime += time;


        }

    }


    public static Vector3[] CreatePointsForCurve(Vector3[] anchorPoints, int totalPoints = 20)
    {
        // on start method
        int totalLoopCount = (anchorPoints.Length / 3);
        Vector3[] movingPoints = new Vector3[totalPoints + 1];
        movingPoints[0] = anchorPoints[0];
        for (int j = 0; j < totalLoopCount; j++)
        {
            for (int i = 1; i < totalPoints; i++)
            {
                float t = i / (float)totalPoints;
                int nodeIndex = j * 2;
                Vector3 point = CalculateBezierCurvePoint(t, anchorPoints[nodeIndex], anchorPoints[nodeIndex + 1], anchorPoints[nodeIndex + 2]);
                movingPoints[i] = point;

            }

        }
        movingPoints[totalPoints] = anchorPoints[anchorPoints.Length - 1];
        return movingPoints;
    }
    public static List<Vector3> GetPointsInCurveByTime(Vector3[] _anchorPoints, int _totalPoints, float _time)
    {
        // on start method
        int totalLoopCount = (_anchorPoints.Length / 3);
        List<Vector3> movingPoints = new List<Vector3>();
        movingPoints.Add(_anchorPoints[0]);
        for (int j = 0; j < totalLoopCount; j++)
        {
            for (int i = 1; i < _totalPoints; i++)
            {
                float t = i / (float)_totalPoints;
                if (_time < t)
                {
                    return movingPoints;
                }
                int nodeIndex = j * 2;
                Vector3 point = CalculateBezierCurvePoint(t, _anchorPoints[nodeIndex], _anchorPoints[nodeIndex + 1], _anchorPoints[nodeIndex + 2]);
                movingPoints.Add(point);

            }

        }
        movingPoints.Add(_anchorPoints[_anchorPoints.Length - 1]);
        return movingPoints;
    }

}