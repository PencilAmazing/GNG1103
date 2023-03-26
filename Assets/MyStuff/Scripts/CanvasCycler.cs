using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

/* This is only for the message scene, don't put this anywhere else */
public class CanvasCycler : MonoBehaviour
{
	[Header("This is only for the message scene, don't put this anywhere else. This must be three, please")]
	public GameObject[] CycleObjects;
	public TextTypewriter Typewriter;
	public PlayableDirector EnglishAudioDirector;
	public PlayableDirector FrenchAudioDirector;
	public UnityEvent OnCycleEnd;
	int index;

	public UnityEvent OnLanguageEnglish;
	public UnityEvent OnLanguageFrench;
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

	bool IsAudioDone()
	{
		if (TypewriterLanguageSettings.lang == TypewriterLanguageSettings.Language.FR) {
			return FrenchAudioDirector.state == PlayState.Paused;
		} else {
			return EnglishAudioDirector.state == PlayState.Paused;
		}
	}

	void ResumeAudio()
	{
		if (TypewriterLanguageSettings.lang == TypewriterLanguageSettings.Language.FR) {
			FrenchAudioDirector.Play();
		} else {
			EnglishAudioDirector.Play();
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
				OnLanguageFrench.Invoke();
			} else {
				TypewriterLanguageSettings.lang = TypewriterLanguageSettings.Language.EN;
				OnLanguageEnglish.Invoke();
			}
			index += 1;
			DisableAllExcept(1); // Show next page
			Typewriter.NextLine();
			ResumeAudio();
			return;
		} else if (index < Typewriter.DialogueScript.Length) {
			// We still have more lines to show
			if (Typewriter.IsDone && IsAudioDone()) {
				ResumeAudio();
				Typewriter.NextLine();
				index += 1;
			}
		} else if (index == Typewriter.DialogueScript.Length) {
			// show last screen
			DisableAllExcept(2);
			ResumeAudio(); // Play last audio track
			index += 1;
		} else {
			//Did we show all lines in script?
			// End of cycle, call scene transition;
			OnCycleEnd.Invoke();
			return;
		}

	}
}
