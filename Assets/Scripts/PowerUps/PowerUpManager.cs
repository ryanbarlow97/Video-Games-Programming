using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private bool speedPowerUpActive = false;
    private bool tripleFireRatePowerUpActive = false;
    private PowerUpEventManager eventManager;
    [SerializeField] private RemainingTimeUI timerBarController;
    [SerializeField] private Image speedPowerUpImage;
    [SerializeField] private Image tripleFireRatePowerUpImage;


    private void Start()
    {
        eventManager = FindObjectOfType<PowerUpEventManager>();
        eventManager.StartListening("SpeedPowerUp", ActivateSpeedPowerUp);
        eventManager.StartListening("TripleFireRatePowerUp", ActivateTripleFireRatePowerUp);
    }

    private void OnDestroy()
    {
        eventManager.StopListening("SpeedPowerUp", ActivateSpeedPowerUp);
        eventManager.StopListening("TripleFireRatePowerUp", ActivateTripleFireRatePowerUp);
    }

    private void ActivateSpeedPowerUp(object powerUpObject)
    {
        if (!speedPowerUpActive && !tripleFireRatePowerUpActive && powerUpObject is SpeedPowerUp powerUp)
        {
            StartCoroutine(ApplyTemporarySpeedPowerUp(powerUp));
        }
    }

    private IEnumerator ApplyTemporarySpeedPowerUp(SpeedPowerUp speedPowerUp)
    {
        speedPowerUpActive = true;
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();

        playerMovement.acceleration *= speedPowerUp.speedMultiplier;

        timerBarController.SetMaxTime(speedPowerUp.duration);
        float timeElapsed = 0f;
        speedPowerUpImage.enabled = true;


        while (timeElapsed < speedPowerUp.duration)
        {
            timeElapsed += Time.deltaTime;
            timerBarController.SetCurrentTime(speedPowerUp.duration - timeElapsed);
            yield return null;
        }

        playerMovement.acceleration /= speedPowerUp.speedMultiplier;
        speedPowerUpActive = false;
        speedPowerUpImage.enabled = false;

    }


    private void ActivateTripleFireRatePowerUp(object powerUpObject)
    {
        if (!tripleFireRatePowerUpActive && !speedPowerUpActive && powerUpObject is TripleFireRatePowerUp powerUp)
        {
            StartCoroutine(ApplyTemporaryTripleFireRatePowerUp(powerUp));
        }
    }


    private IEnumerator ApplyTemporaryTripleFireRatePowerUp(TripleFireRatePowerUp tripleFireRatePowerUp)
    {
        tripleFireRatePowerUpActive = true;
        WeaponSystem weaponSystem = GetComponent<WeaponSystem>();

        weaponSystem.fireRate /= tripleFireRatePowerUp.fireRateMultiplier;

        timerBarController.SetMaxTime(tripleFireRatePowerUp.duration);
        float timeElapsed = 0f;
        tripleFireRatePowerUpImage.enabled = true;

        while (timeElapsed < tripleFireRatePowerUp.duration)
        {
            timeElapsed += Time.deltaTime;
            timerBarController.SetCurrentTime(tripleFireRatePowerUp.duration - timeElapsed);
            yield return null;
        }

        weaponSystem.fireRate *= tripleFireRatePowerUp.fireRateMultiplier;
        tripleFireRatePowerUpActive = false;
        tripleFireRatePowerUpImage.enabled = false;
    }

    public bool IsAnyPowerUpActive()
    {
        return speedPowerUpActive || tripleFireRatePowerUpActive;
    }
}
