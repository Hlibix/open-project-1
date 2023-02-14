using UnityEditor;
using UnityEngine;

public enum StepType
{
    Dialogue,
    GiveItem,
    CheckItem
}

[CreateAssetMenu(fileName = "step", menuName = "Quests/Step")]
public class StepSO : SerializableScriptableObject
{
    [Tooltip("The Character this mission will need interaction with")]
    [SerializeField]
    private ActorSO _actor;

    [Tooltip("The dialogue that will be diplayed befor an action, if any")]
    [SerializeField]
    private DialogueDataSO _dialogueBeforeStep;

    [Tooltip("The dialogue that will be diplayed when the step is achieved")]
    [SerializeField]
    private DialogueDataSO _completeDialogue;

    [Tooltip("The dialogue that will be diplayed if the step is not achieved yet")]
    [SerializeField]
    private DialogueDataSO _incompleteDialogue;

    [SerializeField]
    private StepType _type;

    [Tooltip("The item to check/give")]
    [SerializeField]
    private ItemSO _item;

    [SerializeField]
    private bool _hasReward;

    [Tooltip("The item to reward if any")]
    [SerializeField]
    private ItemSO _rewardItem;

    [SerializeField]
    private int _rewardItemCount = 1; // by default the reward is 1 item (if any)

    [SerializeField]
    private bool _isDone;

    [SerializeField]
    private VoidEventChannelSO _endStepEvent;

    public DialogueDataSO DialogueBeforeStep
    {
        get => _dialogueBeforeStep;
        set => _dialogueBeforeStep = value;
    }

    public DialogueDataSO CompleteDialogue
    {
        get => _completeDialogue;
        set => _completeDialogue = value;
    }

    public DialogueDataSO IncompleteDialogue
    {
        get => _incompleteDialogue;
        set => _incompleteDialogue = value;
    }

    public ItemSO Item
    {
        get => _item;
        set => _item = value;
    }

    public bool   HasReward       => _hasReward;
    public ItemSO RewardItem      => _rewardItem;
    public int    RewardItemCount => _rewardItemCount;

    public VoidEventChannelSO EndStepEvent
    {
        set => _endStepEvent = value;
        get => _endStepEvent;
    }

    public StepType Type => _type;

    public bool IsDone
    {
        get => _isDone;
        set => _isDone = value;
    }

    public ActorSO Actor => _actor;

    public void FinishStep()
    {
        if (_endStepEvent != null)
        {
            _endStepEvent.RaiseEvent();
        }

        _isDone = true;
    }

    //This function is a leftover of the QuestEditorWindow, which is currently non functional
    public DialogueDataSO StepToDialogue()
    {
        var dialogueData = CreateInstance<DialogueDataSO>();
        /*
             dialogueData.SetActor(Actor);
              if (DialogueBeforeStep != null)
                {
                   dialogueData = new DialogueDataSO(DialogueBeforeStep);
                    if (DialogueBeforeStep.Choices != null)
                    {
                      if (CompleteDialogue != null)
                      {
                          if (dialogueData.Choices.Count > 0)
                            {

                             if (dialogueData.Choices[0].NextDialogue == null)
                                  dialogueData.Choices[0].SetNextDialogue(CompleteDialogue);
                         }
                      }
                      if (IncompleteDialogue != null)
                        {
                          if (dialogueData.Choices.Count > 1)
                            {
                              if (dialogueData.Choices[1].NextDialogue == null)
                                  dialogueData.Choices[1].SetNextDialogue(IncompleteDialogue);
                           }

                     }

                 }

             }

             */
        return dialogueData;
    }

#if UNITY_EDITOR
    /// <summary>
    /// This function is only useful for the Questline Tool in Editor to remove a Step
    /// </summary>
    /// <returns>The local path</returns>
    public string GetPath()
    {
        return AssetDatabase.GetAssetPath(this);
    }
#endif
}