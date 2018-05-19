using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour 
{
	int currentScene;
	int sceneCount = 8;

	void Start()
	{
		currentScene = SceneManager.GetActiveScene ().buildIndex;
	}

	public void Next()
	{
		int next = currentScene + 1;
		if (next >= sceneCount)
			next = 0;
		if (next == currentScene)
			return;
		SceneManager.LoadScene (next);
	}

	public void Back()
	{
		int next = currentScene - 1;
		if (next < 0)
			next = sceneCount - 1;
		if (next == currentScene)
			return;
		SceneManager.LoadScene (next);
	}
}
