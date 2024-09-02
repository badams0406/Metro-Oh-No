using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Need this for the Input System
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject currentWeapon;
    
    private Vector3 mouseLook;
    private Vector3 rotationTarget;
    private Vector3 movement1 = Vector3.zero;

    private Animator animator;

    // input fields
    private PlayerInputActions playerActionsAssets;
    private InputAction move;

    // movemnet fields
    private Rigidbody rb;
    [SerializeField] private float movementForce = 1f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float maxSpeed = 5f;

    private Vector3 forceDirection = Vector3.zero;
    [SerializeField] private Camera playerCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerActionsAssets = new PlayerInputActions();
        animator = GetComponent<Animator>();


    }

    private void OnEnable()
    {
        playerActionsAssets.Player.Jump.started += DoJump;
        playerActionsAssets.Player.Attack.started += DoAttack;
        move = playerActionsAssets.Player.Move;
        playerActionsAssets.Player.Enable();
    }


    private void OnDisable()
    {
        playerActionsAssets.Player.Jump.started -= DoJump;
        playerActionsAssets.Player.Attack.started -= DoAttack;
        playerActionsAssets.Player.Disable();
    }


    private void DoJump(InputAction.CallbackContext obj)
    {
        if (IsGrounded())
        {
            forceDirection = Vector3.up * jumpForce;
        }
    }

    private void DoAttack(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("attack");
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f))
            return true;
        else
            return false;
    }

    public void OnMouseLook(InputAction.CallbackContext obj)
    {
        mouseLook = obj.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mouseLook);

        if (Physics.Raycast(ray, out hit))
        {
            rotationTarget = hit.point;
        }

        MovePlayerWithAim();

        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0f)
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.deltaTime;

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;

    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }


    private void MovePlayerWithAim()
    {
        var lookPos = rotationTarget - rb.position;
        lookPos.y = 0f;
        var rotation = Quaternion.LookRotation(lookPos);

        Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.z).normalized;

        if (!aimDirection.Equals(Vector3.zero))
        {
            rb.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }
    }

    public void StartDealDamage()
    {
        currentWeapon.GetComponentInChildren<DamageDealer>().StartDealDamage();
    }

    public void EndDealDamage()
    {
        currentWeapon.GetComponentInChildren<DamageDealer>().EndDealDamage();
    }
}
