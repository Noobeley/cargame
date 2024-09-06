using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private bool isStreetDamaging = false;
    public float currentHP;
    public float maxHp;
    public float Damage1;
    public float Damage2;
    private float destroyable = 0;
    public float damageCone;
    public float damageTree;
    public float damagePed;
    public float damageStreet;
    float lastDamageTime = 0.1f;
    float damageCooldown = -1f;
    public GameObject smoke1;
    public float checkInterval = 1f;

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
        if (Time.time - lastDamageTime < damageCooldown) return;
        if (obstacle.CompareTag("tree"))
        {
            currentHP -= damageTree;
        }
        if (obstacle.CompareTag("cone"))
        {
            currentHP -= damageCone;

        }
        if (obstacle.CompareTag("Finish"))
        {
            currentHP -= damagePed;
        }




    }

         void Update()
        {


        if (IsCarUpsideDown())
        {
            if (!isStreetDamaging)
            {
                StartCoroutine(DamageOverTime());
            }
        }
        else
        {
            if (isStreetDamaging)
            {
                StopCoroutine(DamageOverTime());
                isStreetDamaging = false;
            }
        }

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


    bool IsCarUpsideDown()
    {
        
        float upsideDownThreshold = 160f;
        return Mathf.Abs(transform.up.y) < Mathf.Cos(Mathf.Deg2Rad * upsideDownThreshold);
    }

    IEnumerator DamageOverTime()
    {
        isStreetDamaging = true;

        while (IsCarUpsideDown())
        {
            
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hit, 1f))
            {
                if (hit.collider.CompareTag("street"))
                {
                    
                    currentHP -= damageStreet;
                    
                }
            }
            else
            {
                
                isStreetDamaging = false;
            }

          
            yield return new WaitForSeconds(checkInterval);
        }

        isStreetDamaging = false; 
    }

}


