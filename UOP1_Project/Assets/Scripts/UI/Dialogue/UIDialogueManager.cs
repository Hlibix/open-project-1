using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class UIDialogueManager : MonoBehaviour
{
    [SerializeField]
    private LocalizeStringEvent _lineText;

    [SerializeField]
    private LocalizeStringEvent _actorNameText;

    [SerializeField]
    private GameObject _actorNamePanel;

    [SerializeField]
    private GameObject _mainProtagonistNamePanel;

    [SerializeField]
    private UIDialogueChoicesManager _choicesManager;

    [Header("Listening to")]
    [SerializeField]
    private DialogueChoicesChannelSO _showChoicesEvent;

    private void OnEnable()
    {
        _showChoicesEvent.OnEventRaised += ShowChoices;
    }

    private void OnDisable()
    {
        _showChoicesEvent.OnEventRaised -= ShowChoices;
    }

    public void SetDialogue(LocalizedString dialogueLine, ActorSO actor, bool isMainProtagonist)
    {
        _choicesManager.gameObject.SetActive(false);
        _lineText.StringReference = dialogueLine;

        _actorNamePanel.SetActive(!isMainProtagonist);
        _mainProtagonistNamePanel.SetActive(isMainProtagonist);

        if (!isMainProtagonist)
        {
            _actorNameText.StringReference = actor.ActorName;
        }
        //Protagonist's LocalisedString is provided on the GameObject already
    }

    private void ShowChoices(List<Choice> choices)
    {
        _choicesManager.FillChoices(choices);
        _choicesManager.gameObject.SetActive(true);
    }

    private void HideChoices()
    {
        _choicesManager.gameObject.SetActive(false);
    }
}