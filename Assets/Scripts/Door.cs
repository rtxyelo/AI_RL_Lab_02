using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorActivator : MonoBehaviour
{
    public UnityEvent onActivate { get; } = new UnityEvent();
    public UnityEvent onActivateByBlock { get; } = new UnityEvent();
    public UnityEvent onDeactivateByBlock { get; } = new UnityEvent();
    public UnityEvent onDeactivate { get; } = new UnityEvent();
}

public class Door : MonoBehaviour
{
    private DoorActivator[] activators;

    private int activeCounter = 0;
    public UnityEvent onDoorOpen;
    public UnityEvent onDoorOpenByBlock;
    public UnityEvent onDoorClose;
    public UnityEvent onDoorCloseByBlock;
    private void OnActivate()
    {
        activeCounter++;
        if (activeCounter == activators.Length) Open();
    }

    private void OnActivateByBlock()
    {
        activeCounter++;
        if (activeCounter == activators.Length) OpenByBlock();
    }

    private void OnDeactivateByBlock()
    {
        activeCounter--;
        if (activeCounter < activators.Length) CloseByBlock();
    }

    private void OnDeactivate()
    {
        activeCounter--;
        //if (activeCounter < activators.Length) Close();
    }

    public void ResetActivators(DoorActivator[] newActivators)
    {
        if (activators != null)
            foreach (var activator in activators)
            {
                activator.onActivate.RemoveListener(OnActivate);
                activator.onActivateByBlock.RemoveListener(OnActivateByBlock);
                activator.onDeactivateByBlock.RemoveListener(OnDeactivateByBlock);
                activator.onDeactivate.RemoveListener(OnDeactivate);
            }
        activators = newActivators;
        gameObject.SetActive(true);
        foreach (var activator in activators)
        {
            activator.onActivate.AddListener(OnActivate);
            activator.onActivateByBlock.AddListener(OnActivateByBlock);
            activator.onDeactivateByBlock.AddListener(OnDeactivateByBlock);
            activator.onDeactivate.AddListener(OnDeactivate);
        }
    }

    void Open()
    {
        gameObject.SetActive(false);
        onDoorOpen.Invoke();
    }
    void OpenByBlock()
    {
        gameObject.SetActive(false);
        onDoorOpenByBlock.Invoke();
    }

    void CloseByBlock()
    {
        gameObject.SetActive(true);
        onDoorCloseByBlock.Invoke();
    }

    void Close() 
    { 
        gameObject.SetActive(true);
        onDoorClose.Invoke();
    }
}
