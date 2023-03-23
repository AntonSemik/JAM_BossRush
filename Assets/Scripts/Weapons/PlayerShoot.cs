using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public InputAction OnShootInputAction; float shootInput;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform gunpoint;

    [SerializeField] float reloadTimeBase = 2f, upgradeReloadFactor;
    [SerializeField] int maxBullets = 10, upgradeMaxAmmo = 5;
    [SerializeField] float spread = 10, upgradeSpreadFactor = 0.8f;
    [SerializeField] float bulletVelocity, upgradeVelocityFactor = 1.05f;
    [SerializeField] int bulletDamage, upgradeDamage = 10;
    [SerializeField] AudioSource shootSound;
    int currentBullets = 0;

    private int bulletsPerShot = 1;

    float timeToNextShot = 0; bool isLoaded;

    Queue<GameObject> bulletQueue = new Queue<GameObject>();
    [SerializeField] int poolSize = 20;
    GameObject tempObject;

    public delegate void SendBulletsToUI(int value, int maxvalue);
    public static SendBulletsToUI sendBulletsToUI;

    private void Awake()
    {
        OnShootInputAction.performed += OnShootInput;
        OnShootInputAction.canceled += ResetShootInput;

        SubscribeToUpgrades();
    }

    private void Start()
    {
        if(bullet == null)
        {
            Debug.LogError("Gun projectile not found");
            gameObject.SetActive(false);
        }

        SetPool();
        
        OnShootInputAction.Enable();

        AmmoBox.takeBullet += AddBullet;

        currentBullets = maxBullets;

        if (sendBulletsToUI != null) sendBulletsToUI(currentBullets, maxBullets);
    }

    private void Update()
    {
        if (!isLoaded)
        {
            timeToNextShot -= Time.deltaTime;
            if (timeToNextShot <= 0) isLoaded = true;
        }

        if(shootInput > 0 && isLoaded)
        {
            Shoot();

            timeToNextShot = reloadTimeBase;
            isLoaded = false;
        }
    }

    private void OnDestroy()
    {
        OnShootInputAction.performed -= OnShootInput;
        OnShootInputAction.canceled -= ResetShootInput;

        OnShootInputAction.Disable();

        AmmoBox.takeBullet -= AddBullet;

        UnsubscribeFromUpgrades();
    }

    private void SetPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            tempObject = Instantiate(bullet);
            tempObject.SetActive(false);

            bulletQueue.Enqueue(tempObject);
        }
    }

    private void AddBullet()
    {
        if (currentBullets < maxBullets)
        {
            currentBullets += 5;
            if(currentBullets > maxBullets) currentBullets = maxBullets;
        }

        if (sendBulletsToUI != null) sendBulletsToUI(currentBullets, maxBullets);
    }

    private void Shoot()
    {
        if (currentBullets <= 0) return;
        currentBullets--;
        if (sendBulletsToUI != null) sendBulletsToUI(currentBullets, maxBullets);

        shootSound.Play();

        for (int i = 0; i < bulletsPerShot; i++)
        {
            tempObject = bulletQueue.Dequeue();
            bulletQueue.Enqueue(tempObject);

            tempObject.transform.position = gunpoint.position;
            tempObject.transform.rotation = gunpoint.rotation;
            tempObject.transform.RotateAround(tempObject.transform.position, Vector3.up, Random.Range(-spread, spread));

            Bullet bullet = tempObject.GetComponent<Bullet>();
            bullet.startVelocity = bulletVelocity;
            bullet.damage = bulletDamage;

            tempObject.SetActive(true);
        }
    }

    private void OnShootInput(InputAction.CallbackContext context)
    {
        shootInput = context.ReadValue<float>();
    }

    private void ResetShootInput(InputAction.CallbackContext context)
    {
        shootInput = 0;
    }

    #region upgrades
    void MakeUpgrade(string name)
    {
        switch (name)
        {
            case "maxammo":
                UpgradeMaxAmmo();
                break;

            case "spread":
                UpgradeSpread();
                break;

            case "bulletspershot":
                UpgradeBulletsPerShot();
                break;

            case "firerate":
                UpgradeFirerate();
                break;

            case "damage":
                UpgradeDamage();
                break;

            case "velocity":
                UpgradeBulletVelocity();
                break;
        }
    }

    void SubscribeToUpgrades()
    {
        UpgradeRewards.onUpgrade += MakeUpgrade;
    }
    void UnsubscribeFromUpgrades()
    {
        UpgradeRewards.onUpgrade -= MakeUpgrade;
    }

    private void UpgradeMaxAmmo()
    {
        maxBullets += upgradeMaxAmmo;
        if (sendBulletsToUI != null) sendBulletsToUI(currentBullets, maxBullets);
    }
    private void UpgradeSpread()
    {
        spread *= upgradeSpreadFactor;
    }
    private void UpgradeBulletsPerShot()
    {
        bulletsPerShot++;
    }
    private void UpgradeFirerate()
    {
        reloadTimeBase *= upgradeReloadFactor;
    }

    private void UpgradeDamage()
    {
        bulletDamage += upgradeDamage;
    }

    private void UpgradeBulletVelocity()
    {
        bulletVelocity *= upgradeVelocityFactor;
    }

    #endregion
}
