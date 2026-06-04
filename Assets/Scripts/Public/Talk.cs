using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using cherrydev;
using TMPro;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.Collections;


public class Talk : MonoBehaviour
{
    [Header("解鎖設定")]
    [SerializeField] private TaskSystemUnit3 taskSystem;               // 拖入你的 TaskSystemUnit3 物件
    [SerializeField] private List<int> requiredTaskGroupIDs;
    private DialogBehaviour dialogBehaviour; // 對話行為組件
    [SerializeField] private List<DialogNodeGraph> dialogGraph;  // 對話節點圖
    public DialogNodeGraph ForceInsertDialog;

    private Camera player; // 玩家相機
    private float distance; // 玩家與NPC的距離
    private GameObject talkCanvas; // 對話框Canvas
    private Animator talkAnimator; // 對話框動畫組件
    public float distanceToTalk = 20; // 玩家與NPC的對話距離
    public InputActionReference prev;
    public InputActionReference next;
    [SerializeField] private int ConversationIndex = 0;
    public bool MissionComplete = false;
    public bool AutoConversation = false;
    public Hint hint;
    public Unit3Scene2Talking unit3Scene2Talking;

    void Start()
    {
        // 初始化對話框、提示和玩家相機
        talkCanvas = transform.Find("Canvas").gameObject;
        talkAnimator = talkCanvas.GetComponent<Animator>();
        player = Camera.main;
        dialogBehaviour = GetComponent<DialogBehaviour>();
        talkCanvas.GetComponent<CanvasGroup>().enabled = true;

        // 分配玩家相機給對話框和約束組件
        AssignCamera(player.gameObject);
        AssignConstraint(player.gameObject);
    }

    void Update()
    {
        // 更新對話框的朝向和處理距離相關邏輯
        LookAtPlayer();
        HandleDistance();
    }

    private void LookAtPlayer()
    {
        // 對話框始終朝向玩家
        Vector3 playerPos = player.transform.position;
        playerPos.y = talkCanvas.transform.position.y;
        talkCanvas.transform.LookAt(playerPos);
    }
    public void Missioncomplete()
    {
        MissionComplete = true;
    }

    private void HandleDistance()
    {

        distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < distanceToTalk)
        {

            int requiredGroup = 0;
            if (ConversationIndex < requiredTaskGroupIDs.Count)
                requiredGroup = requiredTaskGroupIDs[ConversationIndex];

            // 檢查是否已解鎖
            // bool isUnlocked = requiredGroup == 0 || taskSystem.IsTaskGroupCompleted(requiredGroup);

            // if (!isUnlocked)
            // {
            //     // 你可以在這裡顯示提示：還沒完成第 X 階段任務，無法對話
            //     return;
            // }

            if (((hint.PointTo && IsInView() && (Input.GetKeyDown(KeyCode.X) || next.action.triggered)) || MissionComplete) && !talkAnimator.GetBool("Active"))
            {
                talkAnimator.SetBool("Active", true);
                MissionComplete = false;

                if (ForceInsertDialog == null)
                {
                    dialogBehaviour.StartDialog(dialogGraph[ConversationIndex]);
                    Debug.Log("Dialog Start");
                }
                else
                {
                    dialogBehaviour.StartDialog(ForceInsertDialog);
                    Debug.Log("Dialog Start");
                }


            }
        }
        else
        {
            talkAnimator.SetBool("Active", false);
            hint.communication = false;
            if (unit3Scene2Talking != null)
                unit3Scene2Talking.SetAllNPCsActive(true);

        }

    }

    public bool IsInView()
    {
        // 檢查NPC是否在玩家的視野範圍內
        Vector3 directionToNPC = (transform.position - player.transform.position).normalized;
        float dotProduct = Vector3.Dot(player.transform.forward, directionToNPC);
        return dotProduct > 0.5f; // 0.5f 可調整，值越高表示視野範圍越窄
    }


    private void AssignCamera(GameObject source)
    {
        // 將玩家相機分配給對話框的Canvas組件
        talkCanvas.GetComponent<Canvas>().worldCamera = source.GetComponent<Camera>();
    }

    private void AssignConstraint(GameObject source)
    {
        // 將玩家相機分配給對話框的LookAtConstraint組件
        LookAtConstraint lookAtConstraint = talkCanvas.transform.GetChild(0).GetComponent<LookAtConstraint>();
        ConstraintSource firstConstraintSource = lookAtConstraint.GetSource(0);
        firstConstraintSource.sourceTransform = source.transform;
        lookAtConstraint.SetSource(0, firstConstraintSource);
    }
    public void NextConversation()
    {
        if (ConversationIndex < dialogGraph.Count - 1)
        {
            ConversationIndex++;
        }
    }

    public void ENDTALK()
    {
        talkAnimator.SetBool("Active", false);
    }
    public void ForceinsertTalk(DialogNodeGraph dialogNodeGraph)
    {
        this.gameObject.SetActive(true);
        ForceInsertDialog = dialogNodeGraph;
        MissionComplete = true;
    }
    public void clearForceNode()
    {
        ForceInsertDialog = null;
    }
    public void insertDialog(DialogNodeGraph dialogNodeGraph)
    {
        ForceInsertDialog = dialogNodeGraph;
    }
    public void Conversationindex(int index)
    {
        ConversationIndex = index;

    }



}
