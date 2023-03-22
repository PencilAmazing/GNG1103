using UnityEngine;
using UnityEngine.Events;

/* This is only for the message scene, don't put this anywhere else */
public class CanvasCycler : MonoBehaviour
{
	[Header("This must be three, please")]
	public GameObject[] CycleObjects;
	public UnityEvent OnCycleEnd;
	int index;
	public TextTypewriter Typewriter;
	// Start is called before the first frame update
	void Start()
	{
		// Disable all except first canvas
		DisableAllExcept(0);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void DisableAllExcept(int except)
	{
		for (int i = 0; i < CycleObjects.Length; i++) {
			if (i == except)
				CycleObjects[i].SetActive(true);
			else
				CycleObjects[i].SetActive(false);
		}
	}

	// Called when player presses a button
	public void NextPanel(bool IsGreen)
	{
		if (index == 0) {
			// We're choosing language
			// Green sets french
			if (IsGreen) {
				TypewriterLanguageSettings.lang = TypewriterLanguageSettings.Language.FR;
			} else {
				TypewriterLanguageSettings.lang = TypewriterLanguageSettings.Language.EN;
			}
			index += 1;
			DisableAllExcept(1); // Show next page
			Typewriter.NextLine();
			return;
		} else if (index < Typewriter.DialogueScript.Length) {
			// We still have more lines to show
			if (Typewriter.IsDone) {
				Typewriter.NextLine();
				index += 1;
			}
		} else if (index == Typewriter.DialogueScript.Length) {
			// show last screen
			DisableAllExcept(2);
			index += 1;
		}

		// Did we show all lines in script?
		if (index >= Typewriter.DialogueScript.Length) {
			// End of cycle, call scene transition;
			OnCycleEnd.Invoke();
			return;
		}

	}
}
