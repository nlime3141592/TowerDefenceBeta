using UnityEngine;
using UnityEngine.UI;

public class UI_Time : MonoBehaviour
{
    private Text cmp_text;

    private void Awake()
    {
        cmp_text = GetComponent<Text>();
    }

    private void Update()
    {
        int playtime = (int)GameManager.s_playtime;
        int min = playtime / 60;
        int sec = playtime % 60;
        cmp_text.text = string.Format("{0:d02}:{1:d02}", min, sec);
    }
}
