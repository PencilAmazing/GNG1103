using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCallback : MonoBehaviour
{
    public string DetectTag = "";
    public UnityEngine.Events.UnityEvent OnTriggerEnterCallback;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (DetectTag.CompareTo("") == 0 || other.CompareTag(DetectTag))
            OnTriggerEnterCallback.Invoke();
    }
}
