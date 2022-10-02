using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loading_manager : MonoBehaviour
{
    public GameObject irene_animation;

    Animator irene_anim_controllor;
    bool isStart = false;

    private void Awake()
    {
        irene_anim_controllor = irene_animation.GetComponent<Animator>();
    }
    public void OnClickIrene()
    {
        if (isStart) return;

        isStart = true;
        irene_anim_controllor.SetTrigger("IsMotion");
        StartCoroutine(GoToMain());
    }
    IEnumerator GoToMain()
    {
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene("Select");
    }
}
