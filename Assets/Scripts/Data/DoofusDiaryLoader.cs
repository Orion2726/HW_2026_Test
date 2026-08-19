using UnityEngine;

public class DoofusDiaryLoader : MonoBehaviour
{
    public static GameConfig Config { get; private set; }

    void Awake()
    {
        LoadConfig();
    }

    void LoadConfig()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("doofus_diary");

        if (jsonFile == null)
        {
            Debug.LogError("Could not find doofus_diary.json in Resources folder.");
            return;
        }

        Config = JsonUtility.FromJson<GameConfig>(jsonFile.text);

        Debug.Log("Doofus Diary loaded successfully!");
        Debug.Log("Player Speed: " + Config.player_data.speed);
        Debug.Log("Pulpit Lifetime: " +
                  Config.pulpit_data.min_pulpit_destroy_time +
                  " - " +
                  Config.pulpit_data.max_pulpit_destroy_time);
        Debug.Log("Pulpit Spawn Time: " +
                  Config.pulpit_data.pulpit_spawn_time);
    }
}