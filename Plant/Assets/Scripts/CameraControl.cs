using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Baidu.Aip.ImageClassify;


public class CameraControl : MonoBehaviour
{
    public string deviceName;
    //识别植物的面板
    public GameObject scanPlantPanel;
    private ImageClassify client;

    private void Start()
    {
        //监听拍照Button
        UIManager.instance.scan_click.onClick.AddListener(StartPhotograph);
        //返回主界面并关掉相机
        UIManager.instance.back_menu_click.onClick.AddListener(BackMainPanel);
    }

    /// <summary>
    /// 切换到拍照场景
    /// 实现拍照
    /// </summary>
    private void StartPhotograph()
    {
        UIManager.instance.camera_display.gameObject.SetActive(true);
        StartCoroutine(OpenCamera());
    }
    /// <summary>
    /// 返回主界面并停止相机
    /// </summary>
    private void BackMainPanel()
    {
        // scanPlantPanel.SetActive(false);
        //停止相机
        UIManager.instance.tex.Stop();
        //关掉截图
        UIManager.instance.picture_get.gameObject.SetActive(false);
        //关掉结果面板，并清楚数据
        GetComponent<GetPicture>().resultPanel.SetActive(false);
    }
    /// <summary>  
    /// 捕获窗口位置  
    /// </summary>  
    public IEnumerator OpenCamera()
    {
        //等待获取手机相机权限
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        //检查APP是否获取到了手机相机权限
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            deviceName = devices[0].name;
            UIManager.instance.tex = new WebCamTexture(deviceName, 1080, 1500, 60);
            //打开相机
            StartCamera();
            //把相机画面显示在RawImage里
            UIManager.instance.camera_display.texture = UIManager.instance.tex;
            //currentImage.GetComponent<RawImage>().texture = tex;
        }
    }

    /// <summary>
    /// 停止相机
    /// </summary>
    public void StopCamera()
    {
        UIManager.instance.tex.Stop();
    }

    /// <summary>
    /// 开启相机
    /// </summary>
    public void StartCamera()
    {
        UIManager.instance.tex.Play();
    }
}
