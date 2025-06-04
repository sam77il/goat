using System.IO;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;
    [Header("Player Data")]
    private TMP_Text playerLevelText;
    public static GameManager Instance;
    public Player MyPlayer;
    public int CurrentLevel { get; private set; } = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame(string playerName)
    {
        List<PlayerSaveData> players = ReadJsonData(Application.persistentDataPath + "/data.json");
        PlayerSaveData chosenPlayer = players.Find(p => p.playerName == playerName);
        chosenPlayer.inventory = new List<Item>
        {
          new("sword", "Sword", 4),
          new("shield", "Shield", 2)
        };

        if (chosenPlayer != null)
        {
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                GameObject p = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
                Player player = p.AddComponent<Player>();
                player.Initialize(chosenPlayer.playerName, chosenPlayer.level, chosenPlayer.inventory);
                MyPlayer = player;
                CurrentLevel = chosenPlayer.level;
                playerLevelText = GameObject.Find("PlayerLevel_Text").GetComponent<TMP_Text>();
                playerLevelText.text = $"Level: {MyPlayer.pLevel}";

                // Unsubscribe danach
                SceneManager.sceneLoaded -= null;
            };

            SceneManager.LoadScene(chosenPlayer.level);
        }
    }

    private List<PlayerSaveData> ReadJsonData(string path)
    {
        if (!File.Exists(path)) return new List<PlayerSaveData>();

        string json = File.ReadAllText(path);
        PlayersManager playersManager = JsonUtility.FromJson<PlayersManager>(json);

        return playersManager.players;
    }
}
