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
    private GameObject smokeInstantiate = default;

	public List<GameObject> allImpacts = new List<GameObject>();

	public int actualBulletNumber = 0;
	public bool finishBullet = false;
	public int collision = 0;

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
			Bullet bullet_ = Instantiate(bullet, barrelEnd.position, rotationBullet).GetComponent<Bullet>();
			bullet_.sceneManagerRifle = this;
			smokeInstantiate = Instantiate(smoke, smokePosition.position, Quaternion.identity);
			actualBulletNumber++;
		}

		if (actualBulletNumber > bulletNumber && collision > bulletNumber)
		{
			DoAction = DoActionVoid;
			
			foreach (var item in allImpacts)
			{
				Destroy(item);
			}
			Destroy(smokeInstantiate);
			LevelManager.Instance.NextLevel();
		}
	}
}
