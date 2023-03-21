using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextCycler : MonoBehaviour
{
	public TMP_Text TextUI;
	[Min(0)]
	public int NumLastCharactersToBlink;
	public float BlinkDelay;

	string cacheString = ""; // Because someone has to hold it

	private void Start()
	{
		if (!TextUI) {
			Debug.Log("No text to blink!");
			this.enabled = false;
		} else {
			cacheString = TextUI.text;
		}
		if (BlinkDelay <= 0) {
			Debug.Log("Blink delay must be true");
			this.enabled = false;
		}
		NumLastCharactersToBlink = Math.Clamp(NumLastCharactersToBlink, 0, cacheString.Length);
	}


	float timeBuffer;
	int index;
	void Update()
	{
		timeBuffer += Time.deltaTime;
		if (timeBuffer >= BlinkDelay) {
			timeBuffer = 0;
			TextUI.text = cacheString.Substring(0, cacheString.Length - NumLastCharactersToBlink + index+1);
			index = (index + 1) % NumLastCharactersToBlink;
		}
	}
}
