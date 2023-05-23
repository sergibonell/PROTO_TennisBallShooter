using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSlowmo : MonoBehaviour
{
    ControllerMovement cm;
    GameObject ball;
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        cm = GetComponent<ControllerMovement>();
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ball != null)
        {
            cm.lookAt(ball.transform.position);
            drawBounceLine();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && cm.isDashing)
        {
            Time.timeScale = 0.1f;
            ball = other.transform.parent.gameObject;
            line.enabled = true;
            StartCoroutine(slowmoTimer());
        }
    }

    void drawBounceLine()
    {
        Vector3 lookDir = (ball.transform.position - Camera.main.transform.position).normalized;

        RaycastHit hit;
        if(Physics.Raycast(ball.transform.position, ball.transform.TransformDirection(lookDir), out hit, 10f))
        {
            Vector3 reflected = Vector3.Reflect(lookDir, hit.normal);
            Vector3[] points = new Vector3[]  { ball.transform.position, hit.point, hit.point + reflected * 10f };
            line.positionCount = 3;
            line.SetPositions(points);
        }
        else
        {
            Vector3[] points = new Vector3[] { ball.transform.position, ball.transform.position + lookDir * 10f };
            line.positionCount = 2;
            line.SetPositions(points);
        }
    }

    IEnumerator slowmoTimer()
    {
        yield return new WaitForSecondsRealtime(5f);
        ball = null;
        line.enabled = false;
        Time.timeScale = 1f;
    }
}
