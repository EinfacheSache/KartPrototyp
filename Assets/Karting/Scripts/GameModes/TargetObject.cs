﻿﻿using UnityEngine;

public abstract class TargetObject : MonoBehaviour
{

    [Tooltip("The amount of time the pickup gives in secs")]
    public float TimeGained;

    [Tooltip("Layers to trigger with")]
    public LayerMask layerMask;

    [Tooltip("The point at which the collect VFX is spawned")]
    public Transform CollectVFXSpawnPoint;

    [Header("Sounds")]

    [Tooltip("Sound played when receiving damages")]
    public AudioClip CollectSound;

    [HideInInspector]
    public bool active;

    void OnEnable()
    {
        active = true;
    }
    
    protected void Register()
    {
        Objective.OnRegisterPickup?.Invoke(this);
    }
}
