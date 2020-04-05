using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles all the raycasting around the player
/// </summary>
public class PlayerRaycastingManager : MonoBehaviour
{

    public RaycastHit2D[] sendRayInEveryDirection(Vector3 up, Vector3 center)
    {
        Vector3 noAngle = up;
        List<RaycastHit2D> data = new List<RaycastHit2D>();
        data.Add(Physics2D.Raycast(center,Vector3.up));
        for (int i = 1; i < 360; i++)
        {
            Vector3 spreadAngle = Quaternion.AngleAxis(1, Vector3.forward) * noAngle;
            noAngle = spreadAngle;
            Ray ray = new Ray(center, spreadAngle);
            RaycastHit2D hit = Physics2D.Raycast(center, spreadAngle);
            data.Add(hit);
        }
        return data.ToArray();
    }

    public float[] getHitDataDistances (RaycastHit2D[] input)
    {
        float[] output = new float[input.Length];
        for (int i =0; i < input.Length;i++)
        {
            if (input[i].collider == null) output[i] = 0;
            else output[i] = Vector3.Distance(input[i].point, new Vector3(0, 0, 0));
        }
        return output;
    }
}
