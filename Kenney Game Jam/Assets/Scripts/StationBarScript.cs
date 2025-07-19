using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationBarScript : MonoBehaviour
{
    public ChargingAreaScript CAS;
    // Start is called before the first frame update
    void Start()
    {
        CAS = GameObject.FindGameObjectWithTag("Station").GetComponent<ChargingAreaScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(CAS.currentHP / CAS.maxHP, transform.localScale.y, transform.localScale.z);
    }
}
