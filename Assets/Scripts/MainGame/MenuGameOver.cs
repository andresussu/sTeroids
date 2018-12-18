using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
	public GameObject ui;
	public Text scoreText;

	public SceneFader sceneFader;

	void Update()
	{
		scoreText.text = "Your score: " + Player.Score.ToString();
	}
	   
	public void Toggle()
	{
		ui.SetActive(!ui.activeSelf);

		if (ui.activeSelf)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	public void Retry()
	{
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
		Time.timeScale = 1f;
	}

	public void Quit()
	{
		Application.Quit();
	}


}
