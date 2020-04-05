using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{

    public Transform world;

    public float speed = 20;

    public float colliderDistance = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) moveForwards();
        if (Input.GetKey(KeyCode.A)) moveLeft();
        if (Input.GetKey(KeyCode.D)) moveRight();
        if (Input.GetKey(KeyCode.S)) moveBackwards();
    }

    public void moveForwards()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(0, 0), Vector2.up);
        if (hit.collider == null || (Vector2.Distance(new Vector2(0, 0), hit.point) > colliderDistance || hit.collider.tag == "PlayerCanCollide"))
        {
//            Debug.Log(Vector2.Distance(new Vector2(0, 0), hit.point));
            world.transform.position -= new Vector3(0, 1, 0) * Time.deltaTime * speed;
        }

    }

    public void moveBackwards ()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(0, 0), Vector2.down);
        if (hit.collider == null || (Vector2.Distance(new Vector2(0, 0), hit.point) > colliderDistance || hit.collider.tag == "PlayerCanCollide"))
        {
            world.transform.position += new Vector3(0, 1, 0) * Time.deltaTime * speed;
        }
    }

    public void moveLeft ()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(0, 0), Vector2.left);
        if (hit.collider == null || (Vector2.Distance(new Vector2(0, 0), hit.point) > colliderDistance || hit.collider.tag == "PlayerCanCollide"))
        {
            world.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
        }
    }

    public void moveRight ()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(0, 0), Vector2.right);
        if (hit.collider == null || (Vector2.Distance(new Vector2(0, 0), hit.point) > colliderDistance || hit.collider.tag == "PlayerCanCollide"))
        {
            world.transform.position -= new Vector3(1, 0, 0) * Time.deltaTime * speed;
        }
    }
}
