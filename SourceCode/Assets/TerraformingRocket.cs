using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerraformingRocket : MonoBehaviour
{
    public float velocity = 25.0f;
    public float acceleration = 1.0f;
    public float decceleration = 2.0f;
    public float degreesToRotatePerSecond = 45.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (velocity * Time.smoothDeltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocity += acceleration;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (velocity - acceleration > 0)
            {
                velocity -= decceleration;
            }
            else
            {
                velocity = 0.0f;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, degreesToRotatePerSecond * Time.smoothDeltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -degreesToRotatePerSecond * Time.smoothDeltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(degreesToRotatePerSecond * Time.smoothDeltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(-degreesToRotatePerSecond * Time.smoothDeltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, degreesToRotatePerSecond * Time.smoothDeltaTime, 0);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, -degreesToRotatePerSecond * Time.smoothDeltaTime, 0);
        }
    }
}
