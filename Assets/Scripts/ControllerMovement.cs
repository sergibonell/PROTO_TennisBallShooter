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

    [SerializeField] public bool isDashing;
    [SerializeField] float gravity;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float sensX = 100f;
    [SerializeField] float sensY = 100f;
    [SerializeField] float dashTime = 1f;
    [SerializeField] float dashDistance = 5f;
    [SerializeField] Vector3 velocity;
    Vector3 dashDirection;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cc = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDashing)
            playerMovement();

        cameraMovement();

        if(Input.GetKeyDown(KeyCode.LeftShift))
            startDash();
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

    public void lookAt(Vector3 point)
    {
        Vector3 vec = point - transform.position;
        vec.y = 0f;

        transform.forward = vec;
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

    void startDash()
    {
        dashDirection = Quaternion.Euler(0, cam.eulerAngles.y, 0) * Vector3.forward;
        StartCoroutine(dashLogic());
    }

    public void cancelDash()
    {
        StopCoroutine(dashLogic());
        isDashing = false;
    }

    IEnumerator dashLogic()
    {
        float startTime = Time.time;
        isDashing = true;
        while (Time.time < startTime + dashTime)
        {
            cc.Move(dashDirection * dashDistance * Time.deltaTime/dashTime);
            yield return null;
        }
        isDashing = false;
    }
}
