using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycastingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
          //  Debug.Log(spreadAngle);
            RaycastHit2D hit = Physics2D.Raycast(center, spreadAngle);
            
            if(hit.collider != null)
            {
             //   Debug.DrawLine(center,hit.point,Color.white);
            }
            else
            {
            //    Debug.DrawLine(center, spreadAngle * 200 + center,Color.white);
            }

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
