using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource musicSource;   // ضع هنا الموسيقى (Loop)

    [Header("UI Button")]
    public Button muteButton;         // زر السماعة
    public Image buttonImage;         // الصورة داخل الزر
    public Sprite speakerOn;          // صورة السماعة شغالة
    public Sprite speakerOff;         // صورة السماعة مقفلة

    private bool isMuted = false;

    void Start()
    {
        // تشغيل الموسيقى
        if (musicSource != null)
            musicSource.Play();

        // ربط الزر بالوظيفة
        if (muteButton != null)
            muteButton.onClick.AddListener(ToggleMute);

        // الصورة الابتدائية للزر
        if (buttonImage != null && speakerOn != null)
            buttonImage.sprite = speakerOn;
    }

    public void ToggleMute()
    {
        if (musicSource == null) return;

        isMuted = !isMuted;
        musicSource.mute = isMuted;

        // تغيير صورة الزر حسب الحالة
        if (buttonImage != null)
        {
            if (isMuted && speakerOff != null)
                buttonImage.sprite = speakerOff;
            else if (!isMuted && speakerOn != null)
                buttonImage.sprite = speakerOn;
        }
    }
}
