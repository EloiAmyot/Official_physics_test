using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController3D : MonoBehaviour
{
    public float a;
    public Vector3 v = Vector3.zero;
    
    public Vector3 normal = new Vector3(0f, 0f, 0f);
   
    public float vitesse = 10f;//Vitesse engendr�e par la fl�che de droite ou de gauche
    private Rigidbody rb;
    public bool isGrounded = false;
    public float anglePente = 0;
    public float angleV = 0;
    public float angleNorm = 0;
    public double teta = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        v.x = 0f;
        v.y = vitesse;
        v.z = 0f;
        a = 0.2f;
        //Debug.Log("allo man wtf");
    }

    //Update is called once per frame
    void Update()
    {
       
    }

    void FixedUpdate()
    {
        /* //v.y -= a;
        //Partie Raycast
        Debug.DrawRay(transform.position, v.normalized, Color.red);
        //RaycastHit hit = Physics.Raycast(transform.position, v.normalized, 1f,6);
        //Debug.Log(hit.normal.x);
        Ray ray = new Ray(transform.position, v.normalized);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Hit");
        }


        /* float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 moveInput = new Vector3(horizontal, vertical, 0f);
        float temp = vitesse;
        angleV = Vector3.SignedAngle(new Vector3(1, 0, 0), moveInput, new Vector3(0, 0, 0));
        if (angleV < 0)
        {
            angleV += 360;
        }

        if (horizontal != 0 && vertical != 0)
        {
            temp = (float)Math.Sqrt(Math.Pow((double)vitesse, 2) / 2);
        }
        v = (moveInput * temp);



        bool bla = true;
        if (horizontal == 0 && vertical == 0)
        {
            bla = false;
        }

        if (isGrounded && bla)
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
            } */
            //Debug.Log("Angle vitesse coll: " + angleV);


        //}
        //Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //rb.MovePosition(transform.position + v * Time.fixedDeltaTime); */

    }
}
