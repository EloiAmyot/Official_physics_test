using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public Vector3 a = new Vector3(0f,0f,0f);
    public float acc = -11f;
    public Vector3 v = Vector3.zero;
    public float vitesse = 10f;//Vitesse engendrée par la flèche de droite ou de gauche

    public Vector3 normal = new Vector3(0f, -0f, 0f);
   
    private Rigidbody2D rb;
    public bool isGrounded = false;
    public float anglePente = 0;
    public float angleV = 0;
    public float angleNorm = 0;
    public double teta = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        a.y = acc * Time.fixedDeltaTime;
        v = 3 * Vector3.down;
    }

    //Update is called once per frame
    void Update()
    {        

    }

    void FixedUpdate()
    {
       /*  Debug.DrawRay(transform.position + (Vector3.Scale((transform.lossyScale/2), v.normalized)), v , Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3.Scale((transform.lossyScale/2), v.normalized)), v , 3f);
        if(hit.collider!=null){
            Debug.Log("hit!");
            rb.MovePosition(transform.position +hit.distance * v.normalized);
        } */
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 moveInput = new Vector3(horizontal, vertical, 0f);
        if(!isGrounded)
        {
            v+=a;
            moveInput.y = 0;
            moveInput.x = 0;
        }
        else
        {
            v.x = 0;
            v.y = 0;
        }
        if(v.x!=0)moveInput.x = 0;

        float temp = vitesse;
        angleV = Vector2.SignedAngle(Vector2.right, new Vector2(v.x, v.y));
        
        if (angleV < 0) {angleV += 360;}
        Debug.Log(angleV);

        if (horizontal != 0 && vertical != 0)
        {
            temp = (float)Math.Sqrt(Math.Pow((double)vitesse, 2) / 2);
        }
        v += (moveInput * temp);

        //Section RayCast
        Debug.DrawRay(transform.position + (Vector3.Scale((transform.lossyScale), v.normalized)), v * Time.fixedDeltaTime, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3.Scale((transform.lossyScale), v.normalized)), v , (float)Math.Sqrt(Math.Pow(v.x, 2)+ Math.Pow(v.y, 2)) * Time.fixedDeltaTime);
        if(hit.collider != null && !isGrounded)
        {
            rb.MovePosition(transform.position + hit.distance * v.normalized);
            return;
        }
        //GitHubTest

        /* if (isGrounded)
        {

            if (angleNorm < 90 && angleNorm > 0)
            {
                teta = angleV - angleNorm - 180;
                temp = (float)(vitesse * Math.Sin(teta));
                v.x = (float)(temp * Math.Cos(angleNorm));
                v.y = (float)(temp * Math.Sin(angleNorm)) * -1;
                //Debug.Log("Cas#1");
            }
            if (angleNorm > 90 && angleNorm < 180 && angleV < 90 && angleV >= 0)
            {
                teta = 180 - angleNorm - angleV;
                temp = (float)(vitesse * Math.Sin(teta));
                v.y = (float)(temp * Math.Cos(angleNorm));
                v.x = (float)(temp * Math.Sin(angleNorm));
                //Debug.Log("Cas#2");
            }
            if (angleNorm < 180 && angleNorm > 90 && angleV < 360 && angleV > 270)
            {
                teta = angleV - angleNorm - 180;
                temp = (float)(vitesse * Math.Sin(teta));
                v.x = (float)(temp * Math.Cos(angleNorm - 90));
                v.y = (float)(temp * Math.Sin(angleNorm - 90));
                //Debug.Log("Cas#3");
            }
            angleV = Vector2.SignedAngle(new Vector2(1, 0), v);
            if (angleV < 0)
            {
                angleV += 360;
            }
            //Debug.Log("Angle vitesse coll: " + angleV);
            
        } */
        rb.MovePosition(transform.position + v * Time.fixedDeltaTime);
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
        if(normal.x <= 0 && normal.y >= 0)
        {
            angleNorm = 180 - angleNorm;
        }
        if(normal.x < 0 && normal.y < 0)
        {
            angleNorm = 180 + angleNorm;
        }
        if( normal.x > 0 && normal.y < 0)
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
        normal.x= 0f;
        normal.y= 0f;
        angleNorm = 0;
    }
}
