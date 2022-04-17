using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class ButtonDownX : MonoBehaviour
{
    public GetImageIndex getImageIndex;
    public string pythonScript;
    public string result;
    public static int avatarID;
    private string basePath;
    public static int buttonTimes = 0;
    public static string buttonName;

    // Start is called before the first frame update
    void Start()
    {
        basePath = @"C:\projects\AvatarRecommendationSystem\Assets\Python\";
        pythonScript = pythonScript + ".py";
        //CallPythonHW(basePath + pythonScript);
        
    }
    public void CallPythonResult()
    {
        SendID();
        if(buttonName != gameObject.name){
            buttonName = gameObject.name;
            
        }
        else
        {
            CallPythonAddHW(basePath + pythonScript, SendID());
            buttonTimes = 0;
        }
        
        
        
    }
    void CallPythonHW(string pyScriptPath)
    {
        CallPythonBase(pyScriptPath);
    }

    void CallPythonAddHW(string pyScriptPath, float a)
    {
        CallPythonBase(pyScriptPath, a.ToString());
    }

    /// <summary>
    /// Unity 调用 Python
    /// </summary>
    /// <param name="pyScriptPath">python 脚本路径</param>
    /// <param name="argvs">python 函数参数</param>
    public void CallPythonBase(string pyScriptPath, params string[] argvs)
    {
        Process process = new Process();

        // ptython 的解释器位置 python.exe
        process.StartInfo.FileName = @"C:\Users\hanza\AppData\Local\Programs\Python\Python36\python.exe";

        // 判断是否有参数（也可不用添加这个判断）
        if (argvs != null)
        {
            // 添加参数 （组合成：python xxx/xxx/xxx/test.python param1 param2）
            foreach (string item in argvs)
            {
                pyScriptPath += " " + item;
            }
        }
        UnityEngine.Debug.Log(pyScriptPath);

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.Arguments = pyScriptPath;     // 路径+参数
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.CreateNoWindow = true;        // 不显示执行窗口

        // 开始执行，获取执行输出，添加结果输出委托
        process.Start();
        process.BeginOutputReadLine();
        process.OutputDataReceived += new DataReceivedEventHandler(GetData);
        process.WaitForExit();
    }

    /// <summary>
    /// 输出结果委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void GetData(object sender, DataReceivedEventArgs e)
    {

        // 结果不为空才打印（后期可以自己添加类型不同的处理委托）
        if (string.IsNullOrEmpty(e.Data) == false)
        {
            result = e.Data;
            UnityEngine.Debug.Log(e.Data);
        }
    }
    public int SendID()
    {
        
        avatarID = getImageIndex.imageIndex[int.Parse(gameObject.name)];
        return avatarID;
    }
}


// Update is called once per frame

    

