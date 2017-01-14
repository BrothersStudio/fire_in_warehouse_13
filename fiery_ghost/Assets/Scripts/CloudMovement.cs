using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
	public float speed;

    void FixedUpdate()
    {
		Vector3 newPosition = transform.position + new Vector3 (speed * Time.deltaTime, 0f, 0f);
		transform.position = newPosition;
    }

	void OnBecameInvisible()
	{
		transform.position = new Vector3 (-11f, Random.Range (-1f, 4f), 0f);
	}
}
