public class PerlinNoiseCameraShake : MonoBehaviour {

	public float duration = 0.5f;
	public float speed = 1.0f;
	public float magnitude = 0.01f;
	
	public bool shaking = false;
	
	// -------------------------------------------------------------------------
	public void PlayShake() {
		
		StopAllCoroutines();
		StartCoroutine("Shake");
	}
	
	// -------------------------------------------------------------------------
	void Update() {
		if (shaking) {
			shaking = false;
			PlayShake();
		}
	}
	
	// -------------------------------------------------------------------------
	IEnumerator Shake() {
		
		float elapsed = 0.0f;
		
		Vector3 originalCamPos = Camera.main.transform.position;
		Vector3 originalCamLocalPos = Camera.main.transform.localPosition;
		float randomStart = Random.Range(-1000.0f, 1000.0f);

		Vector3 _player = GameObject.FindGameObjectWithTag("Player").transform.position;
		
		while (elapsed < duration) {
			
			elapsed += Time.deltaTime;			
			
			float percentComplete = elapsed / duration;			
			
			//Reduce the shake from full power to 0 starting half way through
			float damper = 1.0f - Mathf.Clamp(2.0f * percentComplete - 1.0f, 0.0f, 1.0f);
			
			float alpha = randomStart + speed * percentComplete;

			float x = Mathf.PerlinNoise(alpha, 0) * 2 - 1;
			float y = Mathf.PerlinNoise(0, alpha) * 2 -1;

			Vector3 player = GameObject.FindGameObjectWithTag("Player").transform.position;
			Vector3 playerFollow = player - _player;
			_player = player;

			x *= magnitude * damper;
			y *= magnitude * damper;
			
			Camera.main.transform.position = new Vector3(x + originalCamPos.x + playerFollow.x, y + originalCamPos.y + playerFollow.y, originalCamPos.z);
			
			yield return null;
		}
		shaking = true;
		Camera.main.transform.localPosition = originalCamLocalPos;
	}
}
