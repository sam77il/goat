using System.IO;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject inventoryPrefab;
    [Header("Items")]
    [SerializeField] private List<BaseItem> items;
    private TMP_Text playerLevelText;
    public static GameManager Instance;
    private Player MyPlayer;
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
        chosenPlayer.inventory = new List<Item>()
        {
            new(items[0], 3),
            new(items[1], 6) 
        };

        if (chosenPlayer != null)
        {
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                GameObject p = Instantiate(playerPrefab);
                Player player = p.GetComponent<Player>();
                GameObject inventoryObj = Instantiate(inventoryPrefab);
                Canvas canvas = FindAnyObjectByType<Canvas>();
                inventoryObj.transform.SetParent(canvas.transform, false);
                InventoryManager inventoryManager = inventoryObj.GetComponentInChildren<InventoryManager>();
                inventoryManager.Initialize(chosenPlayer.inventory);
                Debug.Log(inventoryManager);
                player.Initialize(chosenPlayer.playerName, chosenPlayer.level, inventoryManager);
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
