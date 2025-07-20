using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBuff : MonoBehaviour
{

    public BoxCollider2D BC;

    public BombAttack BA;
    // Start is called before the first frame update
    void Start()
    {
        BC = GetComponent<BoxCollider2D>();
        BA = GameObject.FindGameObjectWithTag("Bombing").GetComponent<BombAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BA.Attack();

            GameObject.Destroy(gameObject);
        }
    }
}
