/*
* 4-way movement style similar to games such as Pokemon.  
*/

using UnityEngine;
using System.Collections;

public class JRPGMove : MonoBehaviour {

	private float speed = 3f;
	private float gridSize = 1f;
	private Vector2 input;
	private bool isMoving = false;
	private Vector2 startPosition;
	private Vector2 endPosition;
	private float t;

	private void Update() 
	{
		if (!isMoving)
		{
			input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

			if (input != Vector2.zero)
			{
				StartCoroutine(move(transform));
			}
		}
	}

	public IEnumerator move (Transform transform)
	{
		isMoving = true;
		startPosition = transform.position;
		t = 0;

		endPosition = new Vector2(startPosition.x + System.Math.Sign(input.x) * gridSize, startPosition.y + System.Math.Sign(input.y) * gridSize);

		while (t < 1f)
		{
			t += Time.deltaTime * (speed/gridSize);
			transform.position = Vector2.Lerp(startPosition, endPosition, t);
			yield return null;
		}

		isMoving = false;
		yield return 0;
	}
}
