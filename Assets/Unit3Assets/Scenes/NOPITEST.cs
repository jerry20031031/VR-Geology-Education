using System.IO;
using UnityEngine;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

public class NOPITEST : MonoBehaviour
{
    public void CreateEmptyExcel()
    {
        string path = Path.Combine(Application.persistentDataPath, "TestReport.xlsx");
        Debug.Log("📁 建立 Excel 中：" + path);

        try
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("空白工作表");

            // ✅ 必須至少建立一格資料，否則 Excel 會視為損壞
            sheet.CreateRow(0).CreateCell(0).SetCellValue("Hello NPOI");

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);
                fs.Flush(); // 保險
            }

            Debug.Log("✅ 空白 Excel 已建立：" + path);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("❌ 建立失敗：" + ex.ToString());
        }
    }
}
