using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatePlayer : MonoBehaviour
{
    public InputAction rotateInputAction;
    float rotateInput;

    [SerializeField] float speedBase;
    [SerializeField] float speedPerMultiplier = 1;
    int currentScoreMultiplier = 1;

    private void Start()
    {
        rotateInputAction.Enable();
        rotateInputAction.performed += OnRotateInput;
        rotateInputAction.canceled += OnRotateInput;

        Score.onScoreMultiplierChanged += ScoreMultiplierChanged;
    }

    private void Update()
    {
        Move();
    }

    private void OnDestroy()
    {
        rotateInputAction.performed -= OnRotateInput;
        rotateInputAction.canceled -= OnRotateInput;
        rotateInputAction.Disable();

        Score.onScoreMultiplierChanged -= ScoreMultiplierChanged;
    }

    private void Move()
    {
        transform.RotateAround(transform.position, Vector3.up, rotateInput * (speedBase + speedPerMultiplier * currentScoreMultiplier) * Time.deltaTime);
    }

    void OnRotateInput(InputAction.CallbackContext context)
    {
        rotateInput = context.ReadValue<float>();
    }

    void ScoreMultiplierChanged(int value)
    {
        currentScoreMultiplier = value;
    }
}
