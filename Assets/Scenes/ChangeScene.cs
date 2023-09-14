using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
	public string text;
	public static void changeScene(string txt) 
	{
		SceneManager.LoadScene(txt);
	}
	public void buttonChangeScene() 
	{
		changeScene(text);
	}
}
