using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class etc_window_control : MonoBehaviour
{
    public GameObject etc_window;
    public GameObject etc_angel;
    public GameObject etc_start;
    public GameObject select_data;

    Animator warrior_anim;
    Animator angelmon_anim;
    bool isetcopen = false;
    private void Awake()
    {
        warrior_anim = etc_start.GetComponent<Animator>();
        angelmon_anim = etc_angel.GetComponent<Animator>();
    }
    public void OnClickStartBtn()
    {
        warrior_anim.SetTrigger("IsMotion");
        StartCoroutine(CalculateStart());
    }
    public void EtcWindowControl(bool control)
    {
        if (!control) isetcopen = false;
        etc_window.SetActive(control);
    }
    public void EtcClick()
    {
        if (isetcopen) return;
        isetcopen = true;
        angelmon_anim.SetTrigger("IsMotion");
        StartCoroutine(EtcWindowOpen());
    }
    public void RecalculateEtc()
    {
        StartCoroutine(ReCalculateStart());
    }
    IEnumerator CalculateStart()
    {
        yield return new WaitForSeconds(0.5f);
        select_data.GetComponent<select_data_control>().Cal_Start();
    }
    IEnumerator ReCalculateStart()
    {
        yield return new WaitForSeconds(1f);
        EtcWindowControl(false);
    }
    IEnumerator EtcWindowOpen()
    {
        yield return new WaitForSeconds(0.5f);
        EtcWindowControl(true);
    }
}
