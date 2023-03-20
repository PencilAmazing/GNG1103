using UnityEngine;
using UnityEngine.Playables;

public enum TrackFunction
{
	SimplePause,
	PingPongForTime
}

public enum TrackUntil
{
	WaitForTrigger,
	Seconds,
}

[System.Serializable]
public class WorldEventTrack : PlayableAsset
{
	public TrackFunction Function;
	public TrackUntil Until;
	[Min(0.1f)]
	public float PauseTime;
	[Min(1)]
	public int pings;
	public ExposedReference<Collider> TriggerCollider;

	// Factory method that generates a playable based on this asset
	public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
	{
		var playable = ScriptPlayable<WorldEventBehaviour>.Create(graph);
		var behaviour = playable.GetBehaviour();
		behaviour.function = Function;
		behaviour.until = Until;
		behaviour.pings = pings;
		behaviour.TriggerCollider = TriggerCollider.Resolve(graph.GetResolver());
		behaviour.PauseTime = PauseTime;
		return playable;
	}

}
