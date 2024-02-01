using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private float yawSpeed = 2.0f;
    private float pitchSpeed = 2.0f;

    private Rigidbody playerRB;

    private float MoveSpeed = 10.0f;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();

    }

    // Update is called once per frame

    private void Update()
    {
        yaw += yawSpeed * Input.GetAxis("Mouse X");
        pitch -= pitchSpeed * Input.GetAxis("Mouse Y");

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

        Vector3 forward = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 right = new Vector3(transform.right.x, 0, transform.right.z);

        playerRB.MoveRotation(Quaternion.Euler(pitch, yaw, 0));
        playerRB.MovePosition(transform.position + (right * horizontal) + (forward * vertical));

    }

    
}

