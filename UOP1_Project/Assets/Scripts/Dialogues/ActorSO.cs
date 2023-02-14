using UnityEngine;
using UnityEngine.Localization;

public enum ActorID
{
    BH, // Bard hare
    H,  // hamlet
    F,  // felfel
    A,  // ayoud
    T,  // terra
    LC, //Legendary chef
    C,  // Cerise
    N   //nar
}

/// <summary>
/// Scriptable Object that represents an "Actor", that is the protagonist of a Dialogue
/// </summary>
[CreateAssetMenu(fileName = "newActor", menuName = "Dialogues/Actor")]
public class ActorSO : ScriptableObject
{
    [SerializeField]
    private ActorID _actorId;

    [SerializeField]
    private LocalizedString _actorName;

    public ActorID         ActorId   => _actorId;
    public LocalizedString ActorName => _actorName;
}