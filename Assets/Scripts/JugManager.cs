using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class JugManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Camera _camera = default;
    
    [SerializeField] private Transform jugContainer = default;
    [SerializeField] private Transform shakeContainer = default;
    [SerializeField] private Transform jug = default;
    [SerializeField] private Transform jugPoint = default;
    [SerializeField] private Transform hand = default;
    [SerializeField] private Transform arm = default;

    [SerializeField] private Transform colliderOnCollideTopLeft = default;
    [SerializeField] private Transform colliderOnCollideBottomRight = default;

    [SerializeField] private AnimationCurve shakeEase = default;


    [Header("Values")]
    [SerializeField] private float maxJugHeight = 0f;
    [SerializeField] private float minJugHeightOnCollide = 0f;

    [SerializeField] private float followingSpeed = 0f;
    [SerializeField] private float moveJugUpSpeed = 0f;
    [SerializeField] private float rotateSpeed = 0f;

    [SerializeField] private float angleMaxRotation = 0f;

    [Header("ShakeValues")]
    [SerializeField] private int nShake = 0;
    [SerializeField] private float shakeTime = 0f;
    [SerializeField] private float shakeForceStart = 0f;
    [SerializeField] private float shakeForceEnd = 0f;
    [SerializeField] private int shakeVibratoStart = 0;
    [SerializeField] private int shakeVibratoEnd = 0;


    private Quaternion targetRotation;
    private float timeFromStartShake = 0f;

    private Action DoAction;

    #region Unity Methods
    private void Start()
    {
        SetModeVoid();

        Cursor.lockState = CursorLockMode.Confined;

        jug.rotation.ToAngleAxis(out float currentAngle, out Vector3 axis);
        targetRotation = Quaternion.AngleAxis(currentAngle + angleMaxRotation, axis);
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

    public void SetModeMoveJugUp()
    {
        DoAction = DoActionMoveJugUp;
    }

    public void SetModeRotateJug()
    {
        DoAction = DoActionRotateJug;
    }

    public void SetModeShake()
    {
        DoAction = DoActionShake;
    }

    private void DoActionVoid()
    {
        if (Input.GetMouseButtonDown(0))
            SetModePlay();
    }

    private void DoActionPlay()
    {
        FollowMouse();
        CheckIfJugCollides();
    }

    private void DoActionMoveJugUp()
    {
        MoveJugUp();
    }

    private void DoActionRotateJug()
    {
        RotateJug();
    }

    private void DoActionShake()
    {
        FollowMouse();

        //timeFromStartShake += Time.deltaTime;
    }
    #endregion

    private void FollowMouse()
    {
        Vector2 screenMousePosition = Input.mousePosition;
        Vector3 worldMousePosition = _camera.ScreenToWorldPoint(screenMousePosition) - (jugPoint.position - jug.position);
        worldMousePosition.z = 0;

        if (worldMousePosition.y > maxJugHeight)
            worldMousePosition.y = maxJugHeight;

        jugContainer.position = Vector3.MoveTowards(jugContainer.position, worldMousePosition, followingSpeed * Time.deltaTime);
    }

    private void CheckIfJugCollides()
    {
        if (jug.position.x <= colliderOnCollideBottomRight.position.x &&
            jug.position.x >= colliderOnCollideTopLeft.position.x &&
            jug.position.y <= colliderOnCollideTopLeft.position.y &&
            jug.position.y >= colliderOnCollideBottomRight.position.y)
        {
            if (jugContainer.position.y < minJugHeightOnCollide)
                SetModeMoveJugUp();
            else
                SetModeRotateJug();
        }
    }

    private void MoveJugUp()
    {
        jugContainer.position = Vector3.MoveTowards(
            jugContainer.position,
            new Vector3(jugContainer.position.x, minJugHeightOnCollide, 0),
            moveJugUpSpeed * Time.deltaTime);

        if (jugContainer.position.y >= minJugHeightOnCollide)
            SetModeRotateJug();
    }

    private void RotateJug()
    {
        jug.rotation = Quaternion.RotateTowards(jug.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        if (jug.rotation == targetRotation)
        {
            StartShake();
            SetModeShake();
        }
    }

    private void StartShake()
    {
        Sequence sequence = DOTween.Sequence();

        for (int i = 1; i <= nShake; i++)
        {
            sequence.Append(OnShake(i));
        }

        //sequence.OnComplete(EndLevel);

        sequence.Play();
    }

    private Tween OnShake(int index, float time = 0)
    {
        //if (time == 0)
            return shakeContainer.DOShakePosition(shakeTime / nShake, GetShakeForceFromIndex(index), GetShakeVibratoFromIndex(index), 90f, false, false, ShakeRandomnessMode.Harmonic);
        //else
            //return shakeContainer.DOShakePosition(time, GetShakeForceFromIndex(index), GetShakeVibratoFromIndex(index), 90f, false, false, ShakeRandomnessMode.Harmonic);
    }

    private float GetShakeForceFromIndex(int index)
    {
        float shakeValue = shakeEase.Evaluate((float)index / (float)nShake);
        return (shakeForceEnd - shakeForceStart) * shakeValue + shakeForceStart;
    }
        //=> (shakeForceEnd - shakeForceStart) / nShake * index + shakeForceStart;

    private int GetShakeVibratoFromIndex(int index)
    {
        float shakeValue = shakeEase.Evaluate((float)index / (float)shakeTime);
        //Debug.Log(shakeCurveValue * shakeVibratoStart / shakeVibratoEnd);
        return Mathf.RoundToInt((shakeVibratoEnd - shakeVibratoStart) * shakeValue + shakeVibratoStart);
    }
        //=> (shakeVibratoEnd - shakeVibratoStart) / nShake * index + shakeVibratoStart;

    private void EndLevel()
    {
        OnShake(nShake, 2);
    }


}
