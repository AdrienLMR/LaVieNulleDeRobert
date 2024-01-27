using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
	[SerializeField] private string levelToLoad = default;
	private string currentLevel = default;

	private void Start()
	{
		StartCoroutine(LoadLevel(levelToLoad));
		StartCoroutine(TestLoadscene());
	}

	private IEnumerator TestLoadscene()
	{
		yield return new WaitForSeconds(5f);
		StartCoroutine(LoadLevel("Level2"));
		yield return null;

	}

	private IEnumerator LoadLevel(string levelToLod)
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Additive);

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
		
		if(currentLevel != null)
			StartCoroutine(UnloadLevel(currentLevel));

		currentLevel = levelToLoad;
		Debug.Log("load level " + currentLevel);
		yield return null;
	}

	private IEnumerator UnloadLevel(string unloadLevel)
	{
		AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(unloadLevel, UnloadSceneOptions.None);

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}

		Debug.Log("unload level " + unloadLevel);

		yield return null;
	}

}
