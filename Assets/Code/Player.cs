using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody playerRB;

    private float MoveSpeed = 8.0f;

    public Camera cam;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame

    private void Update()
    {
        
        playerRB.velocity = Vector3.zero;

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

        Vector3 forward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
        Vector3 right = new Vector3(cam.transform.right.x, 0, cam.transform.right.z);
        Vector3 clamped = Vector3.ClampMagnitude((right * horizontal) + (forward * vertical), .15f);

        playerRB.MoveRotation(Quaternion.identity);
        playerRB.MovePosition(transform.position + clamped);

    }

    
}

