using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class TriviaManager : MonoBehaviour
{
	public PlayerSettings player;
	public GameObject trivia;
	public GameObject triviaInstante;

	private void OnTriggerEnter(Collider other)
	{
		if (player.pv.IsMine) 
		{ 
		if (other.gameObject.GetComponent<TipeOfView>())
		{
			if (other.gameObject.GetComponent<TipeOfView>().viewCharacter.pl != null) 
			{
				PlayerSettings ps = other.gameObject.GetComponent<TipeOfView>().viewCharacter.pl;
				if (player.canFight && ps.canFight && triviaInstante == null)
					{
						player.plState = playerStates.Fight;
						player.targetPl = ps;
						triviaInstante = Instantiate(trivia);
						triviaInstante.GetComponent<TriviaMain>().pl = player;
						print(triviaInstante);
						triviaInstante.transform.parent = GameObject.Find("Canvas").transform.Find("CanvasLocal");
						triviaInstante.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
						player.change = true;
					}
				}
			}
		}

	}

}
