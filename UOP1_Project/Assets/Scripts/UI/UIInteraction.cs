using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour
{
    [SerializeField]
    private List<InteractionSO> _listInteractions;

    [SerializeField]
    private Image _interactionIcon;

    public void FillInteractionPanel(InteractionType interactionType)
    {
        if (_listInteractions != null
         && _listInteractions.Exists(o => o.InteractionType == interactionType))
        {
            var icon = _listInteractions.Find(o => o.InteractionType == interactionType).InteractionIcon;
            _interactionIcon.sprite = icon;
        }
    }
}