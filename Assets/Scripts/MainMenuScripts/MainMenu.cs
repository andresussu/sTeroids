using UnityEngine;

public class MainMenu : MonoBehaviour
{

	public string levelToLoad = "MainScene";

	public SceneFader sceneFader;

	public void Play()
	{
		sceneFader.FadeTo(levelToLoad);
	}

	public void Quit()
	{
		Application.Quit();
	}

}