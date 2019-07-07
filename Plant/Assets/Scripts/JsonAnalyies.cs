using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonAnalyies
{

}

public class ResultItem
{
    /// <summary>
    /// 
    /// </summary>
    public double score { get; set; }
    /// <summary>
    /// 商品-农用物资
    /// </summary>
    public string root { get; set; }
    /// <summary>
    /// 花卉
    /// </summary>
    public string keyword { get; set; }
}

public class Root
{
    /// <summary>
    /// 
    /// </summary>
    public int log_id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int result_num { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<ResultItem> result { get; set; }
}

