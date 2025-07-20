using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBuff : MonoBehaviour
{
    public PlayerHealthScript PHS;
    public BoxCollider2D BC;
    // Start is called before the first frame update
    void Start()
    {
        PHS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthScript>();
        BC = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {

            PHS.StartShield();

            GameObject.Destroy(gameObject);
        }
    }
}
