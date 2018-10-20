using UnityEngine;

public class GameController : MonoBehaviour
{
	public static bool GameIsOver;
	public static bool GameIsPaused;

	public GameObject gameOverUi;
	public GameObject tutorialUi;
	public GameObject playerControlButtons;

	public bool playerControlButtonsToogle;

	public int difficulty = 1;

	public string levelToLoad = "MainMenu";

	public SceneFader sceneFader;

	public DebugController debugController;

	void Start()
	{
		GameIsOver = false;
		GameIsPaused = false;

		if (PlayerPrefs.GetInt("Tutorial") == 0)
		{
			tutorialUi.SetActive(false);
		}

		if (debugController.isDebugging == true)
		{
			Debug.Log(PlayerPrefs.GetInt("Tutorial"));
		}
	}


	void Update()
	{
		if (GameIsOver)
			return;

		if (Player.Lives <= 0)
		{
			EndGame();
		}

		if(playerControlButtonsToogle)
		{
			playerControlButtons.SetActive(true);
		}
		else
		{
			playerControlButtons.SetActive(false);
		}

	}

	void EndGame()
	{
		GameIsOver = true;
		gameOverUi.SetActive(true);
	}

	public void Menu()
	{
		Time.timeScale = 1f;
		sceneFader.FadeTo(levelToLoad);
	}

	/*
	private void EdgeUpdate(GameObject _objPosition)
	{
		Vector3 objPosition = _objPosition.transform.position;

		if (objPosition.x > (cam.orthographicSize * cam.aspect) || objPosition.x < (-cam.orthographicSize * cam.aspect))
		{
			objPosition.x *= -0.99f;
		}

		if (objPosition.y > cam.orthographicSize || objPosition.y < -cam.orthographicSize)
		{
			objPosition.y *= -0.99f;
		}

		_objPosition.transform.position = objPosition;
	}
	*/


}