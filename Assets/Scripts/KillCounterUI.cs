using UnityEngine;

public class KillCounterUI : MonoBehaviour
{
    public static KillCounterUI Instance; // Singleton-Zugriff

    [Header("UI Settings")]
    public Font customFont;                     // Optional font from the Inspector
    public Color textColor = Color.white;       // Adjustable color in Inspector

    private int killCount;
    private GUIStyle style;

    void Awake()
    {
        // Singleton Setup
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        // Text-Style für OnGUI
        style = new GUIStyle
        {
            fontSize = 20,
            fontStyle = FontStyle.Bold,
            normal = { textColor = textColor }
        };

        if (customFont != null)
            style.font = customFont;
    }

    public void AddKill()
    {
        killCount++;
    }

    void OnGUI()
    {
        string text = $"KILLS: {killCount}";
        Vector2 pos = new Vector2(20, 20);

        // Outline (black shadow)
        GUI.color = Color.black;
        GUI.Label(new Rect(pos.x - 1, pos.y, 300, 50), text, style);
        GUI.Label(new Rect(pos.x + 1, pos.y, 300, 50), text, style);
        GUI.Label(new Rect(pos.x, pos.y - 1, 300, 50), text, style);
        GUI.Label(new Rect(pos.x, pos.y + 1, 300, 50), text, style);

        // Main colored text
        GUI.color = textColor;
        GUI.Label(new Rect(pos.x, pos.y, 300, 50), text, style);

        // Reset GUI color
        GUI.color = Color.white;
    }
}
