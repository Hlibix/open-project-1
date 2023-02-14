﻿using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField]
    private NPCMovementConfigSO _npcMovementConfig;

    [SerializeField]
    private NPCMovementEventChannelSO _channel;

    public NPCMovementConfigSO NPCMovementConfig => _npcMovementConfig;

    private void OnEnable()
    {
        if (_channel != null)
        {
            _channel.OnEventRaised += Respond;
        }
    }

    private void Respond(NPCMovementConfigSO value)
    {
        _npcMovementConfig = value;
    }
}