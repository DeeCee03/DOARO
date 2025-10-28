using UnityEngine;

public class KillCounterUI : MonoBehaviour
{
    public static KillCounterUI Instance; // Singleton-Zugriff

    private int killCount;
    private GUIStyle style;

    void Awake()
    {
        // Singleton Setup
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        // Text-Style für OnGUI
        style = new GUIStyle();
        style.fontSize = 32;
        style.normal.textColor = Color.white;
        style.fontStyle = FontStyle.Bold;
    }

    public void AddKill()
    {
        killCount++;
    }

    void OnGUI()
    {
          string text = $"🧟{killCount}";
    Vector2 pos = new Vector2(20, 20);

 
    GUI.color = Color.black;
    GUI.Label(new Rect(pos.x - 1, pos.y,     300, 50), text, style);
    GUI.Label(new Rect(pos.x + 1, pos.y,     300, 50), text, style);
    GUI.Label(new Rect(pos.x,     pos.y - 1, 300, 50), text, style);
    GUI.Label(new Rect(pos.x,     pos.y + 1, 300, 50), text, style);

 
    GUI.color = Color.white;
    GUI.Label(new Rect(pos.x, pos.y, 300, 50), text, style);

    // Reset
    GUI.color = Color.white;
    }
}
