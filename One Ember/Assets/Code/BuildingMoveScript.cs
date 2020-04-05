using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script moves pieces of darkness between two points in alternating directions
/// </summary>
public class BuildingMoveScript : MonoBehaviour
{
    public Transform start;

    public Transform end;

    public float progress = 0;

    public bool increasing = true;

    public float speed = 20;

    void Update()
    {
        if(increasing) progress += speed * Time.deltaTime;
        else  progress -= speed * Time.deltaTime;

        if (progress > 1) increasing = false;
        if (progress < 0) increasing = true;

        Vector3 newPosition = Vector3.Lerp(start.position,end.position,progress);
        transform.position = newPosition;
    }
}
