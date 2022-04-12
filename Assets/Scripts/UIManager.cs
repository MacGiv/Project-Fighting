using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFighting.FirstRound;

public class UIManager : MonoBehaviour
{
    public GameObject[] slides;
    public GameObject canvas;

    public GameObject keyboardControls;
    //public int finalSlide = 3;

    private int slideIndex = 0;
    

    void Awake()
    {
        //FindObjectOfType<GameSpeed>().gameSpedValue = 0;
        //canvas.SetActive(true);
        //slides[slideIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextSlide()
    {
        slides[slideIndex].SetActive(false);
        slides[slideIndex + 1].SetActive(true);
        slideIndex++;
    }

    public void PrevSlide()
    {
        slides[slideIndex].SetActive(false);
        slides[slideIndex - 1].SetActive(true);
        slideIndex--;
    }

    public void SkipTutorial()
    {
        slides[slideIndex].SetActive(false);
        slideIndex = slides.Length - 1;
        slides[slideIndex].SetActive(true);
    }

    public void StartGame()
    {
        slides[slideIndex].SetActive(false);
        canvas.SetActive(false);
        FindObjectOfType<GameSpeed>().gameSpedValue = 1;
    }

    public void SeeControls()
    {
        FindObjectOfType<GameSpeed>().gameSpedValue = 0;
        canvas.SetActive(true);
        keyboardControls.SetActive(true);
    }

    public void QuitControls()
    {
        FindObjectOfType<GameSpeed>().gameSpedValue = 1;
        canvas.SetActive(false);
        keyboardControls.SetActive(false);
    }
}
