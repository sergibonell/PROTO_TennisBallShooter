using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //GameManager.Instance.SpeedEvent.AddListener(SetSpeed);
    }

    public void SetDirection(Vector3 dir)
    {
        moveDirection = dir.normalized;
        changeVelocity();
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
        changeVelocity();
    }

    private void changeVelocity()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!rb.useGravity && collision.gameObject.layer == GameManager.Instance.groundLayer)
        {
            rb.useGravity = true;
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
