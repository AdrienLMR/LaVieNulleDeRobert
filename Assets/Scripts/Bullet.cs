using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 100;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        Debug.Log("ColisionBullet");
        Destroy(gameObject);
	}
}
