using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	private bool showedGreeting = false;

	void OnGUI(){
		/*
		GUI.Box (new Rect (0,0,100,50), "Top-left");
		GUI.Box (new Rect (Screen.width - 100,0,100,50), "Top-right");
		GUI.Box (new Rect (0,Screen.height - 50,100,50), "Bottom-left");
		GUI.Box (new Rect (Screen.width - 100,Screen.height - 50,100,50), "Bottom-right");
		*/
		if (GameControllerScript.IsFailed || GameControllerScript.IsWin)
		{
			GUI.Box (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 50,200,150), "");
			if (GameControllerScript.IsFailed)
			{
				GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 30, 120, 60), "Wasted!");
			}
			if (GameControllerScript.IsWin)
			{
				GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 30, 120, 60), "You Win!");
			}

			if (GUI.Button (new Rect (Screen.width / 2 - 60, Screen.height / 2 + 50, 120, 20), "Retry")) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}

		if (!showedGreeting)
		{
			GUIStyle style = GUI.skin.GetStyle ("TextField");
			style.fontSize = 18;
			style = GUI.skin.GetStyle ("Button");
			style.fontSize = 18;

			GUI.TextField (new Rect (Screen.width / 2 - 300, Screen.height / 2 - 150,600,150),
			            "\n" +
			            "  For two hundred years The War is lasting betwen twin planets Bibora \n  and Azamath.\n" +
						"  You are desperate smuggler delivering goods into blockaded Azamath.\n" +
						"  Evade asteroids and occupying forces for great profit!");

			if (GUI.Button (new Rect (Screen.width / 2 - 60, Screen.height / 2 + 50, 120, 20), "Continue")) {
				showedGreeting = true;
			}

		}

	}
}
