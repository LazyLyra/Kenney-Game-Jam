using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BombAttack : MonoBehaviour
{
    public GameObject warning;
    public GameObject particles;
    public GameObject colliders;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Instantiate(warning);

        StartCoroutine(Delay());

        
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);

        Instantiate(particles, new Vector3(-1.5f, -1.5f, -1.5f), Quaternion.Euler(0,0,0));
        Instantiate(colliders, new Vector3(-1.5f, -1.5f, -1.5f), Quaternion.Euler(0, 0, 0));
    }

    
}
