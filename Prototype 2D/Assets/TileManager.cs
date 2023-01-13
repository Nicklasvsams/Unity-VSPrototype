using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameObject infinitile;
    [SerializeField] private GameObject player;
    private Vector3 tileVector = new Vector3(0,0);
    private Vector3 lastVector;
    private List<Vector3> tilePosList = new List<Vector3>()
    { 
        new Vector3(-50, 50),
        new Vector3(0, 50),
        new Vector3(50, 50),
        new Vector3(-50, 0),
        new Vector3(0, 0),
        new Vector3(50, 0),
        new Vector3(-50, -50),
        new Vector3(0, -50),
        new Vector3(50, -50),
    };

    private List<GameObject> tiles = new List<GameObject>();

    private void Start()
    {
        lastVector = tileVector;

        for (int i = 0; i < tilePosList.Count; i++)
        {
            tiles.Add(Instantiate(infinitile, tilePosList[i], new Quaternion(), this.gameObject.transform));
        }
    }

    // Update is called once per frame
    void Update()
    {
        int playerX = (int)player.transform.position.x;
        int playerY = (int)player.transform.position.y;

        tileVector.x = (int)playerX / 50;
        tileVector.y = (int)playerY / 50;

        if (tileVector != lastVector)
        {
            if (tileVector.x > lastVector.x) ShiftTilesRight();
            if (tileVector.x < lastVector.x) ShiftTilesLeft();
            if (tileVector.y > lastVector.y) ShiftTilesUp();
            if (tileVector.y < lastVector.y) ShiftTilesDown();

            lastVector = tileVector;
        }
    }

    private void ShiftTilesRight()
    {
        for (int i = 0; i < 3; i++)
        {
            Destroy(tiles[i * 3]);
            tiles[i * 3] = tiles[i * 3 + 1];
            tiles[i * 3 + 1] = tiles[i * 3 + 2];
            print(tileVector.x + " Right " + tileVector.y);
            tiles[i * 3 + 2] = Instantiate(infinitile, new Vector3(tileVector.x * 50 + 50, tileVector.y * 50 + 50 + (50 * -i)), new Quaternion(), this.gameObject.transform);
        }
    }

    private void ShiftTilesLeft()
    {
        for (int i = 0; i < 3; i++)
        {
            Destroy(tiles[i * 3 + 2]);
            tiles[i * 3 + 2] = tiles[i * 3 + 1];
            tiles[i * 3 + 1] = tiles[i * 3];
            print(tileVector.x + " Left " + tileVector.y);
            tiles[i * 3] = Instantiate(infinitile, new Vector3(tileVector.x * 50 - 50, tileVector.y * 50 + 50 + (50 * -i)), new Quaternion(), this.gameObject.transform);
        }
    }

    private void ShiftTilesUp()
    {
        for (int i = 0; i < 3; i++)
        {
            Destroy(tiles[i + 6]);
            tiles[i + 6] = tiles[i + 3];
            tiles[i + 3] = tiles[i];
            print(tileVector.x + " Up " + tileVector.y);
            tiles[i] = Instantiate(infinitile, new Vector3(tileVector.x * 50 - 50 + i * 50, tileVector.y * 50 + 50), new Quaternion(), this.gameObject.transform);
        }
    }

    private void ShiftTilesDown()
    {
        for (int i = 0; i < 3; i++)
        {
            Destroy(tiles[i]);
            tiles[i] = tiles[i + 3];
            tiles[i + 3] = tiles[i + 6];
            print(tileVector.x + " Down " + tileVector.y);
            tiles[i + 6] = Instantiate(infinitile, new Vector3(tileVector.x * 50 - 50 + i * 50, tileVector.y * 50 - 50), new Quaternion(), this.gameObject.transform);
        }
    }
}