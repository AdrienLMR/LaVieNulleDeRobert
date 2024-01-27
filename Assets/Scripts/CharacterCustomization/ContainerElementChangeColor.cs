using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerElementChangeColor : MonoBehaviour
{
    [SerializeField] private List<Color> allElements = new List<Color>();
    [SerializeField] private Button leftArrow = default;
    [SerializeField] private Button rightArrow = default;
    [SerializeField] private Image imageElement = default;
    [SerializeField] private List<Image> allImagesToChangeColor = default;

	private int index = 0;

	private void Start()
	{
		leftArrow.onClick.AddListener(() => SwitchElement(-1));
		rightArrow.onClick.AddListener(() => SwitchElement(1));

		imageElement.color = allElements[0];
	}

	private void SwitchElement(int factor)
	{
		index += factor;

		if (index <= -1)
			index = allElements.Count - 1;
		else if (index >= allElements.Count)
			index = 0;

		Color newColor = allElements[index];

		imageElement.color = newColor;

		foreach (var item in allImagesToChangeColor)
		{
			item.color = newColor;
		}
	}
}
