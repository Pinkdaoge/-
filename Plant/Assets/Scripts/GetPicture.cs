using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Baidu.Aip.ImageClassify;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class GetPicture : MonoBehaviour
{
    #region private
    //百度基础图片识别类
    private ImageClassify client;
    //Texture的中间部分，“桥”的作用
    private Texture baseTexture;
    #endregion
    #region public
    public Image test;
    public GameObject resultPanel;
    public GameObject loading;
    #endregion
    private void Start()
    {
        //监听上传照片Button
        UIManager.instance.enter_upload_click.onClick.AddListener(UploadPicture);
        var APP_ID = "16649689";
        var API_KEY = "K8zykFcQjW7nAVyjQqjV8rWE";
        var SECRET_KEY = "m3ZXwLhuplyHudrkinw4wkUhBxAoXQ5Z";
        client = new ImageClassify(API_KEY, SECRET_KEY);
        resultPanel.SetActive(false);
    }

    /// <summary>
    /// 获取照片
    /// </summary>
    private void UploadPicture()
    {
        loading.SetActive(true);
        StartCoroutine(Loading());
        //获取到动态图片的Texture
        baseTexture = UIManager.instance.camera_display.mainTexture;
        //转换贴图格式
        Texture2D completeTexture2D = DataConversion.TextureToTexture2D(baseTexture);
        //创建精灵
        Sprite competeSprite = Sprite.Create(completeTexture2D, new Rect(0, 0, completeTexture2D.width, completeTexture2D.height), new Vector2(0.5f, 0.5f));
        //应用精灵到指定图片
        UIManager.instance.picture_get.sprite = competeSprite;
        //打开截图(识别已完成）
        UIManager.instance.picture_get.gameObject.SetActive(true);
        //关闭视频窗口
        UIManager.instance.camera_display.gameObject.SetActive(false);
        //转换成.jpg文件
        byte[] jpgPicture = completeTexture2D.EncodeToJPG();
        //保存图片到电脑指定目录
        //SaveImage.SaveToFile(jpgPicture);
        //保存图片到手机相册
        SaveImage.SaveToFileAndroid(jpgPicture);
        //停止相机
        //GetComponent<CameraControl>().StopCamera();
        //PlantDetectDemo();
        AdvancedGeneralDemo();
    }
    /// <summary>
    /// 植物识别
    /// </summary>
    public void AdvancedGeneralDemo()
    {
        //var images = File.ReadAllBytes(@"C:\Users\Administrator\Desktop\pics\chuju.jpg");
        //var images = File.ReadAllBytes("/sdcard/DCIM/SaveImage/Pic.png");

        var images = File.ReadAllBytes("/sdcard/DCIM/SaveImage/balomn.png");
        // 调用通用物体识别，可能会抛出网络等异常，请使用try/catch捕获
        var result = client.AdvancedGeneral(images);
        // 如果有可选参数
        var options = new Dictionary<string, object>
        {
            {
                "baike_num", 5
            }
        };
        //Newwonsoft 解析数据        
        NewtonsoftRead(result.ToString());
    }

    private void NewtonsoftRead(string datajson)
    {
        JObject json = JObject.Parse(datajson);
        int length = int.Parse(json["result_num"].ToString());
        Debug.Log(length);
        JObject[] jb = new JObject[length];
        float[] id = new float[length];
        string[] name = new string[length];
        for (int i = 0; i < length; i++)
        {
            UIManager.instance.result_data_text.text += json["result"][i].ToString();
            jb[i] = JObject.Parse(json["result"][i].ToString());
            id[i] = (float)jb[i].GetValue("score");
            name[i] = jb[i].GetValue("keyword").ToString();
            UIManager.instance.score_sliders[i].value = id[i];
            UIManager.instance.name_texts[i].text = name[i];
            UIManager.instance.score_texts[i].text = id[i].ToString();
        }
        resultPanel.SetActive(true);
        
    }
    IEnumerator Loading()
    {
        yield return new WaitForSeconds(3f);
        loading.SetActive(false);
    }
}
