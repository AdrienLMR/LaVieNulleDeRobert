using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ContainerMultiElement : MonoBehaviour
{
    [SerializeField] private List<Sprite> allElements = new List<Sprite>();
    [SerializeField] private List<MultiImageToChange> allElementsRobert = new List<MultiImageToChange>();
    [SerializeField] private Button leftArrow = default;
    [SerializeField] private Button rightArrow = default;
    [SerializeField] private Image imageElement = default;
    [SerializeField] private List<Image> imageToChange = default;

	public event SwitchElement switchElement = default;

	private int index = 0;

	private void Start()
	{
		leftArrow.onClick.AddListener(() => SwitchElement(-1));
		rightArrow.onClick.AddListener(() => SwitchElement(1));

		imageElement.sprite = allElements[0]/*.allElements[0] */;

		for (int i = 0; i < imageToChange.Count; i++)
		{
			Image localImageToChange = imageToChange[i];
			localImageToChange.sprite = allElementsRobert[0].allElements[i];
		}
	}

	private void SwitchElement(int factor)
	{
		index += factor;

		if (index <= -1)
			index = allElements.Count - 1;
		else if (index >= allElements.Count)
			index = 0;

		for (int i = 0; i < imageToChange.Count; i++)
		{
			Image localImageToChange = imageToChange[i];
			localImageToChange.sprite = allElementsRobert[index].allElements[i];
		}

		Sprite newSprite = allElements[index]/*.allElements[0]*/;

		imageElement.sprite = newSprite;
		//switchElement?.Invoke(this, newSprite);
	}
}
