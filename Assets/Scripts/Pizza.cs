using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePizza : MonoBehaviour
{
    [SerializeField] private float pizzaSize = 5.0f;
    [SerializeField] private Transform couteau;
    [SerializeField] private Transform selection;
    [SerializeField] private Transform center;
    [SerializeField] private float speedRotation = 50f;
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float distanceToExterieurpizza = 2f;
    [SerializeField] private GameObject lineRenderer = default;
    [SerializeField] private float timeToAddPointToLineRenderer = default;
    [SerializeField] private float canCutPizza = 3;
    [SerializeField] private float increaseSpeedRotation = 50;
    [SerializeField] private float increaseSpeed = 10;
    [SerializeField] private List<GameObject> allLineRenderer = new List<GameObject>();

    private bool isAnimating = false;
    private bool move = false;
    private Vector2 startDirection = default;
    private Vector2 startPosition = default;
    private Vector3 target = default;
    private LineRenderer actualRenderer = default;
    private float counterToAddPointLineRenderer = default;
    private int counterSizeLineRenderer = 0;
    private int counterCanCutPizza = 0;
    private bool stopToutToutTout = false;

    void Update()
    {
        if (!stopToutToutTout)
        {
            if (!move)
            {
                //Vector2 screenMousePosition = Input.mousePosition;
                //Vector2 worldMouse2D = Camera.main.ScreenToWorldPoint(screenMousePosition);
                //Vector3 worldMouse = new Vector3(worldMouse2D.x, worldMouse2D.y, 0.0f);
                //Vector3 direction = worldMouse - transform.position;
                //direction = direction.normalized;

                Vector3 directionToCenter = center.position - couteau.position;

                //couteau.position = transform.position + pizzaSize * direction;
                //selection.position = transform.position + pizzaSize * 0.52f * direction;
                couteau.rotation = Quaternion.LookRotation(couteau.transform.forward, directionToCenter);
                center.rotation = center.rotation * Quaternion.AngleAxis(speedRotation * Time.deltaTime, Vector3.forward);

                if (Input.GetMouseButtonDown(0))
                {
                    move = true;
                    target = couteau.up * distanceToExterieurpizza;
                    startDirection = couteau.forward;
                    startPosition = couteau.position;
                    actualRenderer = Instantiate(lineRenderer).GetComponent<LineRenderer>();
                    allLineRenderer.Add(actualRenderer.gameObject);
                    counterToAddPointLineRenderer = timeToAddPointToLineRenderer;
                    counterSizeLineRenderer = 0;
                    counterCanCutPizza++;
                    speedRotation += increaseSpeedRotation;
                    speed += increaseSpeed;
                }
            }
            else
            {
                couteau.position = Vector3.MoveTowards(couteau.position, target, speed * Time.deltaTime);

                counterToAddPointLineRenderer += Time.deltaTime;

                if (counterToAddPointLineRenderer >= timeToAddPointToLineRenderer)
                {
                    counterSizeLineRenderer++;
                    counterToAddPointLineRenderer = 0f;
                    actualRenderer.positionCount = counterSizeLineRenderer;
                    actualRenderer.SetPosition(counterSizeLineRenderer - 1, couteau.position);
                }

                if (Vector3.Distance(couteau.position, target) <= 0.001f)
                {
                    move = false;
                }
            }
        }

        if (counterCanCutPizza >= canCutPizza && !move)
		{
            stopToutToutTout = true;

            foreach (var item in allLineRenderer)
            {
                Destroy(item);
            }

            LevelManager.Instance.NextLevel();
        }
    }
}
