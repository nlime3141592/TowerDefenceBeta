using UnityEngine;
using UnityEngine.UI;

public class UI_StartButton : MonoBehaviour
{
    public float startButtonSpeed = 3.0f;
    public float startButtonAmp = 15.0f;

    private RectTransform rect;
    private Vector2 anchoredPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        anchoredPosition = rect.anchoredPosition;
    }

    private void Update()
    {
        VibImage();
    }

    private void VibImage()
    {
        // Easing 기능-3 : Sin 함수의 진동을 활용하여 시작 화면의 로고의 디테일한 상하 이동을 구현했습니다.
        float y = Mathf.Sin(Time.time * startButtonSpeed) * startButtonAmp;

        rect.anchoredPosition = anchoredPosition + Vector2.up * y;
    }
}