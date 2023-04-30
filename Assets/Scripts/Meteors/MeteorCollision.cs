using UnityEngine;

public class MeteorCollision : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject[] smallMeteorPrefabs;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Destroy the bullet
            Destroy(other.gameObject);

            // Spawn an explosion at the meteor's position
            GameObject newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Get the bullet impact direction
            Vector2 bulletImpactDirection = (other.transform.position - transform.position).normalized;

            // Get the meteor's speed at the time of collision
            float meteorSpeed = GetComponent<Rigidbody2D>().velocity.magnitude;

            // Spawn smaller meteors
            ICommand spawnSmallerMeteorsCommand = new SpawnSmallerMeteorsCommand(
                smallMeteorPrefabs, transform.position, meteorSpeed / 2, bulletImpactDirection, transform.localScale.x);
            spawnSmallerMeteorsCommand.Execute();

            // Destroy the meteor
            Destroy(gameObject);

            // Destroy explosion after 1.25 seconds
            Destroy(newExplosion, 0.8f);
        }

        if (other.CompareTag("PlayerShip"))
        {
            Debug.Log("PlayerLost");
        }
    }
}
