using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockAppController : AppControllerBase
{
    [SerializeField] private Transform playerTransform;
    public override Transform PlayerTransform => playerTransform;

    [SerializeField] private Rigidbody playerBody;
    public override Rigidbody PlayerBody => playerBody;

    [SerializeField] private Camera mainCamera;
    public override Camera MainCamera => mainCamera;

    public override string BundleVersionCode => GeneratedInfo.BundleVersionCode;

    private MockUIManager uiManager = new MockUIManager();
    public override IUIManager UIManager => UIManager;

    public override void SetPlayerPosition(Vector3 WorldPosition)
    {
        playerTransform.position = WorldPosition;
    }

    public override void SetPlayerPosition(Vector3 WorldPosition, Quaternion WorldRotation)
    {
        playerTransform.position = WorldPosition;
        playerTransform.rotation = WorldRotation;
    }

    public override void SetPlayerRotation(Quaternion WorldRotation)
    {
        playerTransform.rotation = WorldRotation;
    }

    private void Start()
    {
        TransitionController.Instance.RevealScene();
    }
}

public class MockUIManager : IUIManager
{
    public event Action<GameObject> OnMakeGrabbable;

    public void MakeGrabbable(GameObject Item)
    {
        OnMakeGrabbable?.Invoke(Item);
    }
}