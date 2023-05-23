using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLogic : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private Transform barrel;
    private Transform cam;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
        {
            RaycastHit hit;
            Vector3 dir;

            if (Physics.Raycast(cam.position, cam.forward, out hit, 50f, GameManager.Instance.groundLayer))
                dir = hit.point - barrel.position;
            else
                dir = cam.position + cam.forward * 50f - barrel.position;

            Instantiate(ball, barrel.position, Quaternion.identity).GetComponent<BallBehaviour>().SetDirection(dir);
            StartCoroutine(shootCooldown());
        }
    }

    IEnumerator shootCooldown()
    {
        canShoot = false;
        yield return new WaitForSecondsRealtime(0.5f);
        canShoot = true;
    }
}
