using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    
    private Vector3 offset;
    
    void Update()
    {
        float x = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal");
        transform.position += transform.forward * x;
        transform.Rotate(0, z, 0);
    }
}