using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleColli : MonoBehaviour
{
    // Start is called before the first frame update
    private bool colliding = false;
    private Vector2 contactPoint;
    private Collision2D collision;
    void Start()
    {

    }
    

    // Update is called once per frame
    void Update()
    {

    }

    public bool isColliding()
    {
        return colliding;
    }

    public Vector2 GetContactPoint()
    {
        return contactPoint;
    }
    public Collision2D GetCollision()
    {
        return collision;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        print("Player collider colliding");
        contactPoint = coll.GetContact(0).point;
        collision = coll;
        colliding = true;
        //print("Player collider colliding");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        colliding = false;
    }

}
