using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System;
using UnityEditor.SceneManagement;

public class GetImageIndexX : MonoBehaviour
{
    public static int[] imageIndex = new int[16];
    public UnityCallPython getImage;
    public ButtonDown[] getNewImage = new ButtonDown[16];
    public string imageID;
    public string imagTag;
    public Button avatarButton;
    public Image[] avatarImage = new Image[16];

    private string _path = "C:/projects/AvatarRecommendationSystem/Assets/Resources";
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(GetimageID());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator GetimageID()
    {
        yield return new WaitForSeconds(0.1f);
        imageID = getImage.result;
        string r = @"[0-9]+";
        Regex reg = new Regex(r, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(2));//2秒后超时
        MatchCollection mc = reg.Matches(imageID);//设定要查找的字符串
        int n = 0;
        foreach (Match m in mc)
        {

            string s = m.Groups[0].Value;
            
            if (n < 16)
            {
                imageIndex[n] = int.Parse(s);
                avatarImage[n].sprite = Resources.Load<Sprite>(s);
            }
            
            n++;
        }
        UnityEngine.Debug.Log(imageIndex);
        
        /*var reg = new Regex("[0-9]+", RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(2));
        var mc = reg.Matches(imageID);
        foreach (Match m in mc)
        {
            //将会返回123、456、789

            var val = m.Groups[0].Value;
            imageIndex.Add(val);
            UnityEngine.Debug.Log(imageIndex);
            //GetImage(val);
        }*/
    }
    public void Restart()
    {
        EditorSceneManager.LoadScene("VRScene");
    }
    public void ChangeButton()
    {
        int i = 0;
        for ( i = 0; i < 16; i++)
        {
            if(getNewImage[i].result!= String.Empty)
            {
            imageID = getNewImage[i].result;
            getNewImage[i].result = string.Empty;
            UnityEngine.Debug.Log(imageID);
            }
        }
        
        string r = @"[0-9]+";
        Regex reg = new Regex(r, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(2));//2秒后超时
        MatchCollection mc = reg.Matches(imageID);//设定要查找的字符串
        int n = 0;
        foreach (Match m in mc)
        {

            string s = m.Groups[0].Value;

            if (n < 16)
            {
                imageIndex[n] = int.Parse(s);
                avatarImage[n].sprite = Resources.Load<Sprite>(s);
            }

            n++;
        }
        
        
    }
    
}
