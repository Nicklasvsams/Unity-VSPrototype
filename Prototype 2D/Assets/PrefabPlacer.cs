using System.Collections.Generic;
using UnityEngine;

public class PrefabPlacer : MonoBehaviour
{
    public GameObject prefab; // Reference to the prefab to be placed
    public GameObject player;
    public float prefabSize = 50; // Size of the prefab (assumed to be square)
    public int gridSize = 3; // Size of the grid (assumed to be square)

    private List<GameObject> placedPrefabs; // List to store the placed prefabs

    void Start()
    {
        // Initialize the list of placed prefabs
        placedPrefabs = new List<GameObject>();

        // Calculate the center of the grid
        Vector3 gridCenter = transform.position;

        // Loop through the grid and calculate the positions of the prefabs
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                // Calculate the position of the prefab
                Vector3 prefabPosition = new Vector3(
                    gridCenter.x - (gridSize / 2) * prefabSize + i * prefabSize,
                    gridCenter.y - (gridSize / 2) * prefabSize + j * prefabSize,
                    0
                );

                // Instantiate the prefab at the calculated position
                GameObject newPrefab = Instantiate(prefab, prefabPosition, new Quaternion(), this.gameObject.transform);

                // Add the prefab to the list of placed prefabs
                placedPrefabs.Add(newPrefab);
            }
        }
    }

    void Update()
    {
        // Check if the player has moved
        if (player.transform.position != new Vector3(0,0,0) / 50)
        {
            // Calculate the new center of the grid
            Vector3 newGridCenter = transform.position;

            // Loop through the placed prefabs and update their positions
            for (int i = 0; i < placedPrefabs.Count; i++)
            {
                // Calculate the new position of the prefab
                Vector3 newPrefabPosition = new Vector3(
                    newGridCenter.x - (gridSize / 2) * prefabSize + (i % gridSize) * prefabSize,
                    newGridCenter.y - (gridSize / 2) * prefabSize + (i / gridSize) * prefabSize,
                    0
                );

                // Update the position of the prefab
                placedPrefabs[i].transform.position = newPrefabPosition;
            }
        }
    }
}