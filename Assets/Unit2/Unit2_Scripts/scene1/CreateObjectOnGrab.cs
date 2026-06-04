using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CreateObjectOnGrab : MonoBehaviour
{
    public GameObject objectToClone; // 用於指定需要複製的物件（A物件）
    private XRGrabInteractable grabInteractable; // 用於抓取的 XRGrabInteractable 組件

    void Start()
    {
        // 確保 objectToClone 被指定為 A 物件
        if (objectToClone == null)
        {
            Debug.LogError("Please assign the object to clone (A object) in the inspector.");
            return;
        }

        // 獲取 XRGrabInteractable 組件
        grabInteractable = GetComponent<XRGrabInteractable>();

        // 註冊抓取事件
        if (grabInteractable != null)
        {
            grabInteractable.onSelectEntered.AddListener(OnGrab);
        }
        else
        {
            Debug.LogError("XRGrabInteractable not found on the object.");
        }
    }

    // 當物件被抓取時觸發
    private void OnGrab(XRBaseInteractor interactor)
    {
        // 生成物件 A 的副本
        if (objectToClone != null)
        {
            // 創建一個新的 A 物件並設置其位置與旋轉
            GameObject newObject = Instantiate(objectToClone, transform.position, transform.rotation);

            // 同步材質
            SyncMaterials(objectToClone, newObject);
        }
    }

    // 遞歸同步所有子物件的材質
    private void SyncMaterials(GameObject original, GameObject clone)
    {
        Renderer originalRenderer = original.GetComponent<Renderer>();
        Renderer cloneRenderer = clone.GetComponent<Renderer>();

        // 如果該物件有 Renderer，複製材質
        if (originalRenderer != null && cloneRenderer != null)
        {
            cloneRenderer.materials = originalRenderer.materials;
        }

        // 遞歸處理所有子物件
        for (int i = 0; i < original.transform.childCount; i++)
        {
            Transform originalChild = original.transform.GetChild(i);
            Transform cloneChild = clone.transform.GetChild(i);

            if (originalChild != null && cloneChild != null)
            {
                SyncMaterials(originalChild.gameObject, cloneChild.gameObject);
            }
        }
    }

    // 記得在物件被釋放時取消事件訂閱
    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.onSelectEntered.RemoveListener(OnGrab);
        }
    }
}
