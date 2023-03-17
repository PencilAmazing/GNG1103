using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MessageTypeScript : MonoBehaviour
{
    public TMPro.TMP_Text[] Screens;
    public float DisplaySpeed = 0.1f;
    public float PauseDuration = 1.0f;

    public UnityEvent OnDoneAllScreens;    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TypeoutText());
    }

    void DisableAllScreensExcept(int ex)
    {
        for (int i = 0; i < Screens.Length; i++)
        {
            // all false except index ex
            // clever thing i guess
            Screens[i].gameObject.SetActive(i == ex);
        }
    }

    IEnumerator TypeoutText()
    {
        for (int s = 0; s < Screens.Length; s++)
        {
            yield return new WaitForSeconds(PauseDuration);
            string GoalText = Screens[s].text;
            DisableAllScreensExcept(s);
            for (int i = 0; i <= GoalText.Length; i++)
            {
                Screens[s].text = GoalText.Substring(0, i);
                yield return new WaitForSeconds(DisplaySpeed);
            }
        }
        yield return new WaitForSeconds(DisplaySpeed);

        OnDoneAllScreens.Invoke();
    }
}
