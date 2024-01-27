using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class JugManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Transform jugContainer = default;
    [SerializeField] private Transform jug = default;
    [SerializeField] private Transform hand = default;
    [SerializeField] private Transform arm = default;

    [SerializeField] private Transform colliderOnShakeTopLeft = default;
    [SerializeField] private Transform colliderOnShakeBottomRight = default;

    [SerializeField] private Camera _camera = default;

    [Header("Values")]
    [SerializeField] private float followingSpeed = 0f;
    [SerializeField] private float rotateSpeed = 0f;

    [SerializeField] private float angleMaxRotation = 0f;

    private Quaternion targetRotation;
    private float timeFromStartShake = 0f;

    private Action DoAction;

    #region Unity Methods
    private void Start()
    {
        SetModePlay();

        //Cursor.lockState = CursorLockMode.Confined;

        //jug.rotation.ToAngleAxis(out float currentAngle, out Vector3 axis);
        //targetRotation = Quaternion.AngleAxis(currentAngle + angleMaxRotation, axis);
    }

    private void Update()
    {
        DoAction();
    }
    #endregion

    #region State Machine
    public void SetModeVoid()
    {
        DoAction = DoActionVoid;
    }

    public void SetModePlay()
    {
        DoAction = DoActionPlay;
    }

    public void SetModeRotateJug()
    {
        DoAction = DoActionRotateJug;
    }

    public void SetModeShake()
    {
        DoAction = DoActionShake;
    }

    private void DoActionVoid() { }

    private void DoActionPlay()
    {
        FollowMouse();
        CheckIfJugCollides();
    }

    private void DoActionRotateJug()
    {
        RotateJug();
    }

    private void DoActionShake()
    {
        Shake();

        timeFromStartShake += Time.deltaTime;
    }
    #endregion

    private void FollowMouse()
    {
        Vector2 screenMousePosition = Input.mousePosition;
        Vector2 worldMousePosition = _camera.ScreenToWorldPoint(screenMousePosition);

        jugContainer.position = Vector3.MoveTowards(jugContainer.position, worldMousePosition, followingSpeed * Time.deltaTime);
    }

    private void CheckIfJugCollides()
    {
        if (jug.position.x <= colliderOnShakeBottomRight.position.x &&
            jug.position.x >= colliderOnShakeTopLeft.position.x &&
            jug.position.y <= colliderOnShakeTopLeft.position.y &&
            jug.position.y >= colliderOnShakeBottomRight.position.y)
        {
            SetModeRotateJug();
        }
    }

    private void RotateJug()
    {
        jug.rotation = Quaternion.RotateTowards(jug.rotation, targetRotation, rotateSpeed * Time.deltaTime);


        Debug.Log("a");
        //At end
        //SetModeShake();
    }

    private void Shake()
    {

    }
}
