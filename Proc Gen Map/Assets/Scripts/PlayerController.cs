using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;
    CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal");
        transform.position += transform.forward * x;
        transform.Rotate(0, z, 0);

        Physics.gravity = new Vector3(0, 2.0f, 0);
        cc.SimpleMove(Physics.gravity);
    }
}