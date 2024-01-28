using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 100;
    [SerializeField] private GameObject impact = default;
    [SerializeField] private float timeLifeSpan = 0.5f;

    public SceneManagerRifle sceneManagerRifle = default;
    private float counterResteEnVie = 0f;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;

        counterResteEnVie += Time.deltaTime;

        if (counterResteEnVie >= timeLifeSpan)
		{
            sceneManagerRifle.collision++;
            Destroy(gameObject);
		}
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        GameObject impacts = Instantiate(impact, transform.position, Quaternion.identity);
        sceneManagerRifle.allImpacts.Add(impacts);
        sceneManagerRifle.finishBullet = true;
        sceneManagerRifle.collision++;
        Destroy(gameObject);
	}
}
