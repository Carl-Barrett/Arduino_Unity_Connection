using UnityEngine;

public class RotateY : MonoBehaviour
{
    public float rotationSpeed = 360.0f; // Full rotation per second

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
