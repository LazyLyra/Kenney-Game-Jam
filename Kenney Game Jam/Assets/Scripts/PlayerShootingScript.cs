using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingScript : MonoBehaviour
{
    [Header("Direction")]
    public Vector2 aimDirection;
    [SerializeField] float offset;

    [Header("References")]
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetDirection();

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void GetDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        aimDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
    }

    void Fire()
    {
        Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + offset, 0), Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, aimDirection)));
    }
}
