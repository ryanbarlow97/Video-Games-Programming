using UnityEngine;

public class SpawnSmallerMeteorsCommand : ICommand
{
    private GameObject[] smallMeteorPrefabs;
    private Vector3 spawnPosition;
    private float smallMeteorSpeed;
    private Vector2 bulletImpactDirection;
    private float parentScale;

    public SpawnSmallerMeteorsCommand(GameObject[] smallMeteorPrefabs, Vector3 spawnPosition, float smallMeteorSpeed, Vector2 bulletImpactDirection, float parentScale)
    {
        this.smallMeteorPrefabs = smallMeteorPrefabs;
        this.spawnPosition = spawnPosition;
        this.smallMeteorSpeed = smallMeteorSpeed;
        this.bulletImpactDirection = bulletImpactDirection;
        this.parentScale = parentScale;
    }

    public void Execute()
    {
        if (smallMeteorPrefabs != null && smallMeteorPrefabs.Length == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject smallMeteor = GameObject.Instantiate(smallMeteorPrefabs[i], spawnPosition, Quaternion.identity);

                // Scale the small meteor to be 1/4 the size of the parent meteor
                smallMeteor.transform.localScale = parentScale * smallMeteor.transform.localScale;

                Rigidbody2D smallMeteorRigidbody = smallMeteor.GetComponent<Rigidbody2D>();

                // Calculate the base angle in degrees
                float baseAngle = Mathf.Atan2(bulletImpactDirection.y, bulletImpactDirection.x) * Mathf.Rad2Deg + 180f;

                // Calculate a random angle within the 90-degree range
                float angle = baseAngle - 45f + Random.Range(0f, 90f);

                // Convert the angle to a direction vector
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

                // Add randomness to the small meteor speed
                float randomizedSpeed = smallMeteorSpeed * Random.Range(0.8f, 1.2f);

                // Set the direction and speed for the small meteor
                smallMeteorRigidbody.velocity = direction * randomizedSpeed;

                // Add random angular velocity to the small meteor
                smallMeteorRigidbody.angularVelocity = Random.Range(-100f, 100f);
            }
        }
    }
}
