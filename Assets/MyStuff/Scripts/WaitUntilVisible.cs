using UnityEngine;
using UnityEngine.Events;

public class WaitUntilVisible : MonoBehaviour
{
    public Renderer IsVisible;
    public UnityEvent DoOnVisible;
    public bool AttemptExecution { get; set; } = false;
    private bool HasExecuted = false;
    private float lookatTimeBuffer = 0;
    public float TriggerTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (AttemptExecution && !HasExecuted && IsVisible.isVisible)
        {
            if (lookatTimeBuffer <= TriggerTime)
            {
                lookatTimeBuffer += Time.deltaTime;
            }
            else
            {
                HasExecuted = true;
                DoOnVisible.Invoke();
            }
        }

    }
}
