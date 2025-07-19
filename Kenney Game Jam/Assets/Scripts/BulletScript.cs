using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float destroyTime;
    [SerializeField] float timer;

    [SerializeField] Vector2 fireDirection;
    [SerializeField] float moveSpeed;

    public BoxCollider2D BC;
    public PlayerShootingScript PSS;

    // Start is called before the first frame update
    void Start()
    {
        BC = GetComponent<BoxCollider2D>(); 
        PSS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShootingScript>();

        fireDirection = PSS.aimDirection.normalized;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > destroyTime)
        {
            GameObject.Destroy(gameObject);
        }
              
        Vector3 movement = fireDirection * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }
}
