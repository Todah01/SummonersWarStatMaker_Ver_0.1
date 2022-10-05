using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class monster_dropdown_control : MonoBehaviour
{
    public List<string> op_title = new List<string>();
    public Dropdown dropdown;
    public int monster_value = 0;
    public string monster_name_by_value = "";

    private void Awake()
    {
        List<Dictionary<string, object>> monster_names = CSVReader.Read("monster_name_data");
        for (int i = 0; i < monster_names.Count; i++)
        {
            op_title.Add(monster_names[i]["Name"].ToString());
        }
    }
    public void Start()
    {
        StatCheckFunction_UI(0);
    }
    private void Update()
    {
        dropdown.onValueChanged.AddListener(delegate
        {
            Function_Dropdown(dropdown);
        });
    }
    private void Function_Dropdown(Dropdown select)
    {
        StatCheckFunction_UI(dropdown.value);
        monster_value = dropdown.value;
        monster_name_by_value = select.options[select.value].text;
    }

    private void StatCheckFunction_UI(int value)
    {
        dropdown.onValueChanged.RemoveAllListeners();
        dropdown.options.Clear();

        for (int i = 0; i < op_title.Count; i++)
        {
            Dropdown.OptionData newData = new Dropdown.OptionData();
            newData.text = op_title[i];
            dropdown.options.Add(newData);
        }

        dropdown.SetValueWithoutNotify(-1);
        dropdown.SetValueWithoutNotify(0);

        dropdown.value = value;
        dropdown.itemText.text = op_title[dropdown.value];
    }
}
