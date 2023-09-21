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
	public Collider colliderTipe;
	private void OnTriggerEnter(Collider other)
	{
			if (player.pv.IsMine)
			{
				if (player.plState != playerStates.Fight || player.plState != playerStates.Loser || player.plState != playerStates.Winner)
				{
					if (other.gameObject.GetComponent<TipeOfView>())
					{
						if (other.gameObject.GetComponent<TipeOfView>().viewCharacter.pl != null)
						{
							PlayerSettings ps = other.gameObject.GetComponent<TipeOfView>().viewCharacter.pl;
							if (ps.plState != playerStates.Fight || ps.plState != playerStates.Loser || ps.plState != playerStates.Winner)
								{
								print(ps.plState);
								if (ps.gameObject.tag == "Survival" && player.gameObject.tag == "Ghost")
								{
									Fight(ps);
								}
								else if (player.gameObject.tag == "Survival" && ps.gameObject.tag == "Ghost")
								{
									Fight(ps);
								}
								else
								{
									ps = null;
								}
							}
							else
							{
								ps = null;
							}
						}
					}
				}
			}
	}
	void Fight(PlayerSettings rival)
	{
			if (player.canFight && rival.canFight && triviaInstante == null)
			{
				player.plState = playerStates.Fight;
				player.targetPl = rival;
				rival.targetPl = player;
				triviaInstante = Instantiate(trivia);
				triviaInstante.GetComponent<TriviaMain>().pl = player;
				print(triviaInstante);
				triviaInstante.transform.parent = GameObject.Find("Canvas").transform.Find("CanvasLocal");
				triviaInstante.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
				player.change = true;
			}
	}
}
