using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDummy : MonoBehaviour
{
	public GameObject prefab;

	public GameObject player;

	void Start ()
	{

	}
	
	void Update ()
	{
		transform.position = player.transform.position;
	}
}
