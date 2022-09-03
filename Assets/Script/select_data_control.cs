using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class select_data_control : MonoBehaviour
{
    #region Public Variable
    public GameObject[] rune_slots;
    public GameObject[] rune_previews;
    public GameObject[] rune_stats_prefer;
    public GameObject[] even_runes;
    public GameObject etc_bg;
    public GameObject rune_check_window;
    public List<int> rune_dropdown_values;
    public List<string> rune_type;
    public List<string> even_rune_stat_type;
    public List<string> prefer_stat_type;
    public Text selected_monster;
    public Text selected_rune_set;
    public Text selected_rune_set_stat;
    public Text selected_stat;
    #endregion

    public void Cal_Start()
    {
        foreach(var obj in rune_slots)
        {
            rune_dropdown_values.Add(obj.GetComponent<rune_slot_control>().dropdown_value);
        }

        foreach(var obj in rune_previews)
        {
            string cur_rune_set = "";

            if (obj.activeSelf)
            {
                cur_rune_set = obj.transform.Find("rune_preview_text").GetComponent<Text>().text;
                rune_type.Add(cur_rune_set);
            }
        }

        foreach(var obj in even_runes)
        {
            string cur_even_rune_stat = obj.GetComponent<rune_slot_control>().rune_stat_string;
            even_rune_stat_type.Add(cur_even_rune_stat);

        }

        foreach(var obj in rune_stats_prefer)
        {
            string cur_prefer_stat = obj.GetComponent<rune_stat_select_control>().stat_string;
            prefer_stat_type.Add(cur_prefer_stat);
        }

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

        etc_bg.SetActive(false);
        rune_check_window.SetActive(false);
    }
}
