using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public class DataPersistence : MonoBehaviour
{
    [Header("File Header Name")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistence Instance {get; private set;}

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("OOPSIE");
        Instance = this;
    }
    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        loadGame();
    }
    public void newGame()
    {
        this.gameData = new GameData();
    }
    public void saveGame()
    {
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }
    public void loadGame() 
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("LOL");
            newGame();
        }

        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }
    private void OnApplicationQuit()
    {
        saveGame();
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
