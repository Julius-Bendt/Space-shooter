using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {

    public Gradient planetHealthColor;

    public Slider planetSlider, energySlider;
    public Image planetSliderFiller;

    public GameObject gameoverTab;

    public TextMeshProUGUI obj_text, title_text;
    private string obj_t = string.Format("Defend earth \n from the alien ivasion.");
    private string t_text = "Objective >:";
    private char[] title_char;
    private char[] obj_char;
    private float speed = 0.075f;

    GameController gc;

    private float sliderTarget;

    public static bool inited = false;

    public void UpdatePlanetHealthbar()
    {
        if(!inited)
            Start();
        
        float procent = (float)Planet.health/(float)gc.maxPlanetHealth;



        sliderTarget = (float)Planet.health;

        StartCoroutine(SliderEffect());
       
        

        planetSliderFiller.color = planetHealthColor.Evaluate(procent);

        
    }

    private void Start()
    {

        if (inited)
            return;
        
        if (planetSlider == null)
            planetSlider = GameObject.Find("planet healthbar").GetComponent<Slider>();

        if (planetSliderFiller == null)
            planetSliderFiller = GameObject.Find("Fill(planet health)").GetComponent<Image>();


        if (energySlider == null)
            energySlider = GameObject.Find("energy bar").GetComponent<Slider>();

        if (gameoverTab == null)
            gameoverTab = GameObject.Find("Game over UI");

        gameoverTab.SetActive(false);

        if (obj_text == null)
            obj_text = GameObject.Find("obj_desc").GetComponent<TextMeshProUGUI>();

        if (title_text == null)
            title_text = GameObject.Find("obj_title").GetComponent<TextMeshProUGUI>();

        gc = FindObjectOfType<GameController>();
        if (obj_text != null)
        {
            title_char = t_text.ToCharArray();
            obj_char = obj_t.ToCharArray();
            StartCoroutine(ObjectText());
        }

        inited = true;
    }



    IEnumerator ObjectText()
    {

        for (int i = 0; i < title_char.Length; i++)
        {
            gc.ac.PlaySound(gc.beep);
            title_text.text += title_char[i];

            yield return new WaitForSeconds(speed);
        }

        yield return new WaitForSeconds(0.4f);

        for (int i = 0; i < obj_char.Length; i++)
        {
            speed = 0.075f;
            gc.ac.PlaySound(gc.beep);
            obj_text.text += obj_char[i];

            yield return new WaitForSeconds(speed);
        }


        //Delay
        yield return new WaitForSeconds(3);

        //Done writing

        Transform p = obj_text.transform.parent;

        TextMeshProUGUI[] texts = p.GetComponentsInChildren<TextMeshProUGUI>();

        float speedToFade = 2f;

        float ElapsedTime = 0.0f;

        Color startingColor = texts[0].color;

        while (ElapsedTime < speedToFade)
        {
            ElapsedTime += Time.deltaTime;
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].color = Color.Lerp(startingColor, Color.clear, (ElapsedTime / speedToFade));
            }

            yield return null;
        }

        gc.Init();

        yield return null;
    }

    public void LoadLevel(string name)
    {
        try
        {
            SceneManager.LoadScene(name);
        }
        catch(Exception e)
        {
            Debug.Log(string.Format("Found error trying to load level '{0}'. error: {1}", name,e.Message));
        }
        
    }


    public void RestartLevel()
    {
        inited = false;

        LoadLevel("game");
    }

    public void Exit()
    {
        Application.Quit();
    }


    IEnumerator SliderEffect()
    {
        float  i = 0f;
        var rate = 1f / 5f;

        float startValue = planetSlider.value;

        while (i < 1.0)
        {
            i += Time.deltaTime * rate;
            planetSlider.value = Mathf.Lerp(startValue, sliderTarget, i);
        }
            yield return null; 
    }
}
