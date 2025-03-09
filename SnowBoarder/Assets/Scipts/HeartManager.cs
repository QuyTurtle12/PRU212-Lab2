using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public static int health = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Image[] hearts;

    public Sprite fullheart;
    public Sprite emptyheart;
    // Update is called once per frame

     void Awake()
    {
        health = 3;
    }
    void Update()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyheart;
        }
        for (int i = 0; i < health; i++)
            hearts[i].sprite = fullheart;


    }
}
