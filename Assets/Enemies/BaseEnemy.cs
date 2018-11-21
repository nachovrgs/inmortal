using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy :  MonoBehaviour, IDamagableObject
{
    public float health = 100f;
    public int attackDamage;
    public float timeBetweenAttacks = 5000f;
    public bool isDead;

    protected Rigidbody rb;
    protected UnityEngine.AI.NavMeshAgent nav;
    protected GameObject target;
    protected bool isAttacking = false;
    protected Animator anim;
    

    float deltaTime;


    void Update()
    {
        anim.SetFloat("walking", rb.velocity.magnitude);
        deltaTime += Time.deltaTime;
        if (this.health <= 0 && !isDead)
        {
            isDead = true;
            nav.isStopped = true;
            anim.SetBool("attack", false);
            anim.SetBool("die", true);
            FindObjectOfType<EnemySpawner>().spawnedEnemies--;
            Destroy(gameObject,2f);
        }
        if (isAttacking && deltaTime >= timeBetweenAttacks)
        {
            deltaTime = 0;
            Attack(target);
        }
    }
    
    

    public virtual void Attack(GameObject toDamage)
    {
        toDamage.GetComponent<ElevatorHealth>().TakeDamage(attackDamage);
    }

    public void TakeDamage(float damage)
    {
        anim.SetBool("hit", true);
        nav.isStopped = true;
        health -= damage;
        nav.isStopped = false;
        anim.SetBool("hit", false);
    }
    
}
