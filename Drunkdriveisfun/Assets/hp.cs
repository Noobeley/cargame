using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class hp : MonoBehaviour
{
    public int health = 100;
    public TMPro.TextMeshProUGUI healthText;
    public GameObject GameOver;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GameOver.SetActive(true);
        }
    }

    void UpdateHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }
}
