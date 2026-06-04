using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckChildrenActive2 : MonoBehaviour
{
    void OnEnable()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                switch (child.name)
                {
                    case "Yes":
                        LogSystem.LogAction("學習者預測正確(旗子分布，海底擴張過程)");
                        Debug.Log("學習者預測旗子未來走向的結果正確");
                        break;
                    case "No":
                        LogSystem.LogAction("學習者預測錯誤(旗子分布，海底擴張過程)");
                        Debug.Log("學習者預測旗子未來走向的結果錯誤");
                        break;
                    case "No (1)":
                        LogSystem.LogAction("學習者預測錯誤(旗子分布，海底擴張過程)");
                        Debug.Log("學習者預測旗子未來走向的結果錯誤");
                        break;
                    case "No (2)":
                        LogSystem.LogAction("學習者預測錯誤(旗子分布，海底擴張過程)");
                        Debug.Log("學習者預測旗子未來走向的結果錯誤");
                        break;
                    default:
                        //LogSystem.LogAction("錯誤");
                        break;
                }

            }
        }
    }
}
