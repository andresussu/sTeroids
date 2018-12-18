using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuBestScore : MonoBehaviour
{
	public Text highScore;

	public GameObject ui;

	public GameController gameController;

	public GameObject lastActiveMenu;
	private string gameMenuTag = "Menu";

	public int currentScore;

	public int[] highScores = new int[5];
	public string[] playerNames = new string[5];

	void Start ()
	{
		gameController = FindObjectOfType<GameController>();

		highScore.text = ("");

		GetScores();

		for (int i = highScores.Length - 1; i >= 0; i--)
		{
			highScore.text += ((highScores.Length - i) + "º " + playerNames[i] + " - " + highScores[i] + "\n");
		}
	}

	private void Update()
	{
		currentScore = Player.Score;

		if (currentScore > highScores[0])
		{
			highScores[0] = currentScore;
			//GetScores();
		}
		
		if (GameController.GameIsOver)
		{
			SaveScore();

			UpdateText();
		}

	}

	public void Back()
	{
		GameObject[] menus = GameObject.FindGameObjectsWithTag(gameMenuTag);
		foreach (GameObject menu in menus)
		{
			if(menu.activeSelf)
			{
				lastActiveMenu = menu;
			}
		}

		Toggle();
	}

	public void Toggle()
	{
		ui.SetActive(!ui.activeSelf);

		if (lastActiveMenu != null)
		{
			lastActiveMenu.SetActive(!lastActiveMenu.activeSelf);
		}
	}

	public void ResetScores ()
	{
		for (int i = 0; i < highScores.Length; i++)
		{
			PlayerPrefs.DeleteKey("HighScore" + i);
		}

		PlayerPrefs.SetInt("Tutorial", 1);

		highScore.text = ("");
	}

	private void GetScores ()
	{
		for (int i = 0; i < highScores.Length; i++)
		{
			highScores[i] = PlayerPrefs.GetInt("HighScore" + i);
		}
	}

	private void SaveScore()
	{
		for (int i = 0; i < highScores.Length; i++)
		{
			PlayerPrefs.SetInt("HighScore" + i, highScores[i]);

			Array.Sort(highScores);
			Array.Reverse(highScores);
		}
	}

	private void UpdateText()
	{
		highScore.text = ("");

		for (int i = 0; i < highScores.Length; i++)
		{
			highScore.text += ((highScores.Length - i) + "º " + "AAA" + " - " + highScores[i] + "\n");
		}
	}
}
