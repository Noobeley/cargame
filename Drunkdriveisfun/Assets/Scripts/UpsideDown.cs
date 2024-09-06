using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpsideDown : MonoBehaviour
{
    public float slightTiltThreshold = 70f;
    public float severeTiltThreshold = 110f; 
    public float currentHP = 100f;
    public float slightTiltDamageAmount = 5f; 
    public float severeTiltDamageAmount = 10f; 
   
    public float checkInterval = 1f; 

    private bool isDamaging = false;
    private float currentDamageAmount;

    void Update()
    {
        float carUpAngle = Vector3.Angle(transform.up, Vector3.up); 

        if (carUpAngle >= severeTiltThreshold)
        {
           
            HandleDamage(true); 
        }
        else if (carUpAngle >= slightTiltThreshold)
        {
            
            HandleDamage(false); 
        }
        else
        {
           
            StopDamage(); 
        }
    }

    void HandleDamage(bool isSevere)
    {
       
        float damageAmount = isSevere ? severeTiltDamageAmount : slightTiltDamageAmount;
        StartCoroutine(DamageOverTime(damageAmount));
    }

    void StopDamage()
    {
       
        StopCoroutine("DamageOverTime");
    }

    IEnumerator DamageOverTime(float damageAmount)
    {
        while (true)
        {
           
            currentHP -= damageAmount;

           
            float carUpAngle = Vector3.Angle(transform.up, Vector3.up);
            if (carUpAngle < slightTiltThreshold)
            {
               
                StopDamage();
                yield break;
            }

           
            yield return new WaitForSeconds(checkInterval);
        }
    }
}
