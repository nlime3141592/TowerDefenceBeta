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
        // Easing ���-3 : Sin �Լ��� ������ Ȱ���Ͽ� ���� ȭ���� �ΰ��� �������� ���� �̵��� �����߽��ϴ�.
        float y = Mathf.Sin(Time.time * startButtonSpeed) * startButtonAmp;

        rect.anchoredPosition = anchoredPosition + Vector2.up * y;
    }
}