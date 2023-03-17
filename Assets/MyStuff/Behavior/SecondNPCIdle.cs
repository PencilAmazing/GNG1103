using UnityEditor.Recorder.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondNPCIdle : StateMachineBehaviour
{
	public enum IAmLazy
	{
		NextLine,
		TurnLeft
	}

	public SecondSceneManager manager;
	public IAmLazy Operation = IAmLazy.NextLine;

	private Quaternion startangle;

	private void NextLine(Animator animator)
	{
		if (manager == null) {
			Debug.Log("Searching for manager outselves");
			foreach (GameObject item in animator.gameObject.scene.GetRootGameObjects()) {
				if (item.GetComponent<SecondSceneManager>() != null) {
					manager = item.GetComponent<SecondSceneManager>();
					break;
				}
			}
		}
		if (manager == null) {
			Debug.LogError("SecondSceneManager not found in root gameobjects in this scene");
			return;
		}

		// Send signal saying that NPC got up
		manager.TriggerNextLine();
		Debug.Log("NPC got up");
	}

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// Yeah the ExitState doesn't work and i don't care to fix it
		base.OnStateEnter(animator, stateInfo, layerIndex);

		if (Operation == IAmLazy.NextLine)
			this.NextLine(animator);
		else if (Operation == IAmLazy.TurnLeft) {
			this.startangle = animator.gameObject.transform.rotation;
			startangle.eulerAngles += new Vector3(0, -90, 0);
			animator.gameObject.transform.rotation = startangle;
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//	base.OnStateUpdate(animator, stateInfo, layerIndex);
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{

	//}

	// OnStateMove is called right after Animator.OnAnimatorMove()
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that processes and affects root motion
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK()
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that sets up animation IK (inverse kinematics)
	//}
}
