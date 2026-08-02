using UnityEngine;

// Simple singleton GameManager to handle round flow and respawn.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnPlayerDefeated(Fighter defeated)
    {
        Debug.Log($"Player defeated: {defeated.gameObject.name}");

        // TODO: Show round win UI and allow restart. For now we just log.
        // In a later commit we'll show a UI, stop input, and present a restart button.
    }
}
