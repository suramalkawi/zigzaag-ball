using UnityEngine;
using TMPro;

public class TapTextAnimation : MonoBehaviour
{
    public float speed = 2f; // سرعة التغيير
    public float scaleAmount = 1.1f; // أقصى حجم تكبير
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * speed) * (scaleAmount - 1);
        transform.localScale = originalScale * scale;
    }
}
