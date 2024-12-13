using UnityEngine;
using UnityEngine.UI;

public class UI_ResultText : MonoBehaviour
{
    private Text cmp_text;

    private void Awake()
    {
        cmp_text = GetComponent<Text>();
    }

    private void Update()
    {
        if (GameManager.s_playtime <= 0.0f)
        {
            cmp_text.text = "방어 성공";
        }
        else if (GameManager.s_curHeart <= 0)
        {
            cmp_text.text = "방어 실패";
        }
        else
        {
            cmp_text.text = "Invalid Result.";
        }
    }
}
