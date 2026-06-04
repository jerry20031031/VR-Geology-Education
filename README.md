# Unit 1：大陸漂移與海底擴張 VR 教材

本專案為地球科學虛擬實境教材的第一單元，主題是「大陸漂移與海底擴張」。教材透過 VR 場景、角色對話、模型觀察、拼圖操作、選擇題與任務回饋，帶領學生理解韋格納大陸漂移說、盤古大陸、大陸漂移證據，以及海底擴張的形成過程。

本單元主要負責內容為：

- `Scene1.2`：大陸漂移與盤古大陸。
- `Scene3.4`：海底擴張與相關地質證據。
- `Scene5`：單元問答檢核與完成流程。

## 教學目標

學生完成本單元後，應能：

1. 說明韋格納提出大陸漂移說的核心概念。
2. 透過盤古大陸拼圖理解各大陸曾經相連的可能性。
3. 認識支持大陸漂移說的主要證據，例如化石分布、海岸線相似與地層構造連續。
4. 理解海底擴張中，岩漿沿中洋脊上升、冷卻並形成新海洋地殼的過程。
5. 透過海洋地殼年齡、沉積物厚度與地磁反轉紀錄，判斷海底擴張的證據。
6. 透過單元測驗檢核大陸漂移與海底擴張的學習成果。

## 場景總覽

| 場景 | 檔案位置 | 教學重點 | 主要功能 |
| --- | --- | --- | --- |
| Scene1.2 | `Assets/Scenes/unit1/Scene1.2.unity` | 大陸漂移、盤古大陸、韋格納證據 | 大陸拼圖、漂移路徑選擇、證據解鎖 |
| Scene3.4 | `Assets/Scenes/unit1/Scene3.4.unity` | 海底擴張、中洋脊、海洋地殼、地磁證據 | 海底模型探查、岩漿上升、岩石冷卻、資料板配對 |
| Scene5 | `Assets/Scenes/unit1/Scene5.unity` | 單元總結與知識檢核 | `QuestionSystemUnit1` 問答系統、答題回饋、紀錄匯出 |

## Scene1.2：大陸漂移與盤古大陸

`Scene1.2` 是本單元前半段的主要場景，重點在於讓學生透過操作大陸模型，理解「現今各大陸可能曾經相連」的概念。

### 學習內容

- 認識韋格納與大陸漂移說。
- 觀察非洲、南美洲、印度、澳洲、南極洲、亞洲等大陸模型。
- 透過大陸拼圖任務建立盤古大陸的空間概念。
- 判斷大陸漂移的可能路徑。
- 解鎖大陸漂移的證據知識點：
  - 化石分布。
  - 海岸線相似。
  - 地層與山脈構造連續。

### 互動設計

- 學生可拖曳大陸物件並放入對應位置。
- 完成拼圖後，系統會記錄任務完成時間。
- 選擇錯誤漂移路徑時，系統會記錄互動失敗次數。
- 使用提示時，系統會記錄提示次數。
- 與角色對話後可獲得額外知識點。

### 主要腳本

- `Assets/Scripts/unit1/unit1TaskManager.cs`
- `Assets/Scripts/unit1/checkTwoSocket.cs`
- `Assets/Scripts/unit1/checkFourSocket.cs`
- `Assets/Scripts/unit1/scene2scripts/check1.cs`
- `Assets/Scripts/unit1/Scripts-continent`

## Scene3.4：海底擴張與地質證據

`Scene3.4` 是本單元後半段的主要場景，將學習重點從大陸漂移延伸到海底擴張。學生透過海底模型、岩漿活動與資料判讀，理解海洋地殼如何生成與向兩側擴張。

### 學習內容

- 認識中洋脊與海底擴張。
- 理解岩漿沿中洋脊上升並冷卻形成新海洋地殼。
- 觀察海洋地殼年齡與沉積物厚度的分布關係。
- 理解岩石或礦物冷卻後會記錄當時地球磁場。
- 認識地磁反轉與海底擴張證據之間的關聯。

### 互動設計

- 操作海底探查模型，觀察海底構造。
- 透過按鈕或目標點啟動板塊分離與岩漿上升動畫。
- 放置岩石或晶體，觀察冷卻後的變化。
- 完成資料板配對，判斷海底擴張相關證據。
- 針對沉積物厚度、海洋地殼年齡與擴張方向進行預測。

### 主要腳本

