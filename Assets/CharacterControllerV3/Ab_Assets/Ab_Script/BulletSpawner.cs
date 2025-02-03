using BoingKit;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner Instance;
    THEBADDEST.CharacterController3.CharacterController characterController;


    public GameObject bulletPrefab;
    public Transform spawnBulletPos;
    public float spawnBulletInterval = 1f;  // Time interval between spawns
    public float bulletSpeed = 10f;  // Speed of the bullet
    public float bulletDestroyTime;

    private void Awake()
    {
        Instance = this;
    }

    public void CallStartFunction()
    {
        // Invoke the SpawnBullet function repeatedly with the specified interval
        InvokeRepeating("SpawnBullet", 0f, spawnBulletInterval);
    }

    // Function to instantiate a bullet at the given position
    void SpawnBullet()
    {
        // Instantiate a bullet with the position and rotation of spawnBulletPos
        GameObject bullet = Instantiate(bulletPrefab, spawnBulletPos.position, spawnBulletPos.rotation);

        Destroy(bullet, bulletDestroyTime);

        // Get the bullet's rigidbody (assuming it has one)
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            // Set the bullet's velocity to move forward at the specified speed
            bulletRb.velocity = bullet.transform.forward * bulletSpeed;
        }
    }

    public void StopBulletSpawning()
    {
        // Stop invoking the SpawnBullet function
        CancelInvoke("SpawnBullet");
    }
}
