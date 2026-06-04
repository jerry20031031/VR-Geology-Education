using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button driftButton; // �Ʋ��@�����s
    public Button confirmButton; // �ڿ�ܦn�����s
    public GameObject errorPanel; // ���~���O
    public Button showAnswerButton; // �����ݵ��׫��s
    public Button retryButton; // �ڦA��ܤ@�����s
    public TextMeshProUGUI feedbackText; // ��ܿ��~����r
    public GameObject img;
    [Header("correct")]
    public bool alwaysCorrect = false; // �p�G�b Inspector ���Ŀ�A���ܵ��שl�ץ��T


    void Awake()
    {
        // �T�O���s���� null�A�M��K�[��ť��
        if (driftButton != null)
            driftButton.onClick.AddListener(() => OnButtonClicked(OnDriftButtonClicked));

        if (confirmButton != null)
            confirmButton.onClick.AddListener(() => OnButtonClicked(OnConfirmButtonClicked));

        if (showAnswerButton != null)
            showAnswerButton.onClick.AddListener(() => OnButtonClicked(OnShowAnswerButtonClicked));

        if (retryButton != null)
            retryButton.onClick.AddListener(() => OnButtonClicked(OnRetryButtonClicked));

        // ���ÿ��~���O
        if (errorPanel != null)
            errorPanel.SetActive(false);
    }
    void Start()
    {
        LogSystem.instance.TotalKnowledgePoints = 2;
    }

    // �q�Ϊ����s�I���B�z
    void OnButtonClicked(System.Action callback)
    {
        callback?.Invoke();
    }

    void OnDriftButtonClicked()
    {
        Debug.Log("�Ʋ��@���I");
        // �o�̥i�H�[�J�H���վ�ﶵ���޿�
    }

    void OnConfirmButtonClicked()
    {
        bool isCorrect = alwaysCorrect; // ���]�ˬd�޿赲�G

        if (!isCorrect)
        {
            ShowError();
        }
        else
        {
            ShowError();
            // �[�J���T�޿�
        }
    }

    void ShowError()
    {
        if (img != null)
            img.SetActive(true);

        if (feedbackText != null)
            feedbackText.gameObject.SetActive(true);

        if (errorPanel != null)
            errorPanel.SetActive(true);
    }

    void OnShowAnswerButtonClicked()
    {
        Debug.Log("������ܵ��סI");
        // ��ܵ����޿�
    }

    void OnRetryButtonClicked()
    {
        Debug.Log("�A��ܤ@���I");
        if (errorPanel != null)
            errorPanel.SetActive(false);
        // ���m�ﶵ�޿�
    }
}
