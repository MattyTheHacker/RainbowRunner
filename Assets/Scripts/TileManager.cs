using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    //initialize variables
    public GameObject[] tilePrefabs;
    public GameObject startPrefab;
    private List<GameObject> activeTiles;

    private Transform playerTransform;

    private float spawnZ = 10.0f;
    private float tileLength = 10.0f;
    private float safeZone = 15.0f;

    private int numTilesOnScreen = 5;
    private int lastPrefabIndex = 0;

    // Start is called before the first frame update
    private void Start() {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        //initiate starter prefab
        startPrefab = Instantiate(startPrefab, new Vector3(0, 0, 15), Quaternion.identity) as GameObject;
        startPrefab.transform.SetParent(transform);
        spawnZ += 20.0f;
        activeTiles.Add(startPrefab);

        for (int i = 0; i < numTilesOnScreen; i++) {
            //spawn tiles up to limit
            SpawnTile();
        }
    }

    // Update is called once per frame
    private void Update() {
        if(playerTransform.position.z - safeZone > (spawnZ - numTilesOnScreen * tileLength)) {
            SpawnTile();
            DeleteTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1) {
        GameObject go;
        go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    private void DeleteTile() {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex() {
        if(tilePrefabs.Length <= 1) {
            return 0;
        }

        int randomIndex = lastPrefabIndex;

        while(randomIndex == lastPrefabIndex) {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}