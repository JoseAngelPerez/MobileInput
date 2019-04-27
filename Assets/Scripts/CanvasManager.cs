using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
	//replay
	private GameObject image;
	private GameObject button;
	private GameObject quitButton;

	private bool replayFound;

	private void Update()
	{
		if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			if (!replayFound)
			{
				image = GameObject.FindGameObjectWithTag("BG");
				image.SetActive(false);
				button = GameObject.FindGameObjectWithTag("ReplayButton");
				button.SetActive(false);
				quitButton = GameObject.FindGameObjectWithTag("QuitButton");
				quitButton.SetActive(false);
				replayFound = image != null && button != null;
			}
			if (!GameManager.Instance.PlayerOnScene.IsAlive)
			{
				StartCoroutine(ShowOptionsDelay());
			}
		}
	}
	public void PlayButton()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void TryAgainButton()
	{
		GameManager.Instance.PlayerOnScene = null;
		GameManager.Instance.Streets = null;
		replayFound = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void QuitButton()
	{
		Application.Quit();
	}

	private IEnumerator ShowOptionsDelay()
	{
		yield return new WaitForSeconds(4);
		image.SetActive(true);
		button.SetActive(true);
		quitButton.SetActive(true);
	}
}
