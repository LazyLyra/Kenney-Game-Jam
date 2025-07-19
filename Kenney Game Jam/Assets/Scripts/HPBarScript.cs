using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarScript : MonoBehaviour
{
    public PlayerHealthScript PHS;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        PHS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthScript>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = (PHS.currentHealth / PHS.maxHealth);
    }
}
