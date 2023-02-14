using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    private DialogueManager _dialogueManager;

    [SerializeField]
    private InputReader _inputReader;

    [SerializeField]
    private GameStateSO _gameState;

    [Header("Listening on")]
    [SerializeField]
    private PlayableDirectorChannelSO _playCutsceneEvent;

    [SerializeField]
    public DialogueLineChannelSO _playDialogueEvent;

    [SerializeField]
    public VoidEventChannelSO _pauseTimelineEvent;

    [SerializeField]
    public VoidEventChannelSO _onLineEndedEvent;

    private PlayableDirector _activePlayableDirector;
    private bool             _isPaused;

    private bool IsCutscenePlaying => _activePlayableDirector.playableGraph.GetRootPlayable(0).GetSpeed() != 0d;

    private void OnEnable()
    {
        _inputReader.AdvanceDialogueEvent += OnAdvance;
    }

    private void OnDisable()
    {
        _inputReader.AdvanceDialogueEvent -= OnAdvance;
    }

    private void Start()
    {
        _playCutsceneEvent.OnEventRaised  += PlayCutscene;
        _playDialogueEvent.OnEventRaised  += PlayDialogueFromClip;
        _pauseTimelineEvent.OnEventRaised += PauseTimeline;
        _onLineEndedEvent.OnEventRaised   += LineEnded;
    }

    private void PlayCutscene(PlayableDirector activePlayableDirector)
    {
        _inputReader.EnableDialogueInput();
        _gameState.UpdateGameState(GameState.Cutscene);
        _activePlayableDirector = activePlayableDirector;

        _isPaused = false;
        _activePlayableDirector.Play();
        _activePlayableDirector.stopped += HandleDirectorStopped;
    }

    private void CutsceneEnded()
    {
        if (_activePlayableDirector != null)
        {
            _activePlayableDirector.stopped -= HandleDirectorStopped;
        }

        _gameState.UpdateGameState(GameState.Gameplay);
        _inputReader.EnableGameplayInput();
        _dialogueManager.CutsceneDialogueEnded();
    }

    public void LineEnded()
    {
        _dialogueManager.CutsceneDialogueEnded();
    }

    private void HandleDirectorStopped(PlayableDirector director)
    {
        CutsceneEnded();
    }

    private void PlayDialogueFromClip(LocalizedString dialogueLine, ActorSO actor)
    {
        _dialogueManager.DisplayDialogueLine(dialogueLine, actor);
    }

    /// <summary>
    /// This callback is executed when the player presses the button to advance dialogues. If the Timeline is currently paused due to a <c>DialogueControlClip</c>, it will resume its playback.
    /// </summary>
    private void OnAdvance()
    {
        if (_isPaused)
        {
            ResumeTimeline();
            LineEnded();
        }
    }

    /// <summary>
    /// Called by <c>DialogueControlClip</c> on the Timeline.
    /// </summary>
    private void PauseTimeline()
    {
        _isPaused = true;
        _activePlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    private void ResumeTimeline()
    {
        _isPaused = false;
        _activePlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}