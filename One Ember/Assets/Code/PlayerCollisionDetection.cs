using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Transform player;

    [SerializeField]
    private BoxCollider2D collider;

    [SerializeField]
    private MainLevelManager mainLevelMan;

    /// <summary>
    /// checks if the player has moved into a wall.
    /// </summary>
    void Update()
    {
        if(collider.bounds.Contains(player.position))
        {
            mainLevelMan.restartLevel();
        }
    }

    
}
