using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	private bool showedGreeting = false;
	private bool showedWin = false;
	private GameObject playerObject;
	private float startTime = 0f;
	private int elapsed = 0;

	void Start()
	{
		playerObject = GameObject.Find ("destroyer");
		startTime = Time.time;
	}

	void OnGUI(){
		/*
		GUI.Box (new Rect (0,0,100,50), "Top-left");
		GUI.Box (new Rect (Screen.width - 100,0,100,50), "Top-right");
		GUI.Box (new Rect (0,Screen.height - 50,100,50), "Bottom-left");
		*/

		if (GameControllerScript.IsFailed || GameControllerScript.IsWin)
		{
			if (!showedWin)
			{
				elapsed = (int)(Time.time - startTime); 
				showedWin = true;
			}

			GUIStyle style = GUI.skin.GetStyle ("Label");
			style.fontSize = 24;

			GUI.Box (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 50,200,150), "");
			if (GameControllerScript.IsFailed)
			{
				GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 30, 120, 60), "Wasted!");
			}
			if (GameControllerScript.IsWin)
			{
				string timeStr = "Time: " + elapsed / 60 + ":" + elapsed % 60;
				GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 30, 120, 60), "You Win!\n" + timeStr);
			}

			if (GUI.Button (new Rect (Screen.width / 2 - 60, Screen.height / 2 + 50, 120, 30), "Retry")) {
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

			if (GUI.Button (new Rect (Screen.width / 2 - 60, Screen.height / 2 + 50, 120, 30), "Continue")) {
				showedGreeting = true;
			}

		}

		PlayerScript playerScript = (PlayerScript)playerObject.GetComponent<PlayerScript> ();
		GUIStyle labelStyle = GUI.skin.GetStyle ("Label");
		labelStyle.fontSize = 16;
		float factor = playerScript.GetHP () / 100f;
		GUI.contentColor = new Color (0f, 1f, 0f) * factor + new Color (1f, 0f, 0f) * (1 - factor);

		GUI.Box (new Rect (Screen.width - 100,Screen.height - 50,100,50), "");
		GUI.Label (new Rect (Screen.width - 80,Screen.height - 40,100,50), "HP: " + playerScript.GetHP());

	}
}
