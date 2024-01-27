using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerElement : MonoBehaviour
{
    [SerializeField] private List<Sprite> allElements = new List<Sprite>();
    [SerializeField] private Button leftArrow = default;
    [SerializeField] private Button rightArrow = default;

	private int index = 0;

	private void Start()
	{
		leftArrow.onClick.AddListener(() => SwitchElement(-1));
		rightArrow.onClick.AddListener(() => SwitchElement(1));
	}

	private void SwitchElement(int factor)
	{

	}
}
