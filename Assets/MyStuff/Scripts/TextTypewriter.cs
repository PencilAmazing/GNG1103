using System.Collections;
using TMPro;
using UnityEngine;

/* TextTypewriter my beloved*/
public class TextTypewriter : MonoBehaviour
{
	[field: Header("TextTypewriter my beloved child <3")]
	[field: Min(0.01f)]
	[field: SerializeField]
	public float SecondsDelay { get; set; }
	[Min(0.01f)]
	public float AlternateSecondsDelay;
	[field: SerializeField] // DO NOT UNPROPERTIFY ANY PROPERTIES WILL DELETE ALL PREV SETTINGS WHYYYYY
	public TextMeshProUGUI TextGUI { get; set; }

	public Canvas FullUICanvas;

	private string finalString = "";

	[TextArea]
	public string[] DialogueScript;
	[TextArea]
	public string[] AlternateDialogueScript;
	public int DialogIndex { get; private set; }

	public bool IsDone { get; private set; }

	WaitForSeconds _delayBetweenCharactersYieldInstruction;

	public void Start()
	{
		if (SecondsDelay <= 0) {
			SecondsDelay = 0.1f;
			Debug.LogError("Seconds delay must be positive");
		}
		if (TextGUI == null) {
			Debug.LogError("TextGUI reference must not be null");
		}
		Debug.Assert(DialogueScript.Length == AlternateDialogueScript.Length, "Dialog and alternate dialog do not match up!");
		// Replace dialog reference
		if (TypewriterLanguageSettings.IsAlternate()) {
			DialogueScript = AlternateDialogueScript;

		}
	}

	public void StartTypeWriterOnText()
	{
		if (TextGUI != null) {
			if (SecondsDelay <= 0) SecondsDelay = 0.1f;
			finalString = TextGUI.text;
			StartCoroutine(TypeWriterCoroutine(TextGUI, finalString, SecondsDelay));
		} else Debug.LogError("TextGUI reference must not be null");
	}

	public void NextLine()
	{
		if (TextGUI == null) {
			Debug.LogError("TextGUI reference must not be null");
			return;
		}
		if (FullUICanvas) FullUICanvas.enabled = true; // Activate gameObject automatically

		if (SecondsDelay <= 0) SecondsDelay = 0.1f;

		var script = TypewriterLanguageSettings.IsAlternate() ? AlternateDialogueScript : DialogueScript;
		finalString = script[DialogIndex];
		DialogIndex = (DialogIndex + 1) % script.Length;
		StopAllCoroutines(); // Cancel last run
		IsDone = false;
		var delay = TypewriterLanguageSettings.IsAlternate() ? AlternateSecondsDelay : SecondsDelay;
		StartCoroutine(TypeWriterCoroutine(TextGUI, finalString, delay));
	}

	public void SetCanvasActive(bool active)
	{
		if (FullUICanvas) {
			FullUICanvas.enabled = active;
		} else {
			Debug.Log("Subtitle canvas is null, cannot change activity");
		}
	}

	IEnumerator TypeWriterCoroutine(TMP_Text textComponent, string stringToDisplay, float delayBetweenCharacters)
	{
		// Cache the yield instruction for GC optimization
		_delayBetweenCharactersYieldInstruction = new WaitForSeconds(delayBetweenCharacters);
		// Iterating(looping) through the string's characters
		for (int i = 0; i <= stringToDisplay.Length; i++) {
			// Retrieves part of the text from string[0] to string[i]
			textComponent.text = stringToDisplay.Substring(0, i);
			// We wait x seconds between characters before displaying them
			yield return _delayBetweenCharactersYieldInstruction;
		}
		IsDone = true;
	}
}