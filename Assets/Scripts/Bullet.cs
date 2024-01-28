using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 100;
    [SerializeField] private GameObject impact = default;

    public SceneManagerRifle sceneManagerRifle = default;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        GameObject impacts = Instantiate(impact, transform.position, Quaternion.identity);
        sceneManagerRifle.allImpacts.Add(impacts);
        Destroy(gameObject);
	}
}
