using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpawner : MonoBehaviour
{
    public GameObject damage;
    public GameObject healandenergy;
    public GameObject shield;
    public GameObject bombing;

    public GameObject ToSpawn;

    [SerializeField] float timer;
    [SerializeField] float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer > spawnTime)
        {
            timer = 0f;
            int temp = Random.Range(1, 5);
            float x = Random.Range(-18, 15);
            float y = Random.Range(-18, 15);

            Vector3 pos = new Vector3(x, y, 0);

            if (temp == 1)
            {
                ToSpawn = damage;
            }
            else if (temp == 2)
            {
                ToSpawn = healandenergy;
            }
            else if (temp == 3)
            {
                ToSpawn = shield;
            }
            else
            {
                //ToSpawn = bombing;
            }

            Instantiate(ToSpawn, pos, Quaternion.Euler(0,0,0));
        }
    }
}
