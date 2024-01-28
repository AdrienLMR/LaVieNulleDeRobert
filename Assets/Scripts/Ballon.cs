using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
public class Ballon : MonoBehaviour
{
	[SerializeField] private float duration;
	[SerializeField] private float additionalStength;
	[SerializeField] private float startStength;
	[SerializeField] private int additionalVibrato;
	[SerializeField] private int startVibrato;
	[SerializeField] private int forceBallon = 100;
	[SerializeField] private float timeToStopPhysics = 1.5f;

	private float strength;
	private int vibrato;
	
	public float counterStopPhysics;
	public Rigidbody2D rgb;
	public Tween shakeTween;
	public bool stopPhysics = false;

	private void Start()
	{
		rgb = GetComponent<Rigidbody2D>();

		strength = startStength;
		vibrato = startVibrato;

		shakeTween = transform.DOShakePosition(0.5f, strength, vibrato,90,false,false).SetLoops(-1).OnStepComplete(() => {
			shakeTween.Restart();
		});
	}

	private void Update()
	{
		if (stopPhysics)
		{
			counterStopPhysics += Time.deltaTime;

			if(counterStopPhysics >= timeToStopPhysics)
			{
				rgb.velocity = Vector2.zero;

				StartCoroutine(RestartTweenShake());

				counterStopPhysics = 0;
				stopPhysics = false;
			}
		}
	}

	private IEnumerator RestartTweenShake()
	{
		yield return new WaitForSeconds(0.1f);

		shakeTween = transform.DOShakePosition(0.5f, strength, vibrato, 90, false, false).SetLoops(-1).OnStepComplete(() => {
			shakeTween.Restart();
		});

		yield return null;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
		{
			List<Ballon> allBallons = FindObjectsOfType<Ballon>().ToList();
			Vector3 bulletPosition = collision.transform.position;

			foreach (var item in allBallons)
			{
				Debug.Log("AddForce");

				Vector3 force = (item.transform.position - bulletPosition).normalized * forceBallon;

				item.shakeTween.Kill();
				item.rgb.velocity = Vector2.zero;
				item.counterStopPhysics = 0f;
				item.stopPhysics = true;
				item.rgb.AddForce(force);
			}
		}
	}

	//private void OnTriggerEnter2D(Collider2D collision)
	//{
	//	Debug.Log("Colision");
	//	List<Ballon> allBallons = FindObjectsOfType<Ballon>().ToList();
	//	Vector3 bulletPosition = collision.transform.position;

	//	foreach (var item in allBallons)
	//	{
	//		item.shakeTween.Kill();
	//		item.rgb.AddForce(item.transform.position - bulletPosition);
	//	}
	//}
}
