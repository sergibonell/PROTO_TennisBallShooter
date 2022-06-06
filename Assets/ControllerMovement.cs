using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMovement : MonoBehaviour
{
    CharacterController cc;
    Transform cam;
    Vector2 mouse;
    Vector2 keyboard;

    private float rotationX = 0f;
    private float rotationY = 0f;

    [SerializeField] float gravity;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float sensX = 100;
    [SerializeField] float sensY = 100;
    [SerializeField] Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        cameraMovement();
        playerMovement();
    }

    void cameraMovement()
    {
        mouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        rotationX = mouse.x * Time.deltaTime * sensX;
        transform.Rotate(Vector3.up * rotationX);

        rotationY -= mouse.y * Time.deltaTime * sensY;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);
        cam.localRotation = Quaternion.Euler(rotationY, 0f, 0f);
    }

    void playerMovement()
    {
        keyboard = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Jump") && cc.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        else
            velocity.y = cc.isGrounded ? -2f : velocity.y + gravity * Time.deltaTime;

        Vector3 move = keyboard.x * transform.right + keyboard.y * transform.forward + velocity.y * transform.up;
        velocity = move;

        cc.Move(velocity * moveSpeed * Time.deltaTime);
    }


}
