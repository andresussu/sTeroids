using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{

	public GameObject ui;

	public Text scoreText;

	public SceneFader sceneFader;

	public bool isPaused = false;

	void Update()
	{
		if (GameController.GameIsOver)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			Toggle();
		}

		scoreText.text = "Your score: " + Player.Score.ToString();
	}

	public void Toggle()
	{
		ui.SetActive(!ui.activeSelf);

		if (ui.activeSelf)
		{
			isPaused = true;
			Time.timeScale = 0f;
		}
		else
		{
			isPaused = false;
			Time.timeScale = 1f;
		}
	}

	public void Retry()
	{
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
		isPaused = false;
		Time.timeScale = 1f;
	}

	public void Quit()
	{
		Application.Quit();
	}

}