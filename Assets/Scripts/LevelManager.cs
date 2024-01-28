using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
	[SerializeField] private string loadingSceneName = string.Empty;

	[SerializeField] private List<string> allLevelsToLoad = new List<string>();
	private string currentLevel = default;
	private bool unloadFinished = true;
	private int indexLevel = -1;

	private string levelToLoad = string.Empty;

	[SerializeField] private Camera _camera = default;

	public static LevelManager Instance;

	private void Awake()
	{
		if (Instance != this)
			Instance = this;
	}

	private void Start()
	{
		//NextLevel();
		indexLevel++;
		SceneManager.LoadSceneAsync(allLevelsToLoad[indexLevel], LoadSceneMode.Additive);
		currentLevel = allLevelsToLoad[indexLevel];
	}

	public static Camera GetCamera()
		=> Instance._camera;

	public void NextLevel()
	{
		indexLevel++;
		levelToLoad = allLevelsToLoad[indexLevel];
		StartCoroutine(LoadingScreenAppear());
		//StartCoroutine(LoadLevel(allLevelsToLoad[indexLevel]));
	}

	private IEnumerator LoadingScreenAppear()
    {
		AsyncOperation asyncLoading = SceneManager.LoadSceneAsync(loadingSceneName, LoadSceneMode.Additive);

		yield return null;
	}

	public void LoadLevel()
    {
		StartCoroutine(LoadLevel(levelToLoad));
    }

	private IEnumerator LoadLevel(string levelToLoad)
	{
		//AsyncOperation asyncLoading = SceneManager.LoadSceneAsync(loadingSceneName, LoadSceneMode.Additive);

		//yield return new WaitUntil(() => asyncLoading.isDone);

		if (currentLevel != null)
		{
			unloadFinished = false;
			StartCoroutine(UnloadLevel(currentLevel));
		}

		yield return new WaitUntil(() => unloadFinished == true);

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Additive);

		yield return new WaitUntil(() => asyncLoad.isDone);

		currentLevel = levelToLoad;

		Loading.Stop();

		//Debug.Log("load level " + currentLevel);
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

	public void UnloadLoading()
    {
		StartCoroutine(_UnloadLoading());
    }

	private IEnumerator _UnloadLoading()
    {
		AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(loadingSceneName, UnloadSceneOptions.None);
		yield return null;
	}
}
