using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorGlue : MonoBehaviour
{
    public UnityEvent OnStateExitCallback;

    public void OnStateExit()
    {
        OnStateExitCallback.Invoke();
    }
}
