using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class reducehp : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public int health = 100;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        health -= 10;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }
}
