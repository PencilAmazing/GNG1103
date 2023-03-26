using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;

	public Vector3 magnitude = new Vector3(0.5f, 0.5f, 0.5f);
	public float wavelength = 0.5f;

	void Awake()
	{
		if (camTransform == null) {
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	public void DoShake(float duration)
	{
		StopAllCoroutines();
		StartCoroutine(ShakeCoroutine(this.magnitude, duration, this.wavelength));
	}
	private IEnumerator ShakeCoroutine(Vector3 magnitude, float duration, float wavelength)
	{
		Vector3 startPos = camTransform.localPosition;
		float endTime = Time.time + duration;
		float currentX = 0;

		while (Time.time < endTime) {
			Vector3 shakeAmount = new Vector3(
				Mathf.PerlinNoise(currentX, 0) - .5f,
				Mathf.PerlinNoise(currentX, 7) - .5f,
				Mathf.PerlinNoise(currentX, 19) - .5f
			);

			camTransform.localPosition = Vector3.Scale(magnitude, shakeAmount) + startPos;
			currentX += wavelength;
			yield return null;
		}

		camTransform.localPosition = startPos;
	}
}
