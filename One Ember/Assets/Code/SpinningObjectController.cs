using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls the objects moving in a circle in the 3d level.
/// </summary>
public class SpinningObjectController : MonoBehaviour
{

    [SerializeField]
    private Transform center;


    [SerializeField]
    private Transform[] rotationThings;

    public float distance = 20f;

    public float speed = 15f;

    [SerializeField]
    private float offset;

    void Update()
    {
       Vector3[] allPositions =  positionsInACircle(rotationThings.Length, center.position);
        for (int i =0; i < allPositions.Length; i++)
        {
            rotationThings[i].position = allPositions[i];
            rotationThings[i].LookAt(center,transform.up);
        }
    }

    private Vector3[] positionsInACircle(float numberOfPositions, Vector3 center)
    {
        Vector3 noAngle = transform.forward;
        List<Vector3> data = new List<Vector3>();
        data.Add(center + noAngle * distance);
        offset += 0.01f;
        transform.Rotate(0, speed * Time.deltaTime, 0);
        //  Debug.DrawLine(center, center + noAngle * 20f);
        for (int i = 1; i < numberOfPositions; i++)
        {
            Vector3 spreadAngle = Quaternion.AngleAxis(360f / numberOfPositions, Vector3.forward) * noAngle;
            // spreadAngle = Quaternion.AngleAxis(offset, Vector3.forward) * spreadAngle;
            noAngle = spreadAngle;
            data.Add(center + spreadAngle * distance);
            //   Debug.DrawLine(center, center + spreadAngle * 20f);
        }
        return data.ToArray();
    }
}
