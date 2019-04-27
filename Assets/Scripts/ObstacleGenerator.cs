using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
	[SerializeField] private GameObject[] obstacles;
	[SerializeField] private int maxObstaclesInOneShot;
	[SerializeField] private Vector2 instantiationPositionBoundaries;

	private bool truck;

	private void Awake()
	{
	}
	public void GenerateObstacles()
	{
		for (int i = 0; i < Random.Range(1, maxObstaclesInOneShot + 1); i++)
		{
			GameObject obstacleRef = Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(Random.Range(instantiationPositionBoundaries.x, instantiationPositionBoundaries.y + 1), 0, GameManager.Instance.PlayerOnScene.transform.position.z + 30), Quaternion.identity);

			obstacleRef.transform.position += new Vector3(0, 3, 0);
			obstacleRef.transform.eulerAngles = new Vector3(0, 270, 0);
			obstacleRef.tag = "Obstacle";
		}
	}
}
