using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BtnSave : MonoBehaviour
{
	[SerializeField] private GameObject screenToSpawn;
	[SerializeField] private float timeToGoToNextScene = 10f;

	private float counterToGoToNextScene;
	private Button btn;
	private bool startCounter = false;

	private void Start()
	{
		btn = GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		screenToSpawn.SetActive(true);
		startCounter = true;
	}

	private void Update()
	{
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
