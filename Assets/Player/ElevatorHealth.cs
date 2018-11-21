using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorHealth : MonoBehaviour, IDamagableObject {

    public float health = 1000f;
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            print("fin");
            Application.Quit();
            Debug.DebugBreak();
        }
	}
}
