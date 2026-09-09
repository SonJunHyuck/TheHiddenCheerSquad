using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextRetroEffect : MonoBehaviour
{
    [Range(0, 1)] public float S;
    [Range(0, 1)] public float V;

    private TextMeshProUGUI textComponent;

    // 색상 변화 속도 조절 변수
    public float colorChangeSpeed = 1f;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // 시간 기반으로 Hue 값을 변경하여 색상을 만듦
        float hue = Mathf.PingPong(Time.time * colorChangeSpeed, 1f); // 0에서 1 사이의 값 반복
        Color newColor = Color.HSVToRGB(hue, S, V); // 채도(S)와 밝기(V)는 1로 고정

        // 텍스트 색상 업데이트
        textComponent.color = newColor;
    }
}