- `Assets/Scripts/unit1/scence3/Startcontrol.cs`
- `Assets/Scripts/unit1/scence3/boatControl.cs`
- `Assets/Scripts/unit1/scence3/checkDataBoard.cs`
- `Assets/Scripts/unit1/scence3/CheckChildrenActive.cs`
- `Assets/Scripts/unit1/scence3/CheckChildrenActive2.cs`
- `Assets/Scripts/unit1/scence4/rockcount.cs`
- `Assets/Scripts/unit1/scence4/color2.cs`

## Scene5：單元總結與問答檢核

`Scene5` 是第一單元的收束場景，主要用來檢核學生是否理解大陸漂移與海底擴張的核心概念。此場景的重要腳本是 `QuestionSystemUnit1`。

### 核心腳本

- `Assets/Prefabs/Public/Question/QuestionSystemUnit1.cs`

`QuestionSystemUnit1` 負責管理第一單元的問答流程，包含：

- 初始化五題單元測驗。
- 顯示題目文字與答案選項。
- 綁定答案按鈕事件。
- 判斷學生選擇是否正確。
- 以綠色顯示答對、紅色顯示答錯。
- 顯示答題回饋文字、補充說明與圖片。
- 禁用已作答題目的按鈕，避免重複作答。
- 切換到下一題。
- 完成所有題目後關閉問答介面並顯示完成物件。
- 呼叫 `LogSystem` 紀錄並匯出學生作答資料。

### 測驗題目重點

Scene5 的測驗題目包含：

1. 哪位科學家提出盤古大陸？
2. 韋格納提出大陸漂移說時，使用哪些主要證據？
3. 海底擴張中，岩漿沿海底哪個位置上升？
4. 海洋地殼向兩旁擴張時，會記錄地球磁場的什麼變化？
5. 中洋脊岩漿冷卻後會形成新的什麼地質？

### 其他相關腳本

- `Assets/Scripts/unit1/unit1TaskManager11.cs`
- `Assets/Scripts/unit1/scene2scripts/changeScene.cs`
- `Assets/Scripts/unit1/LogSystem.cs`

## 學習歷程紀錄

本單元透過 `LogSystem.cs` 記錄學生的學習過程。紀錄內容包含：

- 進入大陸漂移與海底擴張單元。
- 各任務完成時間。
- 知識點解鎖數量。
- 互動失敗次數。
- 提示使用次數。
- 問答題目、學生選項與答題正確性。
- 完成後匯出學習紀錄。

主要任務包含：

1. 任務一：完成盤古大陸拼圖。
2. 任務二：選擇大陸漂移路徑。
3. 任務三：探查海底模型。
4. 任務四：判斷海底擴張相關證據。
5. 任務五：完成海底磁場或晶體方向分布活動。

## 專案結構

```text
Assets/
  Scenes/
    unit1/
      Scene1.2.unity
      Scene3.4.unity
      Scene5.unity
      intro.unity
      bigHall 1.unity

  Scripts/
    unit1/
      LogSystem.cs
      unit1TaskManager.cs
      unit1TaskManager11.cs
      scene2scripts/
      scence3/
      scence4/
      Scripts-continent/

  Prefabs/
    Public/
      Question/
        QuestionSystemUnit1.cs
        QuestionCanvas.prefab

    unit1/
      ContinentPrefab/
      fossil/
      lava/
      map/
      teacher/
      personSound/
      unit1else/
```

## 開發環境

- Unity：2021.3.4f1
- Universal Render Pipeline：12.1.7
- XR Interaction Toolkit：2.5.4
- Unity OpenXR：1.4.2
- VIVE OpenXR：2.2.0
- Wave XR SDK：5.6.0-r.10.2
- TextMeshPro：3.0.6

## 執行方式

1. 使用 Unity 2021.3.4f1 開啟專案根目錄。
2. 確認 `Packages/manifest.json` 內的套件皆已正常載入。
3. 建議從 `Assets/Scenes/unit1/intro.unity` 或 `Assets/Scenes/unit1/bigHall 1.unity` 進入單元。
4. 若要直接測試第一單元核心場景，可開啟：
   - `Assets/Scenes/unit1/Scene1.2.unity`
   - `Assets/Scenes/unit1/Scene3.4.unity`
   - `Assets/Scenes/unit1/Scene5.unity`

## 負責範圍

本 README 對應第一單元「大陸漂移與海底擴張」，重點整理如下：

- `Scene1.2`：大陸漂移、盤古大陸與韋格納證據。
- `Scene3.4`：海底擴張、中洋脊、海洋地殼與地磁證據。
- `Scene5`：以 `QuestionSystemUnit1` 為核心的單元問答檢核與完成流程。
