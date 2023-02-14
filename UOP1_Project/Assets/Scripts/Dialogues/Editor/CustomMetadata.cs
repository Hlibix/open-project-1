using System;
using UnityEngine;
using UnityEngine.Localization.Metadata;

[Metadata(AllowedTypes = MetadataType.AllTableEntries)] // Hint to the editor to only show this type for a Locale
[Serializable]
public class ActorInfo : IMetadata
{
    [SerializeField]
    private ActorID actor;
}

public class ChoiceInfo : IMetadata
{
    [SerializeField]
    private ChoiceActionType choiceAction;
}