using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform target; // auf welchen Enemy wir schauen
    public Vector3 offset = new Vector3(0, 2f, 0); // Position über dem Kopf
    public Vector2 size = new Vector2(1.2f, 0.2f);

    private EnemyHealth health;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        health = target.GetComponent<EnemyHealth>();
    }

    void OnGUI()
    {
        if (!health || !cam) return;

        Vector3 pos = cam.WorldToScreenPoint(target.position + offset);

   
        if (pos.z < 0) return;


        float width = size.x * 50f; 
        float height = size.y * 50f;
        float x = pos.x - (width / 2);
        float y = Screen.height - pos.y - height; 

      
        GUI.color = Color.black;
        GUI.DrawTexture(new Rect(x - 1, y - 1, width + 2, height + 2), Texture2D.whiteTexture);

    
        float pct = Mathf.Clamp01((float)health.CurrentHealth / health.MaxHealth);
        GUI.color = Color.Lerp(Color.red, Color.green, pct);
        GUI.DrawTexture(new Rect(x, y, width * pct, height), Texture2D.whiteTexture);

   
        GUI.color = Color.white;
    }
}
