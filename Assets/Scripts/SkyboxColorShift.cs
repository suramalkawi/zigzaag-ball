using UnityEngine;

public class SkyboxColorShift : MonoBehaviour
{
    [SerializeField] private float speed = 1f; // سرعة التغير
    private Material skyMat;
    private float t;

    void Start()
    {
        skyMat = RenderSettings.skybox;
    }

    void Update()
    {
        t += Time.deltaTime * speed;
        Color color = Color.HSVToRGB(Mathf.PingPong(t, 1f), 1f, 1f);
        skyMat.SetColor("_SkyTint", color);
    }
}
