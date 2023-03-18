using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipController : MonoBehaviour
{
	public bool HideTooltipOnStart = true;
	[Min(0.1f)]
	public float FadeSpeed = 0.1f;
	public TMP_Text TooltipText;

	// Start is called before the first frame update
	void Start()
	{
		if (TooltipText && HideTooltipOnStart) {
			TooltipText.gameObject.SetActive(false);
		} else {
			Debug.LogError("Tooltip Text must not be empty");
		}
	}

	IEnumerator ShowTooltipCoroutine()
	{
		TooltipText.gameObject.SetActive(true);
		// Reset tooltip alpha
		TooltipText.color *= new Color(1, 1, 1, 0);
		while (TooltipText.color.a < 1.0f) {
			// Slowly increment
			TooltipText.color += new Color(0, 0, 0, 0.1f);
			yield return new WaitForSeconds(FadeSpeed);
		};
		yield return null;
	}

	public void ShowTooltip()
	{
		if (TooltipText) {
			StartCoroutine(ShowTooltipCoroutine());
		} else {
			Debug.LogError("Tooltip Text must not be empty");
		}
	}
}
