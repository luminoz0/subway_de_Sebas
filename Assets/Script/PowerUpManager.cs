using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField]
    private int minPlatformsNumber = 5;

    [SerializeField]
    private int maxPlatformsNumber = 7;

    [SerializeField]
    private InstantiatePoolObjects[] powerUpPools;

    [SerializeField]
    private float powerUpOffset = 0.5f;

    private int platformsNumber;
    private int platformsCounter = 0;

    private void Awake()
    {
        SetPlatformsNumber();
    }

    private void SetPlatformsNumber()
    {
        platformsNumber = Random.Range(minPlatformsNumber, maxPlatformsNumber);
    }

    public void PlatformPassed(Platform platform)
    {
        platformsCounter++;

        if (platformsCounter >= platformsNumber)
        {
            SpawnPowerUp(platform);
            platformsCounter = 0;
            SetPlatformsNumber();
        }
    }

    private void SpawnPowerUp(Platform platform)
    {
        if (!platform.HasCoins()) return;

        InstantiatePoolObjects pool = powerUpPools[Random.Range(0, powerUpPools.Length)];
        pool.InstantiateObject(Vector3.zero);

        GameObject powerUp = pool.GetCurrentObject();

        Collider col = platform.GetComponent<Collider>();

        float yPos = platform.transform.position.y;

        if (col != null)
        {
            yPos = col.bounds.max.y;
        }

        Vector3 spawnPosition = new Vector3(
            platform.transform.position.x,
            yPos + powerUpOffset,
            platform.transform.position.z
        );

        powerUp.transform.position = spawnPosition;

        platform.AddPowerUp(powerUp);
    }
}