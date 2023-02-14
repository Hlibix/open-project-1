using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "InteractionSO", menuName = "UI/Interaction")]
public class InteractionSO : ScriptableObject
{
    [SerializeField]
    private LocalizedString _interactionName;

    [SerializeField]
    private Sprite _interactionIcon;

    [SerializeField]
    private InteractionType _interactionType;

    public Sprite          InteractionIcon => _interactionIcon;
    public LocalizedString InteractionName => _interactionName;
    public InteractionType InteractionType => _interactionType;
}