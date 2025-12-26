using UnityEngine;

public class TapToPlayManager : MonoBehaviour
{
    public GameObject tapToPlayText;
    public CharacterMovement player;

    private bool gameStarted = false;

    void Start()
    {
        if (player != null)
            player.enabled = false;
    }

    void Update()
    {
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            gameStarted = true;
            tapToPlayText.SetActive(false);

            if (player != null)
                player.enabled = true;
        }
    }
}
