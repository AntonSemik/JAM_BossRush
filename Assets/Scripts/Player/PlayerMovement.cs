using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction moveVectorInputAction;
    public InputAction dashInputAction;
    Vector2 moveInput;

    [SerializeField] float speedBase;
    float speedMultiplier = 1;

    [Header("Dash")]
    [SerializeField] float dashSpeed = 30;
    [SerializeField] int dashDamage = 15;
    [SerializeField] float dashTime = 0.1f;
    [SerializeField] float dashReload = 3.0f;
    [SerializeField] DashSphere dashSphere;
    bool isDashing = false;
    float dashingTimer;
    float dashReloadTimer;

    Rigidbody RB;

    private void Awake()
    {
        moveVectorInputAction.performed += OnMoveVectorInput;
        moveVectorInputAction.canceled += OnMoveVectorInput;
        dashInputAction.performed += OnDashInput;

        UpgradeRewards.onUpgrade += MakeUpgrade;
    }

    private void Start()
    {
        RB = GetComponent<Rigidbody>();

        moveVectorInputAction.Enable();
        dashInputAction.Enable();

        dashSphere.dashDamage = dashDamage;
    }

    private void FixedUpdate()
    {

        if (isDashing)
        {
            RB.velocity = transform.forward * dashSpeed;

            if (dashingTimer <= 0)
            {
                isDashing = false;
                dashSphere.gameObject.SetActive(false);
            }

            dashingTimer -= Time.deltaTime;

            return;
        }

        Move();

        if(dashReloadTimer > 0) dashReloadTimer -= Time.deltaTime;
    }

    private void OnDestroy()
    {
        moveVectorInputAction.performed -= OnMoveVectorInput;
        moveVectorInputAction.canceled -= OnMoveVectorInput;
        moveVectorInputAction.Disable();

        dashInputAction.performed -= OnDashInput;
        dashInputAction.Disable();

        UpgradeRewards.onUpgrade -= MakeUpgrade;
    }

    private void Move()
    {
        if (isDashing) return;

        RB.velocity = (new Vector3(moveInput.x,0,moveInput.y).normalized * (speedBase * speedMultiplier));
    }

    void Dash()
    {
        isDashing = true;

        dashingTimer = dashTime;
        dashReloadTimer = dashReload;

        dashSphere.gameObject.SetActive(true);
    }

    void OnMoveVectorInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void OnDashInput(InputAction.CallbackContext context)
    {
        if (dashReloadTimer > 0 || isDashing) return;

        Dash();
    }

    #region upgrades

    void MakeUpgrade(string name)
    {
        switch (name)
        {
            case "movespeed":
                UpgradeMoveSpeed();
                break;

            case "dashspeed":
                UpgradeDashSpeed();
                break;

            case "dashtime":
                UpgradeDashTime();
                break;

            case "dashdamage":
                UpgradeDashDamage();
                break;
        }
    }

    void UpgradeMoveSpeed()
    {
        speedMultiplier += 0.1f;
    }

    void UpgradeDashSpeed()
    {
        dashSpeed *= 1.1f;
    }
    void UpgradeDashTime()
    {
        dashTime += 0.02f;
    }

    void UpgradeDashDamage()
    {
        dashSphere.dashDamage += 5;
    }

    #endregion
}
