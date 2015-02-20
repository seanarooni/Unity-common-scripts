using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
	
	public float duration = 0.5f;
	public float speed = 1.0f;
	public float magnitude = 0.01f;

	public bool catchUp = false;
	public float catchUpSpeed = 10.0f;
	private float increment;
	
	public bool shaking = false;
	private GameObject subject;

	void Start() {
		subject = GameObject.FindGameObjectWithTag("Player");
	}
	
	// -------------------------------------------------------------------------
	public void PlayShake() {
		
		StopCoroutine("Shake");
		shaking = true;
		StartCoroutine("Shake");
	}
	
	// -------------------------------------------------------------------------
	void Update() {
		if (increment <= 1 && catchUp) {
			increment += catchUpSpeed / 100;
			transform.position = Vector3.Lerp(transform.position, new Vector3(subject.transform.position.x, subject.transform.position.y, transform.position.z), increment);
		} else {
			increment = 0;
			catchUp = false;
		}
	}
	
	// -------------------------------------------------------------------------
	IEnumerator Shake() {


		float elapsed = 0.0f;
		
		Vector3 originalCamPos = Camera.main.transform.position;
		float randomStart = Random.Range(-1000.0f, 1000.0f);
		
		Vector3 _player = subject.transform.position;
		
		while (elapsed < duration) {
			
			elapsed += Time.deltaTime;			
			
			float percentComplete = elapsed / duration;			
			
			//Reduce the shake from full power to 0 starting half way through
			float damper = 1.0f - Mathf.Clamp(2.0f * percentComplete - 1.0f, 0.0f, 1.0f);
			
			float alpha = randomStart + speed * percentComplete;
			
			float x = Mathf.PerlinNoise(alpha, 0) * 2 - 1;
			float y = Mathf.PerlinNoise(0, alpha) * 2 -1;
			
			Vector3 player = subject.transform.position;
			Vector3 playerFollow = player - _player;
			_player = player;
			print (playerFollow.x + " " + playerFollow.y);
			
			x *= magnitude * damper;
			y *= magnitude * damper;
			
			Camera.main.transform.position = new Vector3(x + originalCamPos.x + playerFollow.x, y + originalCamPos.y + playerFollow.y, originalCamPos.z);
			
			yield return null;
		}

		catchUp = true;
	}
}
