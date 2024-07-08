using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // player movement speed

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // get horizontal input (A/D or Left/Right arrow keys)
        float verticalInput = Input.GetAxis("Vertical"); // get vertical input (W/S or Up/Down arrow keys)

        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput); // create movement direction vector

        transform.Translate(direction.normalized * speed * Time.deltaTime); // move the player in the desired direction
    }
}

