using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	public string mainMenuScreenName;
	public Font pauseMenuFont;
	//private bool showPauseMenu = false;	
	
	// Update is called once per frame
	void Update () {
		if(Player.PauseGame)
		{
			Time.timeScale = 0.0f;
			AudioListener.volume = 0;
		//	Screen.showCursor = true;
		} else if (!Player.PauseGame && Player.myHealth > 0) {
			Time.timeScale = 1.0f;
			AudioListener.volume = 1;
			Screen.lockCursor = true;

		}
	}
	
	void OnGUI() {
		GUI.skin.box.font = pauseMenuFont;
		GUI.skin.button.font = pauseMenuFont;
		
		if(Player.PauseGame)
		{
			GUI.Box(new Rect(Screen.width /2 - 100, Screen.height /2 - 100, 250, 200), "Pause Menu");
			if (GUI.Button(new Rect(Screen.width /2 - 100, Screen.height /2 - 50, 250, 50), "Main Menu")) {
				Application.LoadLevel(mainMenuScreenName);	
			}
			
			if ( GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height /2, 250, 50), "Restart Game")) {
				Application.LoadLevel("GameScene");
			}
			
			if ( GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height /2 + 50, 250, 50), "Quit Game")) {
				Application.Quit();
			}
		}
	}
	
	
	
}
