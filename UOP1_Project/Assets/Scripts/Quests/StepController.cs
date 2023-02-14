using UnityEngine;

//this script needs to be put on the actor, and takes care of the current step to accomplish.
//the step contains a dialogue and maybe an event.

public class StepController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField]
    private ActorSO _actor;

    [SerializeField]
    private DialogueDataSO _defaultDialogue;

    [SerializeField]
    private QuestManagerSO _questData;

    [SerializeField]
    private GameStateSO _gameStateManager;

    [Header("Listening to channels")]
    [SerializeField]
    private VoidEventChannelSO _winDialogueEvent;

    [SerializeField]
    private VoidEventChannelSO _loseDialogueEvent;

    [SerializeField]
    private IntEventChannelSO _endDialogueEvent;

    [Header("Broadcasting on channels")]
    public DialogueDataChannelSO _startDialogueEvent;

    [Header("Dialogue Shot Camera")]
    public GameObject dialogueShot;

    //check if character is actif. An actif character is the character concerned by the step.
    private DialogueDataSO _currentDialogue;

    public bool isInDialogue; //Consumed by the state machine

    private void Start()
    {
        if (dialogueShot)
        {
            dialogueShot.transform.parent = null;
            dialogueShot.SetActive(false);
        }
    }

    private void PlayDefaultDialogue()
    {
        if (_defaultDialogue != null)
        {
            _currentDialogue = _defaultDialogue;
            StartDialogue();
        }
    }

    //start a dialogue when interaction
    //some Steps need to be instantanious. And do not need the interact button.
    //when interaction again, restart same dialogue.
    public void InteractWithCharacter()
    {
        if (_gameStateManager.CurrentGameState == GameState.Gameplay)
        {
            var displayDialogue = _questData.InteractWithCharacter(_actor, false, false);
            //Debug.Log("dialogue " + displayDialogue + "actor" + _actor);
            if (displayDialogue != null)
            {
                _currentDialogue = displayDialogue;
                StartDialogue();
            }
            else
            {
                PlayDefaultDialogue();
            }
        }
    }

    private void StartDialogue()
    {
        _startDialogueEvent.RaiseEvent(_currentDialogue);
        _endDialogueEvent.OnEventRaised += EndDialogue;
        StopConversation();
        _winDialogueEvent.OnEventRaised  += PlayWinDialogue;
        _loseDialogueEvent.OnEventRaised += PlayLoseDialogue;
        isInDialogue                     =  true;
        if (dialogueShot)
        {
            dialogueShot.SetActive(true);
        }
    }

    private void EndDialogue(int dialogueType)
    {
        _endDialogueEvent.OnEventRaised  -= EndDialogue;
        _winDialogueEvent.OnEventRaised  -= PlayWinDialogue;
        _loseDialogueEvent.OnEventRaised -= PlayLoseDialogue;
        ResumeConversation();
        isInDialogue = false;
        if (dialogueShot)
        {
            dialogueShot.SetActive(false);
        }
    }

    private void PlayLoseDialogue()
    {
        if (_questData != null)
        {
            var displayDialogue = _questData.InteractWithCharacter(_actor, true, false);
            if (displayDialogue != null)
            {
                _currentDialogue = displayDialogue;
                StartDialogue();
            }
        }
    }

    private void PlayWinDialogue()
    {
        if (_questData != null)
        {
            var displayDialogue = _questData.InteractWithCharacter(_actor, true, true);
            if (displayDialogue != null)
            {
                _currentDialogue = displayDialogue;
                StartDialogue();
            }
        }
    }

    private void StopConversation()
    {
        var talkingTo = gameObject.GetComponent<NPC>().talkingTo;
        if (talkingTo != null)
        {
            for (var i = 0; i < talkingTo.Length; ++i)
            {
                talkingTo[i].GetComponent<NPC>().npcState = NPCState.Idle;
            }
        }
    }

    private void ResumeConversation()
    {
        var talkingTo = GetComponent<NPC>().talkingTo;
        if (talkingTo != null)
        {
            for (var i = 0; i < talkingTo.Length; ++i)
            {
                talkingTo[i].GetComponent<NPC>().npcState = NPCState.Talk;
            }
        }
    }
}