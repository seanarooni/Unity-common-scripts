using UnityEngine;
using System.Collections;

public class Legacy-NPC-Dialogue : MonoBehaviour {

	public string[] talkMessage;  //public so text can be modified in the unity editor
	private string displayText;
	private bool showText;
	private int messageLocation = 0;

	void OnGUI() {
		if (showText) {
			GUI.Label(new Rect(10,10,500,20), "" + displayText); //"" + converts to string
			//Update to new GUI system
		}
	}

	public void interact() { //called from Player script
		showText = true;
	
		if (messageLocation + 1 > talkMessage.Length) { //check range
			showText = false;
			messageLocation = 0;
			return;
		} else {
			displayText = talkMessage [messageLocation];
			messageLocation++;
		}
	}

}
