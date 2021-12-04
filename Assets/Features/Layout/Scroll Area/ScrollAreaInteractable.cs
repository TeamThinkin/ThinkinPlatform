using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrollAreaInteractable : XRBaseInteractable
{
    [SerializeField] private bool AllowXMovement = true;
    [SerializeField] private bool AllowYMovement;

    private ScrollArea scrollArea;

    private Vector3 worldReferencePoint;
    private Vector3 lastWorldReferencePoint;
    private MomentumVector3 dragDirection = new MomentumVector3(0.05f);
    private Plane referencePlane = new Plane();
    private Ray ray = new Ray();
    private XRBaseInteractor interactor;
    private bool isDragging;

    override protected void Awake()
    {
        scrollArea = GetComponent<ScrollArea>();
    }

    private void Update()
    {
        if (isDragging)
        {
            worldReferencePoint = getReferencePoint();
            var localDirection = transform.InverseTransformDirection(worldReferencePoint - lastWorldReferencePoint);
            if (!AllowXMovement) localDirection.x = 0;
            if (!AllowYMovement) localDirection.y = 0;
            localDirection.z = 0;

            dragDirection.Set(localDirection);
            
            lastWorldReferencePoint = worldReferencePoint;
        }
        else
        {
            dragDirection.Update();
        }

        scrollArea.OffsetScrollPosition(dragDirection.Value);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        if (args.interactable != this) return;

        interactor = args.interactor;
        lastWorldReferencePoint = getReferencePoint();
        dragDirection.Value = Vector3.zero;
        isDragging = true;
    }

    private Vector3 getReferencePoint()
    {
        referencePlane.SetNormalAndPosition(transform.forward, transform.position);
        ray.origin = interactor.transform.position;
        ray.direction = interactor.transform.forward;
        return referencePlane.GetRaycastPoint(ray);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        isDragging = false;
    }
}
