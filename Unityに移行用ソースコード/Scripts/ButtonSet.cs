using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System;

public class ButtonSet : MonoBehaviour
{
    
    private static Sprite avatarimage;
    private static List<string> imageIndex;
    
    public string pythonScript;
    public static Button[] imageButton;
    private static int buttonIndex = 0;
    public static Image buttonImage;
   
    // Start is called before the first frame update
    void Start()
    {

       
        
        
        
        /* Thread newThread = new Thread(
                          new ParameterizedThreadStart(CallPythonHW)
                          );*/
        Thread newThread = new Thread(RunShellThreadStart);
        newThread.Start();
        
        //CallPythonAddHW(basePath + "AddHelloWorld.py", 3.6f, 5.9f);
    }
    private static void RunShellThreadStart()
    {
        string basePath = @"D:/projects/AvatarRecommendationSystem/Assets/Python/";
        
        CallPythonHW(basePath + "Button.py");
    }
    private static void CallPythonHW(object pyScriptPath)
    {
        UnityEngine.Debug.Log("CallPythonHW");
        CallPythonBase(pyScriptPath.ToString());
    }

    void CallPythonAddHW(string pyScriptPath, float a, float b)
    {
        CallPythonBase(pyScriptPath, a.ToString(), b.ToString());
    }

    /// <summary>
    /// Unity 调用 Python
    /// </summary>
    /// <param name="pyScriptPath">python 脚本路径</param>
    /// <param name="argvs">python 函数参数</param>
    private static void CallPythonBase(string pyScriptPath, params string[] argvs)
    {
        UnityEngine.Debug.Log("CallPythonBase");
        Process process = new Process();

        // ptython 的解释器位置 python.exe
        process.StartInfo.FileName = @"C:/Users/Administrator/AppData/Local/Programs/Python/Python310/python.exe";

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
    private static void GetData(object sender, DataReceivedEventArgs e)
    {
        //UnityEngine.Debug.Log(e.Data);
        // 结果不为空才打印（后期可以自己添加类型不同的处理委托）
        if (string.IsNullOrEmpty(e.Data) == false)
        {
            
            //avatarimage = Resources.Load<Sprite>(e.Data);
            //buttonImage.sprite = avatarimage;
            UnityEngine.Debug.Log("#########");
            
            UnityEngine.Debug.Log(e.Data);
            
           
            
            UnityEngine.Debug.Log("#########");

            SetButton(e.Data);
            //buttonImage.sprite = avatarimage;
        }
    }
    private static void SetButton(string imageID)
    {
        buttonImage = imageButton[buttonIndex].GetComponent<Image>();
        avatarimage = Resources.Load<Sprite>(imageID);
        UnityEngine.Debug.Log("OK");
        buttonImage.sprite = avatarimage;
        buttonIndex++;



    }
    private static void GetImage(string imageID)
    {
        imageIndex.Add(imageID);
        UnityEngine.Debug.Log(imageIndex);
    }
}
