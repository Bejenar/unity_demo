using System;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public Text myText;
    // Публичное поле для хранения значения счетчика
    public int Count;

    void Start()
    {
        Count = 0; // начальное значение счетчика
        myText.text = Count.ToString();
    }

    void Update()
    {
        myText.text = Count.ToString();
    }
}
