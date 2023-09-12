using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriviaManager : MonoBehaviour
{
	public PlayerSettings player;
	public GameObject trivia;
	
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<TipeOfView>())
		{
			PlayerSettings ps = collision.gameObject.GetComponent<TipeOfView>().viewCharacter.pl;
			if (player.canFight && ps.canFight) 
			{
				player.targetPl = ps;
				GameObject gm = Instantiate(trivia);
				gm.transform.IsChildOf(GameObject.Find("Canvas").transform);
			}
		}
	}

}
