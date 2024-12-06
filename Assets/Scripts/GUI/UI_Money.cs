using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Money : MonoBehaviour
{
    private Text cmp_text;

    private void Awake()
    {
        cmp_text = GetComponent<Text>();
    }

    private void Update()
    {
        cmp_text.text = string.Format("{0}", GameManager.s_money);
    }
}
