using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerRifle : MonoBehaviour
{
    [SerializeField] private Transform rifle;
    [SerializeField] private Transform barrelEnd;
    [SerializeField] private GameObject bullet;
    [SerializeField] private int bulletNumber = 5;
    [SerializeField] private GameObject smoke = default;
    [SerializeField] private Transform smokePosition = default;

	private int actualBulletNumber = 0;

	private Action DoAction;

    private void Start()
    {
		DoAction = DoActionUpdate;
    }

    private void Update()
	{
		DoAction();
	}

	private void DoActionVoid(){ }

	private void DoActionUpdate()
    {
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 0;
		Vector3 riflePosition = rifle.position;
		riflePosition.z = 0;

		Vector3 target = mousePosition - riflePosition;

		rifle.rotation = Quaternion.LookRotation(Vector3.forward, target);

		if (Input.GetMouseButtonDown(0) && actualBulletNumber <= bulletNumber)
		{
			Quaternion rotationBullet = rifle.rotation;
			Instantiate(bullet, barrelEnd.position, rotationBullet);
			Instantiate(smoke, smokePosition.position, Quaternion.identity);

			actualBulletNumber++;
		}

		if (actualBulletNumber > bulletNumber)
		{
			DoAction = DoActionVoid;
			LevelManager.Instance.NextLevel();
		}
	}
}
