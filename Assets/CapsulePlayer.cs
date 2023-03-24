using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CapsulePlayer : MonoBehaviour
{
    public Vector3 a = new Vector3(0f, 0f, 0f);
    public float acc;
    public Vector3 v = Vector3.zero;
    public float vitesse = 10f;//Vitesse engendrée par la flèche de droite ou de gauche

    public Vector3 normal = new Vector3(0f, -0f, 0f);

    private Rigidbody2D rb;
    public bool isGrounded = false;
    public float anglePente = 0;
    public float angleV = 0;
    public float angleNorm = 0;
    public double teta = 0;

    public GameObject playerCollider;
    public CapsuleColli pc;
    public Transform pct;//PlayerColliderTransform


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        a.y = acc * Time.fixedDeltaTime;
        v = 3 * Vector3.down;

        playerCollider = GameObject.Find("CapsuleCollider");
        pct = playerCollider.transform;
        pc = playerCollider.GetComponent<CapsuleColli>();
    }

    //Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 moveInput = new Vector3(horizontal, vertical, 0f);
        if (!isGrounded)
        {
            v += a;
            moveInput.y = 0;
            moveInput.x = 0;
        }
        else
        {
            v.x = 0;
            v.y = 0;
        }
        if (v.x != 0) moveInput.x = 0;

        float temp = vitesse;
        angleV = Vector2.SignedAngle(Vector2.right, new Vector2(v.x, v.y));

        if (angleV < 0) { angleV += 360; }

        if (horizontal != 0 && vertical != 0)
        {
            temp = (float)Math.Sqrt(Math.Pow((double)vitesse, 2) / 2);
        }
        v += (moveInput * temp);

        pct.localScale = transform.localScale + new Vector3(v.magnitude * Time.fixedDeltaTime, v.magnitude * Time.fixedDeltaTime/2, 0f);

        print("Is grounded : " + isGrounded);
        print("is colliding : " + pc.isColliding());
        if (!isGrounded && pc.isColliding())
        {

            //Vector3 increment = new Vector3((float)Math.Abs(v.x), (float)Math.Abs(v.y), 0f);
            //Vector3 origin = transform.position + Vector3.Scale(transform.lossyScale / 2 + increment * Time.fixedDeltaTime, v.normalized);
            //Debug.DrawRay(origin, v * Time.fixedDeltaTime, Color.red);
            //RaycastHit2D hit = Physics2D.Raycast(origin, v * Time.fixedDeltaTime, v.magnitude * Time.fixedDeltaTime);


            print("Alolo");
            //transform.position = pc.GetCollision().gameObject.transform.position + new Vector3(transform.position.x, (pc.GetCollision().gameObject.transform.localScale.y) / 2 + GetComponent<CapsuleCollider2D>().size.y * transform.localScale.y / 2, 0f);
            print(pc.GetCollision().gameObject.transform.position);           
            transform.position = pc.GetCollision().gameObject.transform.position;
            //transform.position = pc.GetContactPoint() + new Vector2(0f,(transform.localScale.y)/2);
        }
        else
        {
            transform.position += v * Time.fixedDeltaTime;

        }

        pct.position = transform.position;

    }

    void ground()
    {
        isGrounded = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;

        int nbContacts = collision.contactCount;

        /*Isoler le dernier contact, avec les informations de celui-ci, trouver la normale et l'angle de la normale
        par rapport à l'horiozontale (je converti l'ange de degrés à radians)*/

        ContactPoint2D contact = collision.GetContact(nbContacts - 1);
        normal = contact.normal;
        angleNorm = (float)(180 / (Math.PI) * Math.Asin(Math.Abs(normal.y)));
        if (normal.x <= 0 && normal.y >= 0)
        {
            angleNorm = 180 - angleNorm;
        }
        if (normal.x < 0 && normal.y < 0)
        {
            angleNorm = 180 + angleNorm;
        }
        if (normal.x > 0 && normal.y < 0)
        {
            angleNorm = 270 + angleNorm;
        }
        anglePente = angleNorm + 90;
        //Debug.Log("Angle de la pente : " + anglePente);
        //Debug.Log("Angle normale : " + angleNorm);
        //Debug.Log("Angle vitesse : " + angleV);
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        //Apr�s la collision, on r�initialise toutes les variables li�es � la collision 
        isGrounded = false;
        normal.x = 0f;
        normal.y = 0f;
        angleNorm = 0;
    }
}
