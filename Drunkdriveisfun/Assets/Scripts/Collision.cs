using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public float currentHP;
    public float maxHp;
    public float Damage1;
    public float Damage2;
    private float destroyable = 0;
    public float damageCone;
    public float damageTree;
    public float damagePed;

    public GameObject smoke1;

    public GameObject smoke2;
    // Start is called before the first frame update
    void Start()
    {
        smoke1.SetActive(false);
        smoke2.SetActive(false);
        currentHP = maxHp;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider obstacle)
    {
        if (obstacle.CompareTag("tree"))
        {
            currentHP = currentHP - damageTree;
        }
        if (obstacle.CompareTag("cone"))
        {
            currentHP = currentHP - damageCone;
            
        }
        if (obstacle.CompareTag("ped"))
        {
            currentHP = currentHP - damagePed;
        }

    }

    private void Update()
    {
        if (currentHP <= Damage1 && currentHP > Damage2)
        {
            smoke1.SetActive(true);
        }

        if (currentHP <= Damage2 && currentHP > destroyable)
        {
            smoke1.SetActive(false);
            smoke2.SetActive(true);
        }

        if (currentHP <= destroyable)
        {

            GameOver.gameOver = true;

        }
        
        
    }
}
