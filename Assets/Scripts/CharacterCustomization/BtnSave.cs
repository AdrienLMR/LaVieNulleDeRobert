using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSave : MonoBehaviour
{
	[SerializeField] private float timeToGoToNextScene = 10f;
	[SerializeField] private Transform poseToAddAnim = default;
	[SerializeField] private GameObject animToAdd = default;
	[SerializeField] private GameObject objectToDeactivate = default;
	[SerializeField] private Button btn;

	private float counterToGoToNextScene;
	private bool startCounter = false;

	private void Start()
	{
		//btn = GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		startCounter = true;
		Instantiate(animToAdd, poseToAddAnim.position, Quaternion.identity);
		objectToDeactivate.SetActive(false);
	}

	private void Update()
	{
		Debug.Log(startCounter);

		if (startCounter)
		{
			counterToGoToNextScene += Time.deltaTime;

			if (counterToGoToNextScene >= timeToGoToNextScene)
			{
				LevelManager.Instance.NextLevel();
			}
		}
	}
}
