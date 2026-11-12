
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.25f;

    private float nextFireTime;

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            ShootBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void ShootBullet()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (mousePosition - firePoint.position).normalized;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );

        bullet.GetComponent<Bullet>().SetDirection(direction);
    }
}
