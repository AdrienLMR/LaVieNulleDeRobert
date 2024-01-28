using System;
using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class RingManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Rigidbody2D rigidBody = default;

    private Camera _camera = default;

    [Header("Values")]
    [SerializeField] private Vector3 startPosition = Vector3.zero;
    [SerializeField] private float horizontalSpeed = 0f;
    [SerializeField] private float verticalForce = 0f;
    [SerializeField] private float timePutTheRing = 0f;
    [SerializeField] private float speedPutTheRing = 0f;

    private float elapsedTimePutTheRing = 0f;

    private Action DoAction;

    #region Unity Methods
    private void Start()
    {
        _camera = LevelManager.GetCamera();

        SetModeVoid();
        transform.position = startPosition;
        rigidBody.gravityScale = 0f;   
        StartCoroutine(WaitBeforePlay());
    }

    private IEnumerator WaitBeforePlay()
    {
        yield return new WaitForSeconds(1f);
        rigidBody.gravityScale = 1f;
        SetModePlay();
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

    public void SetModePutTheRing()
    {
        DoAction = DoActionPutTheRing;
    }

    private void DoActionVoid() { }

    private void DoActionPlay()
    {
        MoveHorizontal();

        if (Input.GetMouseButtonDown(0))
            MoveVertical();

        IsOutsideBounds();
    }

    private void DoActionPutTheRing()
    {
        PutTheRing();
    }
    #endregion

    #region Move
    private void MoveHorizontal()
    {
        transform.position = transform.position + Vector3.left * horizontalSpeed * Time.deltaTime;
    }

    private void MoveVertical()
    {
        rigidBody.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
    }

    private void IsOutsideBounds()
    {
        Vector3 cameraSizeInWorld = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, 0));

        Rect viewPortRect = new Rect(-cameraSizeInWorld, cameraSizeInWorld * 2);

        if (transform.position.x <= viewPortRect.xMin ||
            transform.position.y <= viewPortRect.yMin ||
            transform.position.y >= viewPortRect.yMax)
            ReturnToStart();
    }

    private void ReturnToStart()
    {
        transform.position = startPosition;
        rigidBody.velocity = Vector2.zero;
    }
    #endregion

    #region Put the ring
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetModePutTheRing();

        Finger finger = collision.gameObject.GetComponent<Finger>();
        transform.position = new Vector3(transform.position.x, finger.transform.position.y, 0);

        Destroy(rigidBody);
    }

    private void PutTheRing()
    {
        elapsedTimePutTheRing += Time.deltaTime;

        if (elapsedTimePutTheRing > timePutTheRing)
            EndLevel();

        transform.position += Vector3.left * speedPutTheRing * Time.deltaTime;
    }
    #endregion

    private void EndLevel()
    { 
        SetModeVoid();
        LevelManager.Instance.NextLevel();
    }
}
