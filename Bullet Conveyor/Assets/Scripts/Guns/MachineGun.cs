using UnityEngine;

public class MachineGun : Gun
{
    public override void Shoot(GameObject bulletGO)
    {
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bulletGO.transform.position = firePoint.position;
        bulletGO.transform.rotation = firePoint.rotation;

        bullet.enabled = true;
        bulletGO.GetComponent<Collider>().isTrigger = true;

        if (bullet != null)
        {
            TryGiveEffect(bullet);

            bullet.AutoDestroy();
            bullet.damage = gunDamage;

            bulletRb = bulletGO.GetComponent<Rigidbody>();
            bulletRb.isKinematic = false;
            bulletRb.useGravity = false;

            direction = target.position - bullet.transform.position;
            direction.y += yEnemyOffSet;

            bulletRb.velocity = direction.normalized * bulletSpeed;
        }
    }
}
