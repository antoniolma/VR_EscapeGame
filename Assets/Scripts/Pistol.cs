using UnityEngine;

public class Pistol : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float bulletLifetime = 5f; // seconds before bullet is destroyed
    public float numBullets = 6f;
    public bool reloadOneBullet = false;

    [Header("Audio")]
    public AudioClip shootSound;
    public AudioClip failSound;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (reloadOneBullet)
        {
            numBullets++;
            reloadOneBullet = false;
        }
    }

    public void PullTrigger()
    {
        if (numBullets > 0)
        {
            FireBullet();
            numBullets--;
        }
        else
        {
            FireFail();
        }
    }

    public void FireBullet()
    {
        // Spawn bullet
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        // Apply velocity
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
            rb.useGravity = false;
        }

        // Play gun sound
        if (shootSound != null && source != null)
        {
            source.PlayOneShot(shootSound);
        }

        // Destroy bullet after time
        Destroy(bullet, bulletLifetime);
    }

    public void FireFail()
    {
        if (failSound != null && source != null)
        {
            source.PlayOneShot(failSound);
        }
    }
}