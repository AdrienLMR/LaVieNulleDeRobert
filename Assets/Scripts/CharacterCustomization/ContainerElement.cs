using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void SwitchElement(ChangeScreenCustomization sender, Sprite newSprite);

public class ContainerElement : MonoBehaviour
{
    [SerializeField] private List<Sprite> allElements = new List<Sprite>();
    [SerializeField] private Button leftArrow = default;
    [SerializeField] private Button rightArrow = default;
    [SerializeField] private Image imageElement = default;
    [SerializeField] private Image imageToChange = default;

	public event SwitchElement switchElement = default;

	private int index = 0;

	private void Start()
	{
		leftArrow.onClick.AddListener(() => SwitchElement(-1));
		rightArrow.onClick.AddListener(() => SwitchElement(1));

		Sprite startSprite = allElements[0];

		imageElement.sprite = startSprite;
		imageToChange.sprite = startSprite;
	}

	private void SwitchElement(int factor)
	{
		index += factor;

		if (index <= -1)
			index = allElements.Count - 1;
		else if (index >= allElements.Count)
			index = 0;

		Sprite newSprite = allElements[index];

		imageElement.sprite = newSprite;
		imageToChange.sprite = newSprite;

		//switchElement?.Invoke(this, newSprite);
	}
}
