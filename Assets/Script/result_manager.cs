using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class result_manager : MonoBehaviour
{
    #region Public Variable
    public GameObject selected_data;
    public GameObject rune_info;
    public GameObject[] rune_info_datas;
    public Image rune_img_slot;
    public Image rune_img_pattern;
    public Image monster_profile;
    public Text monster_name;
    public Text rune_stat_name;
    public Text[] monster_stats_divide;
    public Text[] monster_plus_stats_divide;
    public Text[] monster_stats_combine;
    #endregion

    #region Local Variable
    List<Dictionary<string, int>> separate_stats;
    List<string> rune_type;
    List<string> even_rune_stat_type;
    List<string> prefer_stat_type;
    int cur_hp, cur_atk, cur_def, cur_spd, cur_crirate, cur_cridmg, cur_res, cur_acc;
    int plus_hp, plus_atk, plus_def, plus_spd, plus_crirate, plus_cridmg, plus_res, plus_acc;
    #endregion
    private void Start()
    {
        // set separate_stats
        Dictionary<string, int> separate_stat_1 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 9}, {"ATK", 9}, {"CRI RATE", 8}, {"CRI DMG", 8}, {"ACC", 8}, {"RES", 8}
        };
        Dictionary<string, int> separate_stat_2 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 9}, {"ATK", 9}, {"DEF", 9}, {"CRI RATE", 8}, {"CRI DMG", 8}, {"ACC", 8}, {"RES", 8}
        };
        Dictionary<string, int> separate_stat_3 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 9}, {"DEF", 9}, {"CRI RATE", 8}, {"CRI DMG", 8}, {"ACC", 8}, {"RES", 8}
        };
        Dictionary<string, int> separate_stat_4 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 9}, {"ATK", 9}, {"DEF", 9}, {"CRI RATE", 8}, {"CRI DMG", 8}, {"ACC", 8}, {"RES", 8}
        };
        Dictionary<string, int> separate_stat_5 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 9}, {"ATK", 9}, {"DEF", 9}, {"CRI RATE", 8}, {"CRI DMG", 8}, {"ACC", 8}, {"RES", 8}
        };
        Dictionary<string, int> separate_stat_6 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 9}, {"ATK", 9}, {"DEF", 9}, {"CRI RATE", 8}, {"CRI DMG", 8}, {"ACC", 8}, {"RES", 8}
        };

        Debug.Log(separate_stats.Count);

        separate_stats.Add(separate_stat_1);
        separate_stats.Add(separate_stat_2);
        separate_stats.Add(separate_stat_3);
        separate_stats.Add(separate_stat_4);
        separate_stats.Add(separate_stat_5);
        separate_stats.Add(separate_stat_6);

        Debug.Log(separate_stats.Count);
    }
    public void Start_StatSetting()
    {
        // get selected data
        rune_type = selected_data.GetComponent<select_data_control>().rune_type;
        even_rune_stat_type = selected_data.GetComponent<select_data_control>().even_rune_stat_type;
        prefer_stat_type = selected_data.GetComponent<select_data_control>().prefer_stat_type;

        // get basic monster stat data
        cur_hp = 11040;
        cur_atk = 801;
        cur_def = 604;
        cur_spd = 103;
        cur_crirate = 15;
        cur_cridmg = 50;
        cur_res = 15;
        cur_acc = 25;

        // cal monster stat
        Cal_Stat(rune_type, even_rune_stat_type, prefer_stat_type, cur_hp, cur_atk, cur_def, cur_spd);

        // set monster stat
        monster_stats_divide[0].text = cur_hp.ToString();
        monster_stats_divide[1].text = cur_atk.ToString();
        monster_stats_divide[2].text = cur_def.ToString();
        monster_stats_divide[3].text = cur_spd.ToString();

        monster_plus_stats_divide[0].text = plus_hp.ToString();
        monster_plus_stats_divide[1].text = plus_atk.ToString();
        monster_plus_stats_divide[2].text = plus_def.ToString();
        monster_plus_stats_divide[3].text = plus_spd.ToString();

        monster_stats_combine[0].text = (cur_crirate + plus_crirate).ToString() + "%";
        monster_stats_combine[1].text = (cur_cridmg + plus_cridmg).ToString() + "%";
        monster_stats_combine[2].text = (cur_res + plus_res).ToString() + "%";
        monster_stats_combine[3].text = (cur_acc + plus_acc).ToString() + "%";
    }
    void Cal_Stat(List<string> rune_type, List<string> even_rune_stat_type, List<string> prefer_stat_type, int hp, int atk, int def, int spd)
    {
        // repeat 6 times
        for(int rune_number = 1; rune_number < 7; rune_number++)
        {
            switch(rune_number)
            {
                case 1:
                    plus_atk += 160;

                    break;
                case 2:
                    CheckEvenRuneStat(rune_number);

                    break;
                case 3:
                    plus_def += 160;

                    break;
                case 4:
                    CheckEvenRuneStat(rune_number);

                    break;
                case 5:
                    plus_hp += 2448;

                    break;
                case 6:
                    CheckEvenRuneStat(rune_number);

                    break;
            }
        }

        // calculate plus stat from current stat

    }

    void CheckEvenRuneStat(int number)
    {
        if(number == 2)
        {
            if (even_rune_stat_type[0] == "SPD") plus_spd += 42;
            else if (even_rune_stat_type[0] == "HP") plus_hp += Mathf.RoundToInt((float)cur_hp * 0.63f);
            else if (even_rune_stat_type[0] == "ATK") plus_atk += Mathf.RoundToInt((float)cur_atk * 0.63f);
            else if (even_rune_stat_type[0] == "DEF") plus_def += Mathf.RoundToInt((float)cur_def * 0.63f);
        }
        else if(number == 4)
        {
            if (even_rune_stat_type[1] == "HP") plus_hp += Mathf.RoundToInt((float)cur_hp * 0.63f);
            else if (even_rune_stat_type[1] == "ATK") plus_atk += Mathf.RoundToInt((float)cur_atk * 0.63f);
            else if (even_rune_stat_type[1] == "DEF") plus_def += Mathf.RoundToInt((float)cur_def * 0.63f);
            else if (even_rune_stat_type[1] == "CRI RATE") plus_crirate += 58;
            else if (even_rune_stat_type[1] == "CRI DMG") plus_cridmg += 80;
        }
        else if(number == 6)
        {
            if (even_rune_stat_type[2] == "HP") plus_hp += Mathf.RoundToInt((float)cur_hp * 0.63f);
            else if (even_rune_stat_type[2] == "ATK") plus_atk += Mathf.RoundToInt((float)cur_atk * 0.63f);
            else if (even_rune_stat_type[2] == "DEF") plus_def += Mathf.RoundToInt((float)cur_def * 0.63f);
            else if (even_rune_stat_type[2] == "RES") plus_res += 64;
            else if (even_rune_stat_type[2] == "ACC") plus_acc += 64;
        }
    }

    void CalStatFromPreferStat(int number)
    {
        Dictionary<string, int> stat_scoreboard = separate_stats[number - 1];

        // check prefer stat and plus score in separte_stats

    }

    public void OnRuneClick(int i)
    {
        // get rune data from seleted data.
        even_rune_stat_type = selected_data.GetComponent<select_data_control>().even_rune_stat_type;

        // get rune img from rune box.
        switch(i)
        {
            case 0:
                rune_stat_name.text = "ATK + 160";
                break;
            case 1:
                if(even_rune_stat_type[0] == "SPD")
                {
                    rune_stat_name.text = even_rune_stat_type[0] + " + 42";
                }
                else
                {
                    rune_stat_name.text = even_rune_stat_type[0] + " + 63%";
                }
                break;
            case 2:
                rune_stat_name.text = "DEF + 160";
                break;
            case 3:
                if (even_rune_stat_type[1] == "CRI RATE")
                {
                    rune_stat_name.text = even_rune_stat_type[1] + " + 58%";
                }
                else if (even_rune_stat_type[1] == "CRI DMG")
                {
                    rune_stat_name.text = even_rune_stat_type[1] + " + 80%";
                }
                else
                {
                    rune_stat_name.text = even_rune_stat_type[1] + " + 63%";
                }
                break;
            case 4:
                rune_stat_name.text = "HP + 2448";
                break;
            case 5:
                if (even_rune_stat_type[2] == "RES")
                {
                    rune_stat_name.text = even_rune_stat_type[2] + " + 64%";
                }
                else if (even_rune_stat_type[1] == "ACC")
                {
                    rune_stat_name.text = even_rune_stat_type[2] + " + 64%";
                }
                else
                {
                    rune_stat_name.text = even_rune_stat_type[2] + " + 63%";
                }
                break;
        }
        // instantiate rune_img
        Transform temp = Instantiate(rune_info_datas[i].transform.Find($"rune ({i + 1})"), rune_img_slot.transform.position, rune_img_slot.transform.rotation);
        temp.transform.SetParent(rune_img_slot.transform);
        temp.localScale = rune_img_slot.transform.localScale;
        //rune_img_slot.sprite = rune_imgs[i].transform.Find($"rune ({i+1})").GetComponent<Image>().sprite;
        //rune_img_pattern.sprite = rune_imgs[i].transform.Find($"rune_Image_ ({i+1})").GetComponent<Image>().sprite;
        rune_info.SetActive(true); 
    }
    public void OnRuneClose()
    {
        rune_info.SetActive(false);

        // delete child
        Transform[] child = rune_img_slot.GetComponentsInChildren<Transform>();
        if(child != null)
        {
            for(int i=1; i<child.Length; i++)
            {
                if (child[i] != rune_img_slot.transform)
                    Destroy(child[i].gameObject);
            }
        }
    }
}
