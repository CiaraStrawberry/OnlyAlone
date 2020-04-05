using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In this game, the player does not move, the world does, makes the math simpler. This function insures nothing can accidentally break that.
/// </summary>
public class PlayerConstraints : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
