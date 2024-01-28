using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float yaw = 0.0f;

    private Rigidbody playerRB;

    private float MoveSpeed = 10.0f;
    private float YawSpeed = 60.0f;

    //public Vector3 MousePos = Vector3.zero;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();

    }

    // Update is called once per frame

    private void Update()
    {

        //MousePos = Input.mousePosition;

    }

    private void FixedUpdate()
    {
        Manoeuvre();
    }


    void Manoeuvre()
    {
        float horizontal = 0.0f;
        float vertical = 0.0f;

        

        if (Input.GetKey(KeyCode.A)) horizontal = -MoveSpeed * Time.fixedDeltaTime;
        else if (Input.GetKey(KeyCode.D)) horizontal = MoveSpeed * Time.fixedDeltaTime;

        if (Input.GetKey(KeyCode.S)) vertical = -MoveSpeed * Time.fixedDeltaTime;
        else if (Input.GetKey(KeyCode.W)) vertical = MoveSpeed * Time.fixedDeltaTime;

        if (Input.GetKey(KeyCode.LeftArrow)) yaw += -YawSpeed * Time.fixedDeltaTime;
        else if (Input.GetKey(KeyCode.RightArrow)) yaw += YawSpeed * Time.fixedDeltaTime;


        playerRB.MoveRotation(Quaternion.Euler(0, yaw, 0));
        playerRB.MovePosition(transform.position + (transform.right * horizontal) + (transform.forward * vertical));

    }

    
}

