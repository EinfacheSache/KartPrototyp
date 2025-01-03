﻿using System.Collections.Generic;
using UnityEngine;

public class ObjectiveHUDManger : MonoBehaviour
{
    [Tooltip("UI panel containing the layoutGroup for displaying objectives")]
    public UITable objectivePanel;
    [Tooltip("Prefab for the primary objectives")]
    public PoolObjectDef primaryObjectivePrefab;
    [Tooltip("Prefab for the primary objectives")]
    public PoolObjectDef secondaryObjectivePrefab;

    Dictionary<Objective, ObjectiveToast> m_ObjectivesDictionary;

    void Awake()
    {
        m_ObjectivesDictionary = new Dictionary<Objective, ObjectiveToast>();
    }

    public void RegisterObjective(Objective objective)
    {
        objective.onUpdateObjective += OnUpdateObjective;

    }

    public void UnregisterObjective(Objective objective)
    {
        objective.onUpdateObjective -= OnUpdateObjective;

        // if the objective if in the list, make it fade out, and remove it from the list
        if (m_ObjectivesDictionary.TryGetValue(objective, out ObjectiveToast toast))
            toast.Complete();
        
        m_ObjectivesDictionary.Remove(objective);
    }

    void OnUpdateObjective(UnityActionUpdateObjective updateObjective)
    {
        if (m_ObjectivesDictionary.TryGetValue(updateObjective.objective, out ObjectiveToast toast))
            //&& !string.IsNullOrEmpty(descriptionText))
        {
            // set the new updated description for the objective, and forces the content size fitter to be recalculated
            Canvas.ForceUpdateCanvases();
            if (!string.IsNullOrEmpty(updateObjective.descriptionText))
                toast.SetDescriptionText(updateObjective.descriptionText);

            if (!string.IsNullOrEmpty(updateObjective.counterText))
                toast.counterTextContent.text = updateObjective.counterText;

            RectTransform toastRectTransform = toast.GetComponent<RectTransform>();
            if (toastRectTransform != null) UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(toastRectTransform);
            
        }
    }
}
