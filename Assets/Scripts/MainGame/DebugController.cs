using UnityEngine;

public class DebugController : MonoBehaviour
{
	public GameObject ui;

	public GameObject player;

	public bool invincibility;

	public bool isPaused = false;

	public bool isDebugging = false;

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.F3))
		{
			Toggle();
		}

		Invincibility();
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

	public void Invincibility()
	{
		PolygonCollider2D pl = player.GetComponent<PolygonCollider2D>();

		if (invincibility)
		{
			pl.isTrigger = false;
		}
		else
		{
			pl.isTrigger = true;
		}

	}

}
