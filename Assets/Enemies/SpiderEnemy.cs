using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : BaseEnemy, IAttacker
{

    public void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Elevator");
        nav.SetDestination(target.transform.position);
        isAttacking = false;
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public override void Attack(GameObject toDamage)
    {
        toDamage.GetComponent<IDamagableObject>().TakeDamage(attackDamage);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            anim.SetBool("attack", true);
            nav.isStopped = true;
            isAttacking = true;
            rb.velocity = Vector3.zero;
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            anim.SetBool("attack", false);
            nav.isStopped = false;
            isAttacking = false;
        }
    }

}
