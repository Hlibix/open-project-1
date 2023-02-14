﻿using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogueClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField]
    private DialogueBehaviour _template;

    [HideInInspector]
    public DialogueLineChannelSO PlayDialogueEvent;

    [HideInInspector]
    public VoidEventChannelSO PauseTimelineEvent;

    // Having ClipCaps set to None makes sure that the clips can't be blended, extrapolated, looped, etc.
    public ClipCaps clipCaps => ClipCaps.None;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialogueBehaviour>.Create(graph, _template);

        _template.PlayDialogueEvent  = PlayDialogueEvent;
        _template.PauseTimelineEvent = PauseTimelineEvent;

        return playable;
    }
}