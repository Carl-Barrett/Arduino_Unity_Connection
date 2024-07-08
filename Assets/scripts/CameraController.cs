using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // the target to follow (in this case, the player)
    public float distance = 10f; // the distance between the camera and the target
    public float sensitivity = 5f; // the sensitivity of the mouse when rotating the camera
    public float minYAngle = -80f; // the minimum vertical angle the camera can rotate to
    public float maxYAngle = 80f; // the maximum vertical angle the camera can rotate to

    private float currentXRotation = 0f; // the current rotation of the camera around the target on the X axis
    private float currentYRotation = 0f; // the current rotation of the camera around the target on the Y axis

    // Update is called once per frame
    void LateUpdate()
    {
        // rotate the camera based on mouse input
        float mouseInputX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseInputY = Input.GetAxis("Mouse Y") * sensitivity;
        currentXRotation += mouseInputX;
        currentYRotation -= mouseInputY;
        currentYRotation = Mathf.Clamp(currentYRotation, minYAngle, maxYAngle);
        Quaternion rotation = Quaternion.Euler(currentYRotation, currentXRotation, 0f);

        // position the camera behind the target at the desired distance
        Vector3 position = target.position - (rotation * Vector3.forward * distance);

        // apply the rotation and position to the camera
        transform.rotation = rotation;
        transform.position = position;
    }
}

