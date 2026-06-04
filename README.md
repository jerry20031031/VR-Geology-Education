# VR-Geology-Education

這是一個以地質教育為主題的 Unity VR 專案，內容包含地形、岩石、火山、板塊與互動任務等學習場景。專案以 VR 互動方式輔助學生理解地質概念。

## 專案資訊

- Unity 版本：`2021.3.4f1`
- 主要分支：`main`
- 專案類型：Unity VR 教育互動專案
- Git LFS：已啟用，用於追蹤大型模型、圖片、音訊與貼圖資產

## 主要內容

- Unit 1：地質與岩石相關互動學習
- Unit 2：地質環境與實驗室相關互動內容
- Unit 3：火山、地形、板塊與岩石模型相關內容
- VR 互動：任務流程、NPC 對話、物件抓取、場景切換與測驗系統

## 如何開啟

1. 安裝 Unity `2021.3.4f1`
2. 安裝 Git LFS
3. Clone 此 repository
4. 執行：

```powershell
git lfs pull
```

5. 使用 Unity Hub 開啟此專案資料夾

## GitHub 版本注意事項

為了讓 repository 能順利上傳到 GitHub，並避免 Git LFS 容量過大，部分大型素材包已排除在 Git 追蹤之外。這些檔案仍保留在原本開發電腦的本機資料夾中，但不會出現在 GitHub clone 版本。

目前排除的主要大型素材包含：

- `Assets/TerrainSampleAssets/`
- `Assets/TmpFont/`
- `Assets/Prefabs/unit1/SkySeries Freebie/`
- `Assets/Prefabs/unit1/room/`
- `Assets/Prefabs/unit1/prefeb_3/`
- `Assets/Prefabs/unit1/Stone textures pack/`
- `Assets/Prefabs/unit1/Sci-Fi UI/`
- `Assets/Unit2/Unit2_Other/sci-fi_lab/`
- `Assets/Unit2/eric/textures/`
- `Assets/Unit3Assets/Prefabs/Lab/`
- `Assets/Unit3Assets/Prefabs/gneiss/`
- `Assets/Unit3Assets/Prefabs/RockWork/`
- `Assets/Unit3Assets/Models/NPC/`

因此，GitHub 版本適合用於程式碼、專案結構與主要設定展示；若要完整重現本機 VR 場景，可能需要補回上述大型素材。

## 版本控制設定

此專案已設定 Unity 專用 `.gitignore`，不會上傳以下 Unity 自動產生資料夾：

- `Library/`
- `Temp/`
- `Logs/`
- `obj/`
- `UserSettings/`
- `.vs/`

大型二進位資產會透過 Git LFS 管理。
