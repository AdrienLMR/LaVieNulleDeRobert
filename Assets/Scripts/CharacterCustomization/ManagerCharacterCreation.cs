using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManagerCharacterCreation : MonoBehaviour
{
	private List<ContainerElement> allContainerElements = new List<ContainerElement>();

	private void Start()
	{
		allContainerElements = FindObjectsOfType<ContainerElement>().ToList();

		foreach (var containerElement in allContainerElements)
		{
			//containerElement +=	
		}
	}
}
