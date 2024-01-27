using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

[RequireComponent(typeof(Button))]
public class ChangeScreenCustomization : MonoBehaviour
{
    [SerializeField] private GameObject screenToActivate;
    [SerializeField] private GameObject screenToDeActivate;
	//[SerializeField] private List<Transform> allContainerFace = new List<Transform>();
	//[SerializeField] private List<Transform> allContainerClothes = new List<Transform>();
	[SerializeField] private Transform containerVerticalLayoutClothes = default;
	[SerializeField] private Transform containerVerticalLayoutFace = default;
	[SerializeField] private bool isFace = false;
	[SerializeField] private float animContainerFaceXPosition = 25;
	[SerializeField] private TextMeshProUGUI txtFace = default;
	[SerializeField] private TextMeshProUGUI txtClothes = default;

	private Vector3 startXPoseFace = default;
	private Vector3 startXPoseClothes = default;

	private Button btn;

	private void Awake()
	{
		btn = GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}

	private void Start()
	{
		//startXPoseFace = allContainerFace[0].transform.position;
		//startXPoseClothes = allContainerClothes[0].transform.position ;	
		startXPoseFace = containerVerticalLayoutFace.transform.position;
		startXPoseClothes = containerVerticalLayoutClothes.transform.position ;
		Debug.Log(startXPoseClothes);

		containerVerticalLayoutClothes.transform.position = startXPoseClothes + Vector3.right * animContainerFaceXPosition;

		//foreach (var item in allContainerClothes)
		//{
		//	item.transform.DOMoveX(startXPoseClothes.x + animContainerFaceXPosition,0.05f,true);
		//}
	}



	private void OnClick()
	{
		if (isFace)
		{
			containerVerticalLayoutFace.transform.DOMoveX(startXPoseFace.x - animContainerFaceXPosition, 0.5f, true);
			containerVerticalLayoutClothes.transform.position = startXPoseClothes + Vector3.right * animContainerFaceXPosition;
			containerVerticalLayoutClothes.DOMoveX(startXPoseClothes.x, 0.5f, true);

			txtFace.text = "Clothes";
			txtClothes.text = "Face";

		}
		else
		{
			containerVerticalLayoutClothes.transform.DOMoveX(startXPoseClothes.x - animContainerFaceXPosition, 0.5f, true);
			containerVerticalLayoutFace.transform.position = startXPoseFace + Vector3.right * animContainerFaceXPosition;
			containerVerticalLayoutFace.DOMoveX(startXPoseFace.x, 0.5f, true);

			txtFace.text = "Face";
			txtClothes.text = "Clothes";
		}

		isFace = !isFace;

		//if (isFace)
		//{
		//	foreach (var item in allContainerFace)
		//	{
		//		item.transform.DOMoveX(startXPoseFace.x- animContainerFaceXPosition, 0.5f, true);
		//	}

		//	foreach (var item in allContainerClothes)
		//	{
		//		item.transform.DOMoveX(startXPoseClothes.x + animContainerFaceXPosition, 0.05f, true);
		//	}

		//	foreach (var item in allContainerClothes)
		//	{
		//		item.transform.DOMoveX(startXPoseClothes.x, 0.5f, true).SetDelay(0.05f)/*.OnComplete(() => containerVerticalLayoutClothes.enabled = true)*/;
		//	}

		//	txtFace.text = "Clothes";
		//	txtClothes.text = "Face";

		//	isFace = false;
		//}
		//else
		//{
		//	foreach (var item in allContainerClothes)
		//	{
		//		item.transform.DOMoveX(startXPoseClothes.x - animContainerFaceXPosition, 0.5f, true);
		//	}

		//	foreach (var item in allContainerFace)
		//	{
		//		item.transform.DOMoveX(startXPoseFace.x + animContainerFaceXPosition, 0.05f, true);
		//	}

		//	foreach (var item in allContainerFace)
		//	{
		//		item.transform.DOMoveX(startXPoseFace.x, 0.5f, true).SetDelay(0.05f)/*.OnComplete(() => containerVerticalLayoutClothes.enabled = true)*/;
		//	}

		//	txtFace.text = "Face";
		//	txtClothes.text = "Clothes";

		//	isFace = true;
		//}

		//screenToActivate.SetActive(true);
		//screenToDeActivate.SetActive(false);
	}
	
	public void ChangeScreenToClothes()
	{
		
	}
}
