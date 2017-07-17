using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal");
        transform.position += transform.forward * x;
        transform.Rotate(0, z, 0);
    }
}