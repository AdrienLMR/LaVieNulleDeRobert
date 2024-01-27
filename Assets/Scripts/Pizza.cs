using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Transform couteau;
    Transform selection;
    [SerializeField] float pizzaSize = 5.0f;
    private bool isAnimating = false;

    // Start is called before the first frame update
    void Start()
    {
        couteau = transform.Find("couteau");
        selection = transform.Find("selection");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenMousePosition = Input.mousePosition;
        Vector2 worldMouse2D = Camera.main.ScreenToWorldPoint(screenMousePosition);
        Vector3 worldMouse = new Vector3(worldMouse2D.x, worldMouse2D.y, 0.0f);
        Vector3 direction = worldMouse - transform.position;
        direction = direction.normalized;
        
        couteau.position = transform.position + pizzaSize * direction;
        selection.position = transform.position + pizzaSize * 0.52f * direction;
    }

}
