using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TypewriterLanguageSettings
{
	public enum Language
	{
		EN, FR
	}

	public static Language lang = Language.EN;
	public static bool IsAlternate() => lang == Language.FR;
}

public class LanguageControls : MonoBehaviour
{
    public bool DEBUGSetAlternateLanguage = false;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        if(DEBUGSetAlternateLanguage) {
            TypewriterLanguageSettings.lang = TypewriterLanguageSettings.Language.FR;
        }
#endif
    }
}
