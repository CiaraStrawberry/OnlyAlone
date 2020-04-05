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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(collider.bounds.Contains(player.position))
        {
            mainLevelMan.restartLevel();
        }
    }

    
}
