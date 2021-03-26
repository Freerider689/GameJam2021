using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : Tower
{
    public GameObject projectilePrefab;
    public Transform canonOrigin;

    public override void Attack()
    {
        Debug.Log("SHOOT SHOOT MOTHERFUCKER!");
        GameObject projectileGameObject = (GameObject)Instantiate(projectilePrefab, canonOrigin.position, canonOrigin.rotation);
        Projectile projectile = projectileGameObject.GetComponent<Bullet>();

        if (projectile != null)
            projectile.Seek(_target);
    }
}
