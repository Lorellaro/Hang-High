using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;
    [SerializeField] float smoothTime = 2;
    [SerializeField] float acceptablerangeDist = 1f;

    public bool moveToPos2 = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector3.Distance(rb.position, pos2.position) > acceptablerangeDist && moveToPos2)
        {
            Vector3 velocity = rb.velocity;
            rb.MovePosition(Vector3.SmoothDamp(rb.position, pos2.position, ref velocity, smoothTime));
        }
        else
        {
            moveToPos2 = false;
        }

        if (Vector3.Distance(rb.position, pos1.position) > acceptablerangeDist && !moveToPos2)
        {
            Vector3 velocity = rb.velocity;
            rb.MovePosition(Vector3.SmoothDamp(rb.position, pos1.position, ref velocity, smoothTime));
        }
        else
        {
            moveToPos2 = true;
        }
    }
}
