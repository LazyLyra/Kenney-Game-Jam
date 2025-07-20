using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamScript : MonoBehaviour
{
    public PlayerMovementScript PMS;
    [SerializeField] float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        PMS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>();   
    }

    // Update is called once per frame
    void Update()
    {
        FacePlayer();

        transform.position = Vector2.MoveTowards(transform.position, PMS.transform.position, moveSpeed * Time.deltaTime);
    }

    void FacePlayer()
    {
        Vector3 playerPos = PMS.transform.position;

        Vector2 direction = new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y);

        transform.up = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Destroy(gameObject);
        }
    }
}
