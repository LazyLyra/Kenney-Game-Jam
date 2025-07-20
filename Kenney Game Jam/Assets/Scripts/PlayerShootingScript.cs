using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerShootingScript : MonoBehaviour
{
    [Header("Shooting")]
    public Vector2 aimDirection;

    [SerializeField] float cooldown;
    [SerializeField] float timer;

    [Header("Damage")]
    public bool Buffed;
    [SerializeField] float buffTimer;
    [SerializeField] float buffTime;

    [Header("References")]
    public GameObject bullet;
    public PlayerSFXManager sfx;
    // Start is called before the first frame update
    void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSFXManager>();

        timer = 0f;
        buffTimer = 0f;
        Buffed = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetDirection();

        timer += Time.deltaTime;
        buffTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timer > cooldown)
        {
            Fire();
            timer = 0f;
        }

        if (buffTimer > buffTime)
        {
            Buffed = false;
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
        Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, aimDirection)));

        sfx.PlaySound(0);
    }

    public void StartBuff()
    {
        buffTimer = 0f;
        Buffed = true;

        sfx.PlaySound(2);
    }
}
