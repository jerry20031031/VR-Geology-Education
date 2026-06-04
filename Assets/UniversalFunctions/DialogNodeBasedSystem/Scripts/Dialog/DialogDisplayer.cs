using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

namespace cherrydev
{
    public class DialogDisplayer : MonoBehaviour
    {
        private GameObject playerCanvas;

        private DialogBehaviour dialogBehaviour;

        [Header("NODE PANELS")]
        [SerializeField] private SentencePanel dialogSentensePanel;

        private AnswerPanel dialogAnswerPanel;


        private void OnEnable()
        {
            playerCanvas = GameObject.Find("Player Canvas");
            dialogAnswerPanel = playerCanvas.transform.GetChild(0).GetComponent<AnswerPanel>();

            dialogBehaviour = GetComponent<DialogBehaviour>();
            dialogBehaviour.AddListenerToDialogFinishedEvent(DisableDialogPanel);

            dialogBehaviour.OnAnswerButtonSetUp += SetUpAnswerButtonsClickEvent;

            dialogBehaviour.OnDialogTextCharWrote += dialogSentensePanel.IncreaseMaxVisibleCharacters;
            dialogBehaviour.OnDialogTextSkipped += dialogSentensePanel.ShowFullDialogText;

            dialogBehaviour.OnSentenceNodeActive += EnableDialogSentencePanel;
            dialogBehaviour.OnSentenceNodeActive += DisableDialogAnswerPanel;
            dialogBehaviour.OnSentenceNodeActive += dialogSentensePanel.ResetDialogText;
            dialogBehaviour.OnSentenceNodeActiveWithParameter += dialogSentensePanel.Setup;

            dialogBehaviour.OnAnswerNodeActive += EnableDialogAnswerPanel;
            // dialogBehaviour.OnAnswerNodeActive += DisableDialogSentencePanel;

            dialogBehaviour.OnAnswerNodeActiveWithParameter += dialogAnswerPanel.EnableCertainAmountOfButtons;
            dialogBehaviour.OnMaxAmountOfAnswerButtonsCalculated += dialogAnswerPanel.SetUpButtons;

            dialogBehaviour.OnAnswerNodeSetUp += SetUpAnswerDialogPanel;

            dialogBehaviour.OnAnswerButtonKeyPressed += HandleAnswerButtonKeyPress;
        }

        private void OnDisable()
        {
            dialogBehaviour.OnAnswerButtonSetUp -= SetUpAnswerButtonsClickEvent;

            dialogBehaviour.OnDialogTextCharWrote -= dialogSentensePanel.IncreaseMaxVisibleCharacters;
            dialogBehaviour.OnDialogTextSkipped -= dialogSentensePanel.ShowFullDialogText;

            dialogBehaviour.OnSentenceNodeActive -= EnableDialogSentencePanel;
            dialogBehaviour.OnSentenceNodeActive -= DisableDialogAnswerPanel;
            dialogBehaviour.OnSentenceNodeActive += dialogSentensePanel.ResetDialogText;
            dialogBehaviour.OnSentenceNodeActiveWithParameter -= dialogSentensePanel.Setup;

            dialogBehaviour.OnAnswerNodeActive -= EnableDialogAnswerPanel;
            // dialogBehaviour.OnAnswerNodeActive -= DisableDialogSentencePanel;

            dialogBehaviour.OnAnswerNodeActiveWithParameter -= dialogAnswerPanel.EnableCertainAmountOfButtons;
            dialogBehaviour.OnMaxAmountOfAnswerButtonsCalculated -= dialogAnswerPanel.SetUpButtons;

            dialogBehaviour.OnAnswerNodeSetUp -= SetUpAnswerDialogPanel;

            dialogBehaviour.OnAnswerButtonKeyPressed -= HandleAnswerButtonKeyPress;
        }

        void Update()
        {
            if (dialogAnswerPanel.gameObject.active)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        private void HandleAnswerButtonKeyPress(int index, AnswerNode answerNode)
        {
            if (index >= 0)// && index < dialogAnswerPanel.buttons.Count
            {
                dialogBehaviour.SetCurrentNodeAndHandleDialogGraph(answerNode.childSentenceNodes[index]);
            }
        }
        /// <summary>
        /// Disable dialog answer and sentence panel
        /// </summary>
        public void DisableDialogPanel()
        {
            DisableDialogAnswerPanel();
            DisableDialogSentencePanel();
        }

        /// <summary>
        /// Enable dialog answer panel
        /// </summary>
        public void EnableDialogAnswerPanel()
        {
            ActiveGameObject(dialogAnswerPanel.gameObject, true);
            dialogAnswerPanel.DisalbleAllButtons();
        }

        /// <summary>
        /// Disable dialog answer panel
        /// </summary>
        public void DisableDialogAnswerPanel()
        {
            ActiveGameObject(dialogAnswerPanel.gameObject, false);
        }

        /// <summary>
        /// Enable dialog sentence panel
        /// </summary>
        public void EnableDialogSentencePanel()
        {
            dialogSentensePanel.ResetDialogText();

            ActiveGameObject(dialogSentensePanel.gameObject, true);
        }

        /// <summary>
        /// Disable dialog sentence panel
        /// </summary>
        public void DisableDialogSentencePanel()
        {
            ActiveGameObject(dialogSentensePanel.gameObject, false);
        }

        /// <summary>
        /// Enable or disable game object depends on isActive bool flag
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="isActive"></param>
        public void ActiveGameObject(GameObject gameObject, bool isActive)
        {
            if (gameObject == null)
            {
                Debug.LogWarning("Game object is null");
                return;
            }

            gameObject.SetActive(isActive);
        }

        /// <summary>
        /// Setting up answer button onClick event
        /// </summary>
        /// <param name="index"></param>
        /// <param name="answerNode"></param>
        public void SetUpAnswerButtonsClickEvent(int index, AnswerNode answerNode)
        {
            // 這裡是你綁定的函數，用來處理點擊後的對話邏輯
            Debug.Log($"按鈕 {index} 綁定的函數: 切換至節點 {answerNode.childSentenceNodes[index].GetSentenceText()}節點外部函數{answerNode.childSentenceNodes[index].GetExternalFunctionName()}");

            dialogAnswerPanel.GetButtonByIndex(index).onClick.AddListener(() =>
            {
                if (dialogBehaviour)
                {
                    dialogBehaviour.SetCurrentNodeAndHandleDialogGraph(answerNode.childSentenceNodes[index]);
                }
            });
        }

        /// <summary>
        /// Setting up answer dialog panel
        /// </summary>
        /// <param name="index"></param>
        /// <param name="answerText"></param>
        public void SetUpAnswerDialogPanel(int index, string answerText)
        {
            dialogAnswerPanel.GetButtonTextByIndex(index).text = answerText;
            dialogAnswerPanel.GetButtonKeyByIndex(index).text = (index + 1).ToString();
        }

        private void AssignConstraint()
        {
            // 將玩家相機分配給對話框的LookAtConstraint組件
            LookAtConstraint lookAtConstraint = dialogAnswerPanel.GetComponent<LookAtConstraint>();
            ConstraintSource firstConstraintSource = lookAtConstraint.GetSource(0);
            firstConstraintSource.sourceTransform = Camera.main.transform;
            lookAtConstraint.SetSource(0, firstConstraintSource);
        }
    }
}