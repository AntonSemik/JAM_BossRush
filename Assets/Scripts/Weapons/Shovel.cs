using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shovel : MonoBehaviour
{
    [SerializeField] InputAction onAttackInput;

    [SerializeField] Transform shovelModel;
    [SerializeField] AnimationCurve shovelSwipeCurve;
    [SerializeField] float swipeTime;

    [SerializeField] float attackReloadSeconds; [SerializeField] float reloadUpgradeFactor;
    [SerializeField] int damage; [SerializeField] int damageUpgrade;

    [SerializeField] LayerMask whatCanBeHit;
    [SerializeField] Transform damageZone;
    [SerializeField] ParticleSystem attackVFX;
    [SerializeField] AudioSource swingsound;

    private float attackReloadTimer;
    private float swipeCurvePoint;
    private bool isSwiping = false;

    IsKillable tempKillable;

    private void Awake()
    {
        onAttackInput.Enable();
        onAttackInput.performed += OnAttackInput;
        UpgradeRewards.onUpgrade += MakeUpgrade;
    }

    private void OnDestroy()
    {
        onAttackInput.performed -= OnAttackInput;
        onAttackInput.Disable();
        UpgradeRewards.onUpgrade -= MakeUpgrade;
    }

    private void Update()
    {
        if (attackReloadTimer > 0) attackReloadTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (isSwiping)
        {
            SwipeBlade();
        }
    }

    void StartAttack()
    {
        isSwiping = true;
        attackReloadTimer = attackReloadSeconds;
        attackVFX.Play();

        swipeCurvePoint = 0;
        shovelModel.localRotation = Quaternion.Euler(0, shovelSwipeCurve.Evaluate(swipeCurvePoint), -130);

        swingsound.Play();

        foreach (Collider collider in Physics.OverlapSphere(damageZone.position + new Vector3(-0.14f, 0, 0.6f), 0.7f, whatCanBeHit))
        {
            DoDamage(collider.gameObject);
        }
    }

    void SwipeBlade()
    {
        swipeCurvePoint += Time.fixedDeltaTime / swipeTime;

        if (swipeCurvePoint >= 1)
        {
            swipeCurvePoint = 0;
            isSwiping = false;
        }

        shovelModel.localRotation = Quaternion.Euler(0, shovelSwipeCurve.Evaluate(swipeCurvePoint), -130);
    }

    void DoDamage(GameObject target)
    {
        tempKillable = target.GetComponent<IsKillable>();

        if (tempKillable != null)
        {
            tempKillable.TakeDamage(damage, 0);
        }

        tempKillable = null;
    }

    void OnAttackInput(InputAction.CallbackContext context)
    {
        if (isSwiping) return;
        if (attackReloadTimer > 0) return;

        StartAttack();
    }

    #region upgrades

    void MakeUpgrade(string name)
    {
        switch (name)
        {
            case "attackrate":
                UpgradeAttackRate();
                break;

            case "shoveldamage":
                UpgradeShovelDamage();
                break;
        }
    }

    void UpgradeAttackRate()
    {
        attackReloadSeconds *= reloadUpgradeFactor;
    }

    void UpgradeShovelDamage()
    {
        damage += damageUpgrade;
    }

    #endregion

}
