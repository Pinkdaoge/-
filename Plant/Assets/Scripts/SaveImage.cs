using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveImage 
{
    /// <summary>
    /// 保存图片到本地
    /// 覆盖一张图片的方式
    /// </summary>
    /// <param name="data"></param>
    public static void SaveToFile(byte[] data)
    {
        FileStream fs = new FileStream(@"C:\Users\Administrator\Desktop\pics\pic.jpg", FileMode.Create, FileAccess.Write);
        fs.Write(data, 0, data.Length);
        fs.Flush();
        fs.Close();

    }
    /// <summary>
    /// 将图片保存到手机相册
    /// </summary>
    /// <param name="data"></param>
    public static void SaveToFileAndroid(byte[] data)
    {
        SaveImages(data);
    }

    /// <summary>
    /// 保存png图片
    /// </summary>
    /// <param name="targetByte"></param>
    public static void SaveImages(byte[] targetByte)
    {
        string path = Application.persistentDataPath;
#if UNITY_ANDROID
        path = "/sdcard/DCIM/SaveImage"; //设置图片保存到设备的目录.
#endif
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        string savePath = path + "/" + "balomn" + ".png";
        File.WriteAllBytes(savePath, targetByte);
        //savePngAndUpdate(savePath);       
    }

    //调用iOS或Android原生方法保存图片后更新相册.
    private void savePngAndUpdate(string fileName)
    {
#if UNITY_ANDROID
        GetAndroidJavaObject().Call("scanFile", fileName, "保存成功辣٩(๑>◡<๑)۶ "); //这里我们可以设置保存成功弹窗内容
#endif
    }

    //用于获取Android原生方法类对象
    private AndroidJavaObject GetAndroidJavaObject()
    {
        return new AndroidJavaObject("com.example.saveimagelibrary.SaveImageActivity"); //设置成我们aar库中的签名+类名
    }
}
