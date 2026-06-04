using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.GraphicsBuffer;

[ExecuteInEditMode]
public class socketCount : MonoBehaviour
{
    public XRSocketInteractor[] sockets; // 放置物體的 Socket
    public GameObject[] objectsToPlace; // 可抓取的物體 (7~12)
    public GameObject[] cubes; // 計數達到 2 時顯示的對應 Cube
    public GameObject[] cube1111; // 計數達到 1 時顯示的對應 Cube

    public AudioSource audioSource;
    public AudioSource audioSourceerror1;
    public AudioSource audioSourceerror2;

    private int[] counters; // 計數器對應每個物體
    private int[] correctSocketIndices; // 每個物體的正確 Socket 索引

    private void Start()
    {
        counters = new int[objectsToPlace.Length];
        correctSocketIndices = new int[objectsToPlace.Length];

        // 初始化正確的對應關係（假設物體7對應Socket1，以此類推）
        for (int i = 0; i < objectsToPlace.Length; i++)
        {
            correctSocketIndices[i] = i; // 物體7對應Socket1 (索引0)
        }

        foreach (var socket in sockets)
        {
            socket.selectEntered.AddListener(OnSocketSelectEnter);
        }
    }

    private void OnSocketSelectEnter(SelectEnterEventArgs args)
    {
        XRSocketInteractor socket = args.interactorObject as XRSocketInteractor;
        GameObject placedObject = args.interactableObject.transform.gameObject;

        int objectIndex = System.Array.IndexOf(objectsToPlace, placedObject);
        int socketIndex = System.Array.IndexOf(sockets, socket);

        if (objectIndex != -1 && socketIndex != -1)
        {
            if (socketIndex == correctSocketIndices[objectIndex])
            {
                // 物體放置在正確的 Socket
                counters[objectIndex] = 0;
                HideCube(objectIndex);

                audioSource.Play();
            }
            else
            {
                XRBaseInteractable interactable = args.interactableObject as XRBaseInteractable;
                if (interactable != null)
                {
                    // 強制取消與插槽的互動
                    var interactionManager = interactable.interactionManager;
                    interactionManager.SelectExit(args.interactorObject, interactable);
                }
                testtttt222 snapBackScript = args.interactableObject.transform.GetComponent<testtttt222>();
                if (snapBackScript != null)
                {
                    snapBackScript.ResetPosition();
                }
                // 物體放置在非正確的 Socket
                counters[objectIndex]++;

                if (counters[objectIndex] == 1)
                {
                    ShowCube2(objectIndex);
                    audioSourceerror1.Play();
                }
                if (counters[objectIndex] == 2)
                {
                    LogSystem.instance.PredictionResult = null;
                    LogSystem.LogAction("學習者同一塊拼圖拼錯數次");
                    LogSystem.instance.InteractionFailures++;
                    ShowCube(objectIndex);
                    HideCube2(objectIndex);
                    // 播放該物件的 AudioSource
                    AudioSource objAudio = objectsToPlace[objectIndex].GetComponent<AudioSource>();
                    if (objAudio != null)
                    {
                        objAudio.Play();
                        
                    }
                    audioSourceerror1.Play();
                }
                if (counters[objectIndex] >2)
                {
                    ShowCube(objectIndex);
                    HideCube2(objectIndex);
                    audioSourceerror1.Play();

                }


            }

            // 重置其他物體的計數器
            ResetOtherCounters(objectIndex);
        }
    }

    private void ResetOtherCounters(int excludedIndex)
    {
        for (int i = 0; i < counters.Length; i++)
        {
            if (i != excludedIndex)
            {
                counters[i] = 0;
                HideCube(i);
                HideCube2(i);
            }
        }
    }

    private void ShowCube(int index)
    {
        if (cubes[index] != null)
        {
            cubes[index].SetActive(true);
        }
    }

    private void HideCube(int index)
    {
        if (cubes[index] != null)
        {
            cubes[index].SetActive(false);
        }
    }
    private void ShowCube2(int index)
    {
        if (cube1111[index] != null)
        {
            cube1111[index].SetActive(true);
        }
    }

    private void HideCube2(int index)
    {
        if (cube1111[index] != null)
        {
            cube1111[index].SetActive(false);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(socketCount))]
    public class SocketInteractionManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            socketCount manager = (socketCount)target;

            if (manager.counters == null || manager.counters.Length != manager.objectsToPlace.Length)
            {
                manager.counters = new int[manager.objectsToPlace.Length];
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Object Counters", EditorStyles.boldLabel);

            for (int i = 0; i < manager.objectsToPlace.Length; i++)
            {
                string objectName = manager.objectsToPlace[i] != null ? manager.objectsToPlace[i].name : "Unnamed Object";
                EditorGUILayout.LabelField($"{objectName}: {manager.counters[i]}");
            }

            // 強制刷新 Inspector 顯示
            if (GUI.changed)
            {
                EditorUtility.SetDirty(manager);
            }
        }

        private void OnEnable()
        {
            EditorApplication.update += ForceRepaint;
        }

        private void OnDisable()
        {
            EditorApplication.update -= ForceRepaint;
        }

        private void ForceRepaint()
        {
            Repaint();
        }
    }
#endif
}
