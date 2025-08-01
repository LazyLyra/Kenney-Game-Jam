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
  

    // Start is called before the first frame update
    void Start()
    {
        BC = GetComponent<BoxCollider2D>(); 
        

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        fireDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        fireDirection = fireDirection.normalized;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject.Destroy(gameObject);
        }
    }
}
