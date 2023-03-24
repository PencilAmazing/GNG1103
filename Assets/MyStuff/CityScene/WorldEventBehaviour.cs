using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class WorldEventBehaviour : PlayableBehaviour
{
	public class Spaghetti : MonoBehaviour
	{
		public bool IsColliding = false;
		public bool HasCollided = false;
		public void OnTriggerEnter(Collider other) { IsColliding = true; HasCollided = true; }
		public void OnTriggerExit(Collider other) => IsColliding = false;
		public void OnTriggerStay(Collider other)
		{
			HasCollided = true;
			IsColliding = true;
		}
	}

	//public Collider binding;
	public float PauseTime;
	float buffer;
	public TrackFunction function;
	public TrackUntil until;
	public Collider TriggerCollider;
	public int pings;
	int NumberPings = 0;
	public bool IgnoreTrigger;

	// Called when the owning graph starts playing
	public override void OnGraphStart(Playable playable)
	{
		if (until == TrackUntil.WaitForTrigger) {
			if (!TriggerCollider.GetComponent<Spaghetti>()) {
				TriggerCollider.gameObject.AddComponent<Spaghetti>();
			}
		}
		buffer = 0;
	}

	// Called when the owning graph stops playing
	public override void OnGraphStop(Playable playable)
	{
	}

	// Called when the state of the playable is set to Play
	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		// WARNING gets called every frame when paused
	}

	// Called when the state of the playable is set to Paused
	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		var duration = playable.GetDuration();
		var count = playable.GetTime() + info.deltaTime;

		if ((info.effectivePlayState == PlayState.Paused && count > duration) || playable.GetGraph().GetRootPlayable(0).IsDone()) {
			// Execute your finishing logic here:
			if (function == TrackFunction.PingPongForTime && until == TrackUntil.WaitForTrigger) {
				// if we're not ponging and we didnt collide yet
				if (NumberPings / 2 < pings || !HasCollided()) {
					NumberPings += 1;
					//Debug.Log("Ping");
					ReverseSpeed(playable);
				}
			}
		} else {
			// if we already pinged once, repong
			if (function == TrackFunction.PingPongForTime && until == TrackUntil.WaitForTrigger && NumberPings > 0) {
				NumberPings += 1;
				ReverseSpeed(playable);
				//Debug.Log("Pong");
			}
		}
	}

	// Called each frame while the state is set to Play
	public override void PrepareFrame(Playable playable, FrameData info)
	{
		if (function == TrackFunction.SimplePause) {
			if (until == TrackUntil.Seconds) {
				Until(playable);
			} else if (until == TrackUntil.WaitForTrigger && !HasCollided()) {
				SkipFrame(playable);
			}
		}
		//else if (function == TrackFunction.PingPongForTime) {
		//	if (until == TrackUntil.Seconds) {
		//		Until(playable);
		//	}
		//}
	}

	private void Until(Playable playable)
	{
		if ((buffer <= PauseTime)) {
			buffer += Time.deltaTime; // Global delta because we're paused
			SkipFrame(playable);
		}
	}
	private void SkipFrame(Playable playable)
	{
		var root = playable.GetGraph().GetRootPlayable(0);
		root.SetTime(root.GetPreviousTime());
	}
	private void ReverseSpeed(Playable playable)
	{
		var root = playable.GetGraph().GetRootPlayable(0);
		root.SetSpeed(-root.GetSpeed());
	}
	private bool HasCollided()
	{
		return IgnoreTrigger || (TriggerCollider.GetComponent<Spaghetti>() && TriggerCollider.GetComponent<Spaghetti>().HasCollided);
	}
}
