using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class countOne : MonoBehaviour
{
    public GameObject[] trackedObjects = new GameObject[6];

    [Header("每個物件開啟次數")]
    public int[] openCounts = new int[6];

    [Header("每個物件關閉次數")]
    public int[] closeCounts = new int[6];

    private bool[] hasOpenedOnce = new bool[6];
    private bool[] hasClosedOnce = new bool[6];

    [Header("每個物件第一次開啟事件")]
    public UnityEvent[] onFirstOpen = new UnityEvent[6];

    [Header("每個物件第一次關閉事件")]
    public UnityEvent[] onFirstClose = new UnityEvent[6];

    private bool[] lastStates = new bool[6];

    private void Start()
    {
        for (int i = 0; i < trackedObjects.Length; i++)
        {
            if (trackedObjects[i] != null)
                lastStates[i] = trackedObjects[i].activeSelf;
        }
    }

    void Update()
    {
        for (int i = 0; i < trackedObjects.Length; i++)
        {
            if (trackedObjects[i] == null) continue;

            bool currentState = trackedObjects[i].activeSelf;

            if (currentState != lastStates[i])
            {
                if (currentState)
                {
                    openCounts[i]++;
                    if (!hasOpenedOnce[i])
                    {
                        hasOpenedOnce[i] = true;
                        onFirstOpen[i]?.Invoke();
                    }
                }
                else
                {
                    closeCounts[i]++;
                    if (!hasClosedOnce[i])
                    {
                        hasClosedOnce[i] = true;
                        onFirstClose[i]?.Invoke();
                    }
                }

                lastStates[i] = currentState;
            }
        }
    }
}
