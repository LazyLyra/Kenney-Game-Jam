using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : MonoBehaviour
{
    public PlayerShootingScript PSS;
    public BoxCollider2D BC;
    // Start is called before the first frame update
    void Start()
    {
        PSS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShootingScript>();
        BC = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            PSS.StartBuff();
        }

        GameObject.Destroy(gameObject);
    }
}
