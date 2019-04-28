using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlAndMovement : MonoBehaviour
{
    [Header("Movement Keybindings:")]
    public KeyCode thrustForward = KeyCode.W;
    public KeyCode thrustBackward = KeyCode.S;

    public KeyCode strafeRight = KeyCode.D;
    public KeyCode strafeLeft = KeyCode.A;

    public KeyCode rotateRight = KeyCode.J;
    public KeyCode rotateLeft = KeyCode.G;

    public KeyCode dampMovement = KeyCode.E;
    

    [Header("Movement Stats:")]
    public float forwardAcceleration = 2500f;
    public float strafeAcceleration = 1500f;
    public float RCSPower = 7.5f;

    public float dampingForce = 2f;
    private float normalDrag;

    [Header("Sockets:")]
    public new Rigidbody rigidbody;

    void Start()
    {
        normalDrag = rigidbody.drag;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(thrustForward))
        {
            rigidbody.AddRelativeForce(0, 0, forwardAcceleration * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        else if (Input.GetKey(thrustBackward))
        {
            rigidbody.AddRelativeForce(0, 0, -strafeAcceleration * Time.fixedDeltaTime, ForceMode.Acceleration);
        }

        if (Input.GetKey(strafeRight))
        {
            rigidbody.AddRelativeForce(strafeAcceleration * Time.fixedDeltaTime, 0, 0, ForceMode.Acceleration);
        }
        else if (Input.GetKey(strafeLeft))
        {
            rigidbody.AddRelativeForce(-strafeAcceleration * Time.fixedDeltaTime, 0, 0, ForceMode.Acceleration);
        }

        if (Input.GetKey(rotateRight))
        {
            rigidbody.AddRelativeTorque(0, RCSPower, 0, ForceMode.Acceleration);
        }
        else if (Input.GetKey(rotateLeft))
        {
            rigidbody.AddRelativeTorque(0, -RCSPower, 0, ForceMode.Acceleration);
        }

        if (Input.GetKey(dampMovement))
        {
            rigidbody.drag = dampingForce;
        }
        else rigidbody.drag = normalDrag;

        //BUG FIX (don't judge)
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

    }
}
