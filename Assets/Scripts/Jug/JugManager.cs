using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class JugManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Transform jugContainer = default;
    [SerializeField] private Transform shakeContainer = default;
    [SerializeField] private Transform jug = default;
    [SerializeField] private Transform jugPoint = default;
    [SerializeField] private Transform _particleSystem = default;

    [SerializeField] private Transform colliderOnCollideTopLeft = default;
    [SerializeField] private Transform colliderOnCollideBottomRight = default;

    [SerializeField] private AnimationCurve shakeCurve = default;

    [SerializeField] private WaterDrop waterDropPrefab = default;
    private List<WaterDrop> waterDrops;

    private Camera _camera = default;

    [Header("Values")]
    [SerializeField] private float maxJugHeight = 0f;
    [SerializeField] private float minJugHeightOnCollide = 0f;

    [SerializeField] private float followingSpeed = 0f;
    [SerializeField] private float moveJugUpSpeed = 0f;
    [SerializeField] private float rotateSpeed = 0f;

    [SerializeField] private float angleMaxRotation = 0f;

    [SerializeField] private float nMinWaterDrop = 0f;
    [SerializeField] private float nMaxWaterDrop = 0f;

    [SerializeField] private float waterDropOffset = 0f;

    [SerializeField] private Tweener shakeAtStart;

    [Header("ShakeValues")]
    [SerializeField] private int nShake = 0;
    [SerializeField] private float shakeTime = 0f;
    [SerializeField] private float shakeForceStart = 0f;
    [SerializeField] private float shakeForceEnd = 0f;
    [SerializeField] private int shakeVibratoStart = 0;
    [SerializeField] private int shakeVibratoEnd = 0;

    [SerializeField] private Vector3 minAngleVectorDirectionWaterDrop = Vector3.zero;
    [SerializeField] private Vector3 maxAngleVectorDirectionWaterDrop = Vector3.zero;
    [SerializeField] private float waterForce = 0f;
    [SerializeField] private float minWaterForce = 0f;
    [SerializeField] private float maxWaterForce = 0f;

    private float elapsedShakeTimeFromStart = 0f;
    private Quaternion targetRotation;
    private Vector3 lastPositionShakeContainer;

    private Action DoAction;

    #region Unity Methods
    private void Start()
    {
        SetModeVoid();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        jug.rotation.ToAngleAxis(out float currentAngle, out Vector3 axis);
        targetRotation = Quaternion.AngleAxis(currentAngle + angleMaxRotation, axis);

        _camera = LevelManager.GetCamera();

        waterDrops = new List<WaterDrop>();

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);

        SetModePlay();

        shakeAtStart = shakeContainer.DOShakePosition(1f, 0.05f, 10, 90f, false, false, ShakeRandomnessMode.Harmonic).SetLoops(-1);
        shakeAtStart.Play();
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

    private void DoActionVoid() {}

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
        elapsedShakeTimeFromStart += Time.deltaTime;
        SpawnParticle();
    }
    #endregion

    #region Part1
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
        {
            shakeAtStart.Kill();
            StartShake();
            SetModeShake();
        }
    }

    private void RotateJug()
    {
        jug.rotation = Quaternion.RotateTowards(jug.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        if (jug.rotation == targetRotation)
        {
            shakeAtStart.Kill();
            StartShake();
            SetModeShake();

            lastPositionShakeContainer = shakeContainer.position;
        }
    }
    #endregion

    #region Shake
    private void StartShake()
    {
        Sequence sequence = DOTween.Sequence();

        for (int i = 1; i <= nShake; i++)
        {
            sequence.Append(OnShake(i));
        }

        sequence.Append(OnShake(nShake, 5));

        sequence.OnComplete(EndLevel);

        sequence.Play();
    }

    private Tween OnShake(int index, float time = 0)
    {
        float shakeValue = shakeCurve.Evaluate((float)index / (float)nShake);

        return shakeContainer.DOShakePosition(
            shakeTime / nShake,
            GetShakeForce(shakeValue),
            GetShakeVibrato(shakeValue),
            90f,
            false,
            false,
            ShakeRandomnessMode.Harmonic);
    }

    private float GetShakeForce(float shakeValue)
        => (shakeForceEnd - shakeForceStart) * shakeValue + shakeForceStart;

    private int GetShakeVibrato(float shakeValue)
        => Mathf.RoundToInt((shakeVibratoEnd - shakeVibratoStart) * shakeValue + shakeVibratoStart);
    #endregion

    private void SpawnParticle()
    {
        float shakeValue = shakeCurve.Evaluate(elapsedShakeTimeFromStart / shakeTime);

        int nWaterDrops = Mathf.RoundToInt((nMaxWaterDrop - nMinWaterDrop) * shakeValue + nMinWaterDrop);

        Vector3 currentVelocity = lastPositionShakeContainer - shakeContainer.position;

        WaterDrop waterDrop;
        Vector3 direction;
        float force;

        float randomAngle;

        if (Vector3.Dot(currentVelocity, minAngleVectorDirectionWaterDrop) > 0 &&
            Vector3.Dot(currentVelocity, maxAngleVectorDirectionWaterDrop) > 0)
        {
            for (int i = 0; i < nWaterDrops; i++)
            {
                randomAngle = UnityEngine.Random.Range(0, 360);

                waterDrop = Instantiate(waterDropPrefab, _particleSystem.position, Quaternion.AngleAxis(randomAngle, Vector3.forward));

                direction = Quaternion.AngleAxis(UnityEngine.Random.Range(-waterDropOffset, waterDropOffset), Vector3.forward) * currentVelocity;

                force = Mathf.Clamp(shakeCurve.Evaluate(elapsedShakeTimeFromStart / shakeTime) * waterForce, minWaterForce, maxWaterForce);

                waterDrop.rigidBody.AddForce(direction.normalized * force, ForceMode2D.Impulse);

                waterDrops.Add(waterDrop);
            }
        }

        lastPositionShakeContainer = shakeContainer.position;
    }

    private void EndLevel()
    {
        SetModeVoid();
        Cursor.visible = true;
        LevelManager.Instance.NextLevel();
    }
}
