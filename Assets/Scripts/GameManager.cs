using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private bool invoked = false;
	private float timer = 0;

	private Player player;
	private ObstacleGenerator obstacleGenerator;

	private GameObject[] streets;

	private static GameManager instance;

	public static GameManager Instance
	{
		get { return instance; }
	}
	public Player PlayerOnScene
	{
		get { return player; }
		set { player = value; }
	}
	public GameObject[] Streets
	{
		get { return streets; }
		set { streets = value; }
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0)
		{

		}
		else
		{
			if (player == null)
			{
				player = FindObjectOfType<Player>();
			}
			if (!player.IsAlive)
			{
				player = null;
				streets = null;
			}
			Debug.Log(player);
			if (obstacleGenerator == null)
			{
				obstacleGenerator = FindObjectOfType<ObstacleGenerator>();
			}
			if (streets == null)
			{
				streets = GameObject.FindGameObjectsWithTag("Street");
			}

			if (player.IsAlive)
			{
				if (!invoked)
				{
					obstacleGenerator.InvokeRepeating("GenerateObstacles", 2.0f, 3);
					invoked = true;
				}
				if (player != null && streets != null)
				{
					StreetControl();
				}
			}
			else
			{
				obstacleGenerator.CancelInvoke();
			}
		}
	}

	private void StreetControl()
	{
		foreach (GameObject street in streets)
		{
			if (street.transform.position.z - player.transform.position.z <= -20)
			{
				street.transform.position += new Vector3(0, 0 , 60);
			}
		}
	}
}
