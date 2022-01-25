using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SunCount : MonoBehaviour
{
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        GameController.Instance.Data.Sun.SubscribeToText(text, (num) => $"Sun: {num}");
    }
}
