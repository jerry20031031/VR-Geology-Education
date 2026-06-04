using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class socket : MonoBehaviour
{
    public XRSocketInteractor socketInteractor;

    private void OnEnable()
    {
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.AddListener(OnObjectSelected);
            socketInteractor.selectExited.AddListener(OnObjectDeselected);
        }
        else
        {
            Debug.LogWarning("socketInteractor is null on OnDisable.");
        }
    }

    private void OnDisable()
    {
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnObjectSelected);
            socketInteractor.selectExited.RemoveListener(OnObjectDeselected);
        }
        else
        {
            Debug.LogWarning("socketInteractor is null on OnDisable.");
        }
    }

    private void OnObjectSelected(SelectEnterEventArgs args)
    {
        if (args.interactableObject == null || args.interactableObject.transform == null)
        {
            Debug.LogWarning("Selected object is null or has been destroyed.");
            return;
        }

        GameObject selectedObject = args.interactableObject.transform.gameObject;

        if (selectedObject == null)
        {
            Debug.LogWarning("Selected GameObject is already destroyed.");
            return;
        }

        // 解決帶子物件的層級問題
        if (selectedObject.transform.parent != null)
        {
            Debug.Log($"Detaching {selectedObject.name} from parent {selectedObject.transform.parent.name}");
            selectedObject.transform.SetParent(null); // 將物件從父物件中脫離
        }

        Debug.Log($"Object {selectedObject.name} has been placed in the socket.");
    }

    private void OnObjectDeselected(SelectExitEventArgs args)
    {
        if (args.interactableObject == null || args.interactableObject.transform == null)
        {
            Debug.LogWarning("Deselected object is null or has been destroyed.");
            return;
        }

        GameObject deselectedObject = args.interactableObject.transform.gameObject;

        if (deselectedObject == null)
        {
            Debug.LogWarning("Deselected GameObject is already destroyed.");
            return;
        }

        Debug.Log($"Object {deselectedObject.name} has been removed from the socket.");
    }
}
