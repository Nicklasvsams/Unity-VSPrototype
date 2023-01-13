using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(player.gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);


        //Debug.Log("Test");
        //floatingTextManager = (FloatingTextManager)GameObject.Find("FloatingText").GetComponent("FloatingTextManager");
        //player = (PlayerController)GameObject.Find("Player").GetComponent("PlayerController");
        

        // DeleteSaves();
    }

    // Ressources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public PlayerController player;
    public FloatingTextManager floatingTextManager;

    // Logic
    public int currency;
    public int experience;

    public void ShowText(string text, int fontsize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(text, fontsize, color, position, motion, duration);
    }

    /*
     * int preferredSkin
     * int currency
     * int experience
     * int weaponLevel
     */
    public void SaveState()
    {
        Debug.Log("Save state");

        string s = "";

        s += "0|";
        s += currency + "|";
        s += experience + "|";
        s += "0";

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState")) return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // Change player skin
        // skin = int.Parse(data[0]);
        currency = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        // Change weapon level
        // weapon = int.Parse(data[3]);

        Debug.Log("Load state");
    }

    private void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Deleted");
    }
}
