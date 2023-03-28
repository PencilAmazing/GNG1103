using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class InterSceneInfo
{
	/// <summary>
	/// Do we have to fade out on scene start?
	/// </summary>
	public static bool CompleteTransition = true;
}


public class SwitchScene : MonoBehaviour
{

	public SpriteRenderer FadeSprite;
	public bool LookForIncompleteTransition = false;
	[Min(1)]
	public float FadeDurationSeconds = 10.0f;

	void Start()
	{
		if (FadeSprite == null) return;

		if (LookForIncompleteTransition && InterSceneInfo.CompleteTransition) {
			StartCoroutine(FadeSpriteOut());
		}
	}

	IEnumerator FadeSpriteOut()
	{
		Color col = new Color(0, 0, 0, 1);
		float time = 0;
		while (time < FadeDurationSeconds) {
			time += Time.deltaTime;
			col.a = 1 - Mathf.Clamp01(time / FadeDurationSeconds);
			FadeSprite.color = col;
			yield return null;
		}
		// We're done, clean up
		InterSceneInfo.CompleteTransition = false;
	}

	IEnumerator FadeSpriteIn(string SceneName)
	{
		Color col = new Color(0, 0, 0, 0);
		float time = 0;
		while (FadeSprite != null && time < FadeDurationSeconds) {
			time += Time.deltaTime;
			col.a = Mathf.Clamp01(time / FadeDurationSeconds);
			FadeSprite.color = col;
			yield return null;
		}
		// Sprite is now black
		// notify we need to complete transition on other side
		InterSceneInfo.CompleteTransition = true;
		SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
	}

	public void FadeSpriteInAndSwitchScene(string SceneName)
	{
		Debug.Log("Begin scene transition");
		StartCoroutine(FadeSpriteIn(SceneName));
	}
}
