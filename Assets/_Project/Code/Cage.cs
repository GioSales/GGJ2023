using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : Interactable
{
    [SerializeField] private GameObject sideBarToOpen;
    [SerializeField] private float openCageBarHeight = 10f;
    [SerializeField] private float tweenDuration = 1;

    public override void OnInteract()
    {
        OpenCage();
    }

    [ContextMenu("Open Cage Test")]
    private void OpenCage()
    {
        sideBarToOpen.transform.DOMoveY(openCageBarHeight, tweenDuration).SetRelative();
    }
}
