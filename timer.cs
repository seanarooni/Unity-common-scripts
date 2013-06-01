/*
a timer I used for survival mode in a game written in C#.

Just set the starTime = Time.time again in the appropriate place to reset the clock during the game.
*/

using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
  
	private float startTime;
	string textTime;

	void Awake () {
		startTime = Time.time;	
	}

	void OnGUI () {
		float guiTime = Time.time - startTime;

		float minutes = guiTime / 60;
		float seconds = guiTime % 60;
		float fraction = (guiTime * 100) % 100;

		textTime = string.Format ("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);

		GUI.Label( new Rect( 400, 25, 100, 30 ), textTime);
	}
}