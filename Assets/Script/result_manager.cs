using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    Dictionary<string, int> stat_rainforce_value = new Dictionary<string, int>()
    {
        {"SPD", 6}, {"HP", 8}, {"ATK", 8}, {"DEF", 8}, {"CRI RATE", 6}, {"CRI DMG", 7}, {"ACC", 8}, {"RES", 8}
    };
    List<Dictionary<string, int>> separate_stats = new List<Dictionary<string, int>>();
    List<Dictionary<string, int>> rune_stat_infos = new List<Dictionary<string, int>>();
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
            {"SPD", 10}, {"HP", 10}, {"ATK", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}
        };
        Dictionary<string, int> separate_stat_2 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"ATK", 10}, {"DEF", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}
        };
        Dictionary<string, int> separate_stat_3 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"DEF", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}
        };
        Dictionary<string, int> separate_stat_4 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"ATK", 10}, {"DEF", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}
        };
        Dictionary<string, int> separate_stat_5 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"ATK", 10}, {"DEF", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}
        };
        Dictionary<string, int> separate_stat_6 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"ATK", 10}, {"DEF", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}
        };
        separate_stats.Add(separate_stat_1);
        separate_stats.Add(separate_stat_2);
        separate_stats.Add(separate_stat_3);
        separate_stats.Add(separate_stat_4);
        separate_stats.Add(separate_stat_5);
        separate_stats.Add(separate_stat_6);
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

        monster_stats_combine[0].text = (Mathf.Min(100, cur_crirate + plus_crirate)).ToString() + "%";
        monster_stats_combine[1].text = (cur_cridmg + plus_cridmg).ToString() + "%";
        monster_stats_combine[2].text = (Mathf.Min(100, cur_res + plus_res)).ToString() + "%";
        monster_stats_combine[3].text = (Mathf.Min(100, cur_acc + plus_acc)).ToString() + "%";
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
                    CalStatFromPreferStat(rune_number);
                    break;
                case 2:
                    CheckEvenRuneStat(rune_number);
                    CalStatFromPreferStat(rune_number);
                    break;
                case 3:
                    plus_def += 160;
                    CalStatFromPreferStat(rune_number);
                    break;
                case 4:
                    CheckEvenRuneStat(rune_number);
                    CalStatFromPreferStat(rune_number);
                    break;
                case 5:
                    plus_hp += 2448;
                    CalStatFromPreferStat(rune_number);
                    break;
                case 6:
                    CheckEvenRuneStat(rune_number);
                    CalStatFromPreferStat(rune_number);
                    break;
            }
        }

        //for (int i = 0; i < rune_stat_infos.Count; i++)
        //{
        //    Debug.Log($"rune number {i + 1} option -> ");
        //    foreach (var dict in rune_stat_infos[i])
        //    {
        //        Debug.Log(dict.Key + " : " + dict.Value);
        //    }
        //}
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
        #region set scoreboard
        Dictionary<string, int> stat_scoreboard = separate_stats[number - 1];
        // get rune data from seleted data
        even_rune_stat_type = selected_data.GetComponent<select_data_control>().even_rune_stat_type;
        // check prefer stat and plus score in separte_stats
        prefer_stat_type = selected_data.GetComponent<select_data_control>().prefer_stat_type;
        if (stat_scoreboard.ContainsKey(prefer_stat_type[0])) stat_scoreboard[prefer_stat_type[0]] += 3;
        if (stat_scoreboard.ContainsKey(prefer_stat_type[1])) stat_scoreboard[prefer_stat_type[1]] += 2;
        if (stat_scoreboard.ContainsKey(prefer_stat_type[2])) stat_scoreboard[prefer_stat_type[2]] += 1;

        // sort stat_scoreboard by value
        stat_scoreboard.OrderByDescending(item => item.Key).ToDictionary(x => x.Key, x => x.Value);
        #endregion

        #region mim_stat_demand
        List<int> min_demand_crirate = new List<int>() { 100, 85, 70 };
        List<int> min_demand_acc = new List<int>() { 85, 70, 55 };
        List<int> min_demand_res = new List<int>() { 100, 100, 100 };
        int min_crirate = 0;
        int min_acc = 0;
        int min_res = 0;
        bool prefer_option_on = false;
        bool prefer_crirate = false;
        bool prefer_acc = false;
        bool prefer_res = false;
        #endregion

        // check prefer stat and set minimum stat
        for (int i = 0; i < prefer_stat_type.Count; i++)
        {
            switch(prefer_stat_type[i])
            {
                case "CRI RATE":
                    if (i == 0) min_crirate = 100;
                    else if (i == 1) min_crirate = 85;
                    else if (i == 2) min_crirate = 70;
                    prefer_crirate = true;
                    break;
                case "ACC":
                    if (i == 0) min_acc = 85;
                    else if (i == 1) min_acc = 70;
                    else if (i == 2) min_acc = 55;
                    prefer_acc = true;
                    break;
                case "RES":
                    min_res = 100;
                    prefer_res = true;
                    break;
            }
        }

        // temp dict for save rune info
        Dictionary<string, int> temp_rune_info = new Dictionary<string, int>();

        // set pre-option
        if (number != 2 && prefer_acc)
        {
            if (!temp_rune_info.ContainsKey("ACC"))
            {
                prefer_option_on = true;
                int pre_option_value = CalRainforceValue(stat_rainforce_value["ACC"]);
                temp_rune_info.Add("ACC", pre_option_value);
            }
        }
        else if (number != 2 && prefer_res)
        {
            if (!temp_rune_info.ContainsKey("RES"))
            {
                prefer_option_on = true;
                int pre_option_value = CalRainforceValue(stat_rainforce_value["RES"]);
                temp_rune_info.Add("RES", pre_option_value);
            }
        }

        // if rune number is odd number, just add stat to rune.
        if (number % 2 == 1)
        {
            // set prefer basic stat to rune
            for (int i = 0; i < 3; i++)
            {
                // check prefer stat and plus score in separte_stats
                if (!stat_scoreboard.ContainsKey(prefer_stat_type[i]) || (!temp_rune_info.ContainsKey(prefer_stat_type[i])))
                    continue;

                int rainforce_value = CalRainforceValue(stat_rainforce_value[prefer_stat_type[i]]);
                temp_rune_info.Add(prefer_stat_type[i], rainforce_value);
            }

            // set prefer extra basic stat to rune
            foreach (string key in stat_scoreboard.Keys)
            {
                if (!temp_rune_info.ContainsKey(key))
                {
                    int rainforce_value = CalRainforceValue(stat_rainforce_value[key]);
                    temp_rune_info.Add(key, rainforce_value);
                }

                if (temp_rune_info.Count == 4)
                    break;
            }

            // rune rainforce
            for (int i = 0; i < 4; i++)
            {
                string rainforce_stat = CalRainforceStatNumber(temp_rune_info, prefer_option_on);
                int rainforce_value = CalRainforceValue(stat_rainforce_value[rainforce_stat]);

                temp_rune_info[rainforce_stat] += rainforce_value;
            }
        }
        // if rune number is even number, check even number main stat before add stat to rune.
        else
        {
            // get even rune stat
            string even_stat_type = "";
            if (number == 2) even_stat_type = even_rune_stat_type[0];
            else if (number == 4) even_stat_type = even_rune_stat_type[1];
            else if (number == 6) even_stat_type = even_rune_stat_type[2];

            // set prefer basic stat to rune
            for (int i = 0; i < 3; i++)
            {
                // check stat between even rune stat and prefer stat
                if (prefer_stat_type[i] == even_stat_type)
                    continue;

                // check prefer stat and plus score in separte_stats
                if (!stat_scoreboard.ContainsKey(prefer_stat_type[i]) || (!temp_rune_info.ContainsKey(prefer_stat_type[i])))
                    continue;

                int rainforce_value = CalRainforceValue(stat_rainforce_value[prefer_stat_type[i]]);
                temp_rune_info.Add(prefer_stat_type[i], rainforce_value);
            }

            // set prefer extra basic stat to rune
            foreach (string key in stat_scoreboard.Keys)
            {
                if (!temp_rune_info.ContainsKey(key) && key != even_stat_type)
                {
                    int rainforce_value = CalRainforceValue(stat_rainforce_value[key]);
                    temp_rune_info.Add(key, rainforce_value);
                }

                if (temp_rune_info.Count == 4)
                    break;
            }

            // rune rainforce
            for (int i = 0; i < 4; i++)
            {
                string rainforce_stat = "";
                if(number == 2)
                {
                    if (prefer_crirate && temp_rune_info.ContainsKey("CRI RATE"))
                    {
                        if (cur_crirate + plus_crirate < min_crirate)
                            rainforce_stat = "CRI RATE";
                    }
                    else if (prefer_acc && temp_rune_info.ContainsKey("ACC"))
                    {
                        if (cur_acc + plus_acc < min_acc)
                            rainforce_stat = "ACC";
                    }
                    else if (prefer_res && temp_rune_info.ContainsKey("RES"))
                    {
                        if (cur_acc + plus_acc < min_res)
                            rainforce_stat = "RES";
                    }
                }
                else rainforce_stat = CalRainforceStatNumber(temp_rune_info, prefer_option_on);

                int rainforce_value = CalRainforceValue(stat_rainforce_value[rainforce_stat]);

                temp_rune_info[rainforce_stat] += rainforce_value;
            }
        }

        // conversion rune stat
        string converstion_stat = CalConverstionStatFromRune(temp_rune_info);
        int converstion_stat_value = CalConversionStatValue(converstion_stat);
        temp_rune_info[converstion_stat] = converstion_stat_value;

        // grinding rune stat
        for (int i = 0; i < temp_rune_info.Count; i++)
        {
            if(temp_rune_info.Keys.ToList()[i] == "HP" || temp_rune_info.Keys.ToList()[i] == "ATK" || temp_rune_info.Keys.ToList()[i] == "DEF")
            {
                temp_rune_info[temp_rune_info.Keys.ToList()[i]] += 10;
            }
            else if(temp_rune_info.Keys.ToList()[i] == "SPD")
            {
                temp_rune_info[temp_rune_info.Keys.ToList()[i]] += 5;
            }
        }

        Debug.Log(number);
        foreach (var dict in temp_rune_info)
        {
            Debug.Log(dict.Key + " : " + dict.Value);
        }

        // calculate plus stat from current stat
        foreach (var dict in temp_rune_info)
        {
            if (dict.Key == "SPD") plus_spd += dict.Value;
            else if (dict.Key == "HP") plus_hp += Mathf.RoundToInt((float)cur_hp * (dict.Value / 100f));
            else if (dict.Key == "ATK") plus_atk += Mathf.RoundToInt((float)cur_atk * (dict.Value / 100f));
            else if (dict.Key == "DEF") plus_def += Mathf.RoundToInt((float)cur_def * (dict.Value / 100f));
            else if (dict.Key == "CRI RATE") plus_crirate += dict.Value;
            else if (dict.Key == "CRI DMG") plus_cridmg += dict.Value;
            else if (dict.Key == "RES") plus_res += dict.Value;
            else if (dict.Key == "ACC") plus_acc += dict.Value;
        }

        // add stat to rune_stat_infos
        rune_stat_infos.Add(temp_rune_info);
    }
    int CalRainforceValue(int rainforce_value)
    {
        int percentage = Random.Range(1, 100);
        if (percentage > 0 && percentage <= 5) return rainforce_value -= 2;
        else if (percentage > 5 && percentage <= 30) return rainforce_value -= 1;
        else return rainforce_value;
    }
    string CalRainforceStatNumber(Dictionary<string, int> rainforce_stat_dict, bool prefer_option_check)
    {
        List<string> temp = new List<string>(rainforce_stat_dict.Keys);

        int percentage = Random.Range(1, 100);

        if(prefer_option_check)
        {
            if (percentage > 0 && percentage <= 5) return temp[4];
            else if (percentage > 5 && percentage <= 15) return temp[3];
            else if (percentage > 15 && percentage <= 30) return temp[2];
            else return temp[1];
        }
        else
        {
            if (percentage > 0 && percentage <= 5) return temp[3];
            else if (percentage > 5 && percentage <= 15) return temp[2];
            else if (percentage > 15 && percentage <= 30) return temp[1];
            else return temp[0];
        }
    }
    string CalConverstionStatFromRune(Dictionary<string, int> converstion_stat_dict)
    {
        string check_stat = "";
        int check_max_value = -1;

        foreach(var dict in converstion_stat_dict)
        {
            // excluding stat that is inefficient to use grinding stone.
            if (dict.Key == "CRI RATE" || dict.Key == "CRI DMG" || dict.Key == "SPD")
                continue;

            // excluding stat that do not require conversion
            if (dict.Value > stat_rainforce_value[dict.Key])
                continue;

            // get difference from check value
            int check_value = stat_rainforce_value[dict.Key] - dict.Value;
            if(check_value > check_max_value)
            {
                check_stat = dict.Key;
                check_max_value = check_value;
            }
            else if(check_value == check_max_value)
            {
                if(prefer_stat_type.Contains(dict.Key) && prefer_stat_type.Contains(check_stat))
                {
                    // check stat priority from prefer stat list
                    int difference = prefer_stat_type.IndexOf(dict.Key) - prefer_stat_type.IndexOf(check_stat);
                    if(difference < 0)
                    {
                        check_stat = dict.Key;
                        check_max_value = check_value;
                    }
                }
                else if(prefer_stat_type.Contains(dict.Key) && !prefer_stat_type.Contains(check_stat))
                {
                    check_stat = dict.Key;
                    check_max_value = check_value;
                }
            }
        }
        return check_stat;
    }
    int CalConversionStatValue(string conversion_stat)
    {
        int conversion_value = 0;
        if (conversion_stat == "HP" || conversion_stat == "ATK" || conversion_stat == "DEF")
            conversion_value = 13;
        else if (conversion_stat == "RES" || conversion_stat == "ACC")
            conversion_value = 11;
        else if (conversion_stat == "SPD")
            conversion_value = 10;

        return conversion_value;
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
