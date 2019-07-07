using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonAnalusis 
{
    
}
//识别
public class Location_result
{
    public string width { get; set; }
    public string top { get; set; }
    public string left { get; set; }
    public string height { get; set; }
}

public class Baike_info
{
    public string baike_url { get; set; }//百科网址
    public string image_url { get; set; }//图片网址
    public string description { get; set; }//百科内容
}

public class Result
{
    public string score { get; set; }//准确率
    public string year { get; set; }//汽车出厂日期
    public Baike_info baike_info { get; set; }
    public string name { get; set; }//名称
}

public class RootObject
{
    public string log_id { get; set; }
    public Location_result location_result { get; set; }
    public List<Result> result { get; set; }
    public string color_result { get; set; }
}