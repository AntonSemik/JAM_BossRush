using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveToCursorAxis : MonoBehaviour
{
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform orientation;

    [SerializeField] bool moveOnX;
    [SerializeField] bool moveOnZ;

    private Vector3 newPosition;
    private float3 mouseOnGroundPosition;
    private RaycastHit raycastHit;
    private Ray ray;

    private void Update()
    {
        CalculatePosition();
        orientation.position = newPosition;
    }

    public Vector3 CalculatePosition()
    {
        newPosition = orientation.position;

        if (moveOnX) newPosition.x = GetPointOnGround().x;
        if (moveOnZ) newPosition.z = GetPointOnGround().z;

        return newPosition;
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
