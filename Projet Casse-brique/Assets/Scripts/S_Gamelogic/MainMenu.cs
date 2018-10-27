using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private Animator menuAnim;
    [SerializeField] private AudioSource audioButtons;

    [SerializeField] private GameObject creditsUI;
    [SerializeField] private SceneFader sf;


    [SerializeField] private bool isAnimating;
    [SerializeField] private bool showCredits = false;

    private void Start()
    {
        creditsUI.SetActive(false);
    }



    public void Play()
    {
        audioButtons.Play();

        if (!isAnimating && !showCredits)
        {
            isAnimating = true;
            menuAnim.Play("anim_GoUp");
        }
    }

    public void CancelPlay()
    {
        audioButtons.Play();

        if(!isAnimating && !showCredits)
        {
            isAnimating = true;
            menuAnim.Play("anim_GoDown");
        }
    }




    public void StopAnimation()
    {
        isAnimating = false;
    }









    public void LoadLevel(int index)
    {
        audioButtons.Play();

        sf.FadeToScene(index);
    }

    public void Credits(bool b)
    {
        audioButtons.Play();

        showCredits = b;
        creditsUI.SetActive(b);
    }

    public void Quit()
    {
        audioButtons.Play();

        if (!isAnimating)
        {
            sf.FadeToQuitScene();
        }
    }
}
