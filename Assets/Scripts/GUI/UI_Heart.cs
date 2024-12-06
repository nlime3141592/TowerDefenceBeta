using UnityEngine;
using UnityEngine.UI;

public class UI_Heart : MonoBehaviour
{
    private Text cmp_text;

    private void Awake()
    {
        cmp_text = GetComponent<Text>();
    }

    private void Update()
    {
        cmp_text.text = string.Format("{0}", GameManager.s_curHeart);
    }
}
