using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform orientation;

    private float3 mouseOnGroundPosition;
    private RaycastHit raycastHit;
    private Ray ray;

    private void Update()
    {
        orientation.LookAt(GetPointOnGround(), Vector3.up);
    }

    public Vector3 GetPointOnGround()
    {
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out raycastHit, 100f, whatIsGround))
        {
            mouseOnGroundPosition = raycastHit.point;
        }

        return mouseOnGroundPosition;
    }
}
