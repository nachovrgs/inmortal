using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class LaserGun : MonoBehaviour,FireArm,IAttacker {

    public VRTK_InteractableObject linkedObject;
    public GameObject laser;
    public Transform barrelEnd;
    public int pulse = 1500;
    public int damageMultiplier = 1;
    public float fireSpeed = 5000f;
    public int rounds = 10;
    public float attackDamage = 10f;

    protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
        }
    }

    protected virtual void OnDisable()
    {
        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
        }
    }

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        Fire();
    }

    public void Fire()
    {
        if (rounds > 0)
        {
            GetComponent<AudioSource>().Play();
            GameObject laserInstance = Instantiate(laser, barrelEnd.position, barrelEnd.rotation);
            laserInstance.GetComponent<LaserBullet>().multiplier = damageMultiplier;
            laserInstance.GetComponent<Rigidbody>().AddForce(barrelEnd.forward * fireSpeed);
            rounds--;
        }
    }

    public void Reload(int ammo)
    {
        throw new System.NotImplementedException();
    }

    public ushort PulseDuration()
    {
        return (ushort)pulse;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Attack(collision.gameObject);
        }
    }

    public void Attack(GameObject toDamage)
    {
        toDamage.GetComponent<IDamagableObject>().TakeDamage(attackDamage);
    }
}
