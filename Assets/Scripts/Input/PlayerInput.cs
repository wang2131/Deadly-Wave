using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerActions inputActions;

    [SerializeField]private float playerJumperBufferTime;
    private WaitForSeconds waitJumpInputBufferTimer;

    public bool HasJumpInputBuffer { get; set; }
    public bool isMove => inputActions.PlayerActionMap.Move.ReadValue<float>() != 0f;

    public float moveAxes
    {
        get {return inputActions.PlayerActionMap.Move.ReadValue<float>() >= 0 ? 1f : -1f; }
    }

    public bool isJump => inputActions.PlayerActionMap.Jump.WasPressedThisFrame();

    public bool stopJump => inputActions.PlayerActionMap.Jump.WasReleasedThisFrame();

    public bool isAttack => inputActions.PlayerActionMap.Attack.WasPressedThisFrame();

    public bool isSlid => inputActions.PlayerActionMap.Slid.WasPressedThisFrame();

    public bool isBackStep => inputActions.PlayerActionMap.BackStep.WasPressedThisFrame();

    private void Awake()
    {
        inputActions = new PlayerActions();
        waitJumpInputBufferTimer = new WaitForSeconds(playerJumperBufferTime);
    }

    private void OnEnable()
    {
        SetActionMapEnabled();
    }

    private void OnDisable()
    {
        SetActionDisabled();
    }

    public void SetActionMapEnabled()
    {
        inputActions.Enable();
    }

    public void SetActionDisabled()
    {
        inputActions.Disable();
    }

    public void SetJumpInputBufferTimer()
    {
        StopCoroutine(JumpInputBufferCoroutine());
        StartCoroutine(JumpInputBufferCoroutine());
    }

    private IEnumerator JumpInputBufferCoroutine()
    {
        HasJumpInputBuffer = true;
        yield return waitJumpInputBufferTimer;
        HasJumpInputBuffer = false;
    }

}
