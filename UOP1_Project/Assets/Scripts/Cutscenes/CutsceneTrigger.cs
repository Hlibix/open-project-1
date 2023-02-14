using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Class to trigger a cutscene.
/// </summary>
public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField]
    private bool _playOnStart;

    [SerializeField]
    private bool _playOnce;

    [SerializeField]
    private QuestManagerSO _questManager;

    [Header("Listening to")]
    [SerializeField]
    private VoidEventChannelSO _playSpeceficCutscene;

    [Header("Broadcasting on")]
    [SerializeField]
    private PlayableDirectorChannelSO _playCutsceneEvent;

    private PlayableDirector _playableDirector;

    private void Start()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        if (_playOnStart)
        {
            if (_playCutsceneEvent != null)
            {
                _playCutsceneEvent.RaiseEvent(_playableDirector);
            }
        }

        //Check if we are playing a new game, we should play the intro cutscene
        if (_questManager)
        {
            if (_questManager.IsNewGame())
            {
                _playableDirector.Play();
            }
        }
    }

    private void OnEnable()
    {
        _playSpeceficCutscene.OnEventRaised += PlaySpecificCutscene;
    }

    private void OnDisable()
    {
        _playSpeceficCutscene.OnEventRaised -= PlaySpecificCutscene;
    }

    private void PlaySpecificCutscene()
    {
        if (_playCutsceneEvent != null)
        {
            _playCutsceneEvent.RaiseEvent(_playableDirector);
        }

        if (_playOnce)
        {
            Destroy(this);
        }
    }

    //THIS WILL BE REMOVED LATER WHEN WE HAVE ALL EVENTS SET UP, NOW WE ONLY NEED IT TO TEST CUTSCENE WITH TRIGGER
    //Remember to remove collider componenet when we remove this
    private void OnTriggerEnter(Collider other)
    {
        //Fake event raise to test quicker
        _playSpeceficCutscene.RaiseEvent();
    }
}