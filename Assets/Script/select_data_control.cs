﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class select_data_control : MonoBehaviour
{
    #region Public Variable
    public GameObject[] rune_slots;
    public GameObject[] rune_previews;
    public GameObject[] rune_stats_prefer;
    public GameObject[] even_runes;
    public GameObject resultmanager;
    public GameObject btn_cal_start;
    public GameObject etc_bg;
    public GameObject rune_check_window;
    public GameObject loading_canvas;
    public GameObject result_ui;
    public GameObject selected_monster_bg;
    public GameObject selected_rune_bg;
    public GameObject selected_prefer_stat_bg;
    public GameObject pirate;
    public GameObject word_bubble;
    public List<int> rune_dropdown_values;
    public List<string> rune_type;
    public List<string> even_rune_stat_type;
    public List<string> prefer_stat_type;
    public Text selected_monster;
    public Text selected_rune_set;
    public Text selected_rune_set_stat;
    public Text selected_stat;
    public Text msg_error;
    #endregion

    #region Local Variable
    Animator pirate_anim;
    int rune_cnt = 6;
    int even_rune_stat_cnt = 3;
    int prefer_stat_cnt = 4;
    bool ispirateon = false;
    bool check_rune = false;
    bool check_even_rune_stat = false;
    bool check_prefer_stat = false;
    #endregion
    private void Awake()
    {
        pirate_anim = pirate.GetComponent<Animator>();
    }
    public void Cal_Start()
    {
        foreach (var obj in rune_slots)
        {
            if (obj.GetComponent<rune_slot_control>().dropdown_value != 0)
                rune_dropdown_values.Add(obj.GetComponent<rune_slot_control>().dropdown_value);
        }

        if (rune_dropdown_values.Count == rune_cnt)
            check_rune = true;

        foreach (var obj in rune_previews)
        {
            string cur_rune_set = "";

            if (obj.activeSelf)
            {
                cur_rune_set = obj.transform.Find("rune_preview_text").GetComponent<Text>().text;
                rune_type.Add(cur_rune_set);
            }
        }

        foreach (var obj in even_runes)
        {
            string cur_even_rune_stat = obj.GetComponent<rune_slot_control>().rune_stat_string;
            if (cur_even_rune_stat != "")
                even_rune_stat_type.Add(cur_even_rune_stat);
        }

        if (even_rune_stat_type.Count == even_rune_stat_cnt)
            check_even_rune_stat = true;

        foreach (var obj in rune_stats_prefer)
        {
            string cur_prefer_stat = obj.GetComponent<rune_stat_select_control>().stat_string;
            if (cur_prefer_stat != "")
                prefer_stat_type.Add(cur_prefer_stat);
        }

        if (prefer_stat_type.Count == prefer_stat_cnt)
            check_prefer_stat = true;

        // Debug.Log(rune_dropdown_values.Count + " " + even_rune_stat_type.Count + " " + prefer_stat_type.Count);

        selected_monster.text = "Water Ryu";
        selected_rune_set.text = string.Join("\n\n", rune_type);
        selected_rune_set_stat.text = string.Join("\n\n", even_rune_stat_type);
        selected_stat.text = string.Join(", ", prefer_stat_type);

        etc_bg.SetActive(true);
        rune_check_window.SetActive(true);
    }

    public void Cal_Reset()
    {
        rune_type.Clear();
        even_rune_stat_type.Clear();
        prefer_stat_type.Clear();

        btn_cal_start.GetComponent<Button>().interactable = true;
        selected_monster_bg.GetComponent<Image>().DOKill();
        selected_rune_bg.GetComponent<Image>().DOKill();
        selected_prefer_stat_bg.GetComponent<Image>().DOKill();
        msg_error.text = "";
        word_bubble.SetActive(false);
        pirate.SetActive(false);
        etc_bg.SetActive(false);
        rune_check_window.SetActive(false);
    }
    public void OnClickPirate()
    {
        if (ispirateon) return;

        ispirateon = true;
        pirate_anim.SetTrigger("IsMotion");
        word_bubble.SetActive(true);
        Invoke("WordBubbleClose", 2f);
    }
    void WordBubbleClose()
    {
        ispirateon = false;
        word_bubble.SetActive(false);
    }
    public void ResultWindowOpen()
    {
        if (check_rune && check_even_rune_stat && check_prefer_stat)
        {
            resultmanager.GetComponent<result_manager>().Start_StatSetting();
            loading_canvas.SetActive(true);
            StartCoroutine(OpenResultWindow());
        }
        else
        {
            btn_cal_start.GetComponent<Button>().interactable = false;
            btn_cal_start.transform.DOPunchPosition(new Vector3(10f, 0, 0), 0.75f, 50, 0f);
            StartCoroutine(ShakeEffect());
        }
    }
    IEnumerator OpenResultWindow()
    {
        yield return new WaitForSeconds(2f);
        loading_canvas.SetActive(false);
        result_ui.SetActive(true);
    }
    IEnumerator ShakeEffect()
    {
        yield return new WaitForSeconds(0.75f);

        pirate.SetActive(true);

        if (!check_rune || !check_even_rune_stat)
        {
            msg_error.text += "rune set part\n";
            selected_rune_bg.GetComponent<Text>()
                .DOFade(0.2f, 0.25f)
                .SetEase(Ease.OutSine)
                .SetLoops(4, LoopType.Yoyo);
        }
            
        if (!check_prefer_stat)
        {
            msg_error.text += "stat part";
            selected_prefer_stat_bg.GetComponent<Text>()
                .DOFade(0.2f, 0.25f)
                .SetEase(Ease.OutSine)
                .SetLoops(4, LoopType.Yoyo);
        }
    }
}
