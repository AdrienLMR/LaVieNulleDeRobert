using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
	[SerializeField] private List<string> allLevelToLoad = new List<string>();
	private string currentLevel = default;
	private bool unloadFinished = true;
	private int indexLevel = -1;

	private LevelManager Instance;

	private void Awake()
	{
		if (Instance != this)
			Instance = this;
	}

	private void Start()
	{
		ChangeLevel();
	}

	private void ChangeLevel()
	{
		indexLevel++;
		StartCoroutine(LoadLevel(allLevelToLoad[indexLevel]));
	}

	private IEnumerator LoadLevel(string levelToLoad)
	{
		Debug.Log("Affichez l'ecran de chargement");

		if (currentLevel != null)
		{
			unloadFinished = false;
			StartCoroutine(UnloadLevel(currentLevel));
		}

		yield return new WaitUntil(() => unloadFinished == true);

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Additive);

		yield return new WaitUntil(() => asyncLoad.isDone);

		currentLevel = levelToLoad;
		Debug.Log("load level " + currentLevel);
		Debug.Log("Desaffichez l'ecran de chargement");
		yield return null;
	}

	private IEnumerator UnloadLevel(string unloadLevel)
	{
		AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(unloadLevel, UnloadSceneOptions.None);
		yield return new WaitUntil(() => asyncLoad.isDone);
		unloadFinished = true;
		Debug.Log("unload " + unloadLevel);
		yield return null;
	}

}
