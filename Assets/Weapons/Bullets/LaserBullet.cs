using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour {
    public float damage = 25f;
    public int multiplier = 1;
    public float lifeSpan = 250f;
    public GameObject bulletMark;
    public static List<GameObject> bulletMarks;
    private readonly int maxMarks = 10;
    private static int currentMark = 0;

    private void Start()
    {
        Destroy(gameObject, lifeSpan);
        if(bulletMarks==null||bulletMarks.Count == 0)
        {
            CreateBulletMarks();
        }
    }

    private void CreateBulletMarks()
    {
        bulletMarks = new List<GameObject>();
        for (int i = 0; i < maxMarks; i++)
        {
            GameObject newBulletMark = Instantiate(bulletMark);
            newBulletMark.SetActive(false);
            bulletMarks.Add(newBulletMark);
        }
        currentMark = 0;
    }

    public float GetDamage()
    {
        return damage * multiplier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject mark = bulletMarks[currentMark];
        if (collision.gameObject.tag.Equals("Surface"))
        {
            mark.transform.position = collision.contacts[0].point;
            mark.transform.rotation = Quaternion.FromToRotation(Vector3.forward, collision.contacts[0].normal);
            mark.SetActive(true);
            currentMark = (currentMark+1) % maxMarks;

        }

        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Attack(collision.gameObject);
        }
    }

    public void Attack(GameObject toDamage)
    {
        toDamage.GetComponent<IDamagableObject>().TakeDamage(damage);
    }
}
