using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class etc_window_control : MonoBehaviour
{
    public GameObject etc_window;
    public GameObject etc_angel;

    Animator angelmon_anim;
    bool isetcopen = false;
    private void Awake()
    {
        angelmon_anim = etc_angel.GetComponent<Animator>();
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
