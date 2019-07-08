using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }

    
    public RawImage camera_display;
    public Image picture_get;
    public WebCamTexture tex;
    public Button enter_upload_click;
    public Button back_menu_click;
    public Button other_menu_click;
    public Button selet_picture_click;
    public Button scan_click;
    public Button package_click;
    public Button room_click;
    public Text result_data_text;
    public Text network_check_text;
    public Slider[] score_sliders;
    public Text[] score_texts;
    public Text[] name_texts;

    //For test 
}
