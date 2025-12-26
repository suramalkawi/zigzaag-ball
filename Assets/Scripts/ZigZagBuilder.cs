using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagBuilder : MonoBehaviour
{
    private List<GameObject> Tiles = new List<GameObject>();

    [SerializeField] private GameObject TilePrefab;
    [SerializeField] private GameObject character;

    private Vector3 nextDirection = Vector3.right;
    [SerializeField] private int totalTiles = 50;
    [SerializeField] private Vector3 startPos = new Vector3(3, 0, 2);

    [Header("Tile Size Settings")]
    [SerializeField] private float minSize = 0.5f;
    [SerializeField] private float maxSize = 2f;
    [SerializeField] private float tileHeight = 1f;
    [SerializeField] private float shrinkAmount = 0.05f;
    [SerializeField] private int startShrinkAfter = 50;
    [SerializeField] private int shrinkStepInterval = 6;

    private Vector3 currentScale;
    private int totalTilesCreated = 0;
    private bool firstStart = true;

    private void Start()
    {
        if (TilePrefab == null || character == null)
        {
            Debug.LogError("ZigZagBuilder: TilePrefab or character not assigned!");
            return;
        }

        if (firstStart)
        {
            currentScale = new Vector3(maxSize, tileHeight, maxSize);
            totalTilesCreated = 0;
            firstStart = false;
        }

        BuildInitialPath();
    }

    public void BuildInitialPath()
    {
        ClearTiles();

        GameObject first = Instantiate(TilePrefab, startPos, Quaternion.identity);
        first.transform.localScale = currentScale;
        Tiles.Add(first);
        totalTilesCreated++;

        for (int i = 1; i < totalTiles; i++)
            AddNewTile();
    }

    public void ResetBuilderPath()
    {
        currentScale = new Vector3(maxSize, tileHeight, maxSize);
        totalTilesCreated = 0;

        ClearTiles();

        GameObject first = Instantiate(TilePrefab, startPos, Quaternion.identity);
        first.transform.localScale = currentScale;
        Tiles.Add(first);
        totalTilesCreated++;

        for (int i = 1; i < totalTiles; i++)
            AddNewTile();
    }

    private void ClearTiles()
    {
        if (Tiles != null)
        {
            foreach (var tile in Tiles)
                if (tile != null) Destroy(tile);
            Tiles.Clear();
        }
        else
            Tiles = new List<GameObject>();
    }

    private void AddNewTile()
    {
        if (Tiles.Count == 0)
        {
            GameObject first = Instantiate(TilePrefab, startPos, Quaternion.identity);
            first.transform.localScale = currentScale;
            Tiles.Add(first);
            totalTilesCreated++;
            return;
        }

        nextDirection = (Random.Range(0, 2) == 1) ? Vector3.right : Vector3.forward;

        Vector3 lastPos = Tiles[Tiles.Count - 1].transform.position;
        Vector3 lastScale = Tiles[Tiles.Count - 1].transform.localScale;
        Vector3 newPos = lastPos;

        if (nextDirection == Vector3.right)
            newPos.x += (lastScale.x + currentScale.x) / 2f;
        else
            newPos.z += (lastScale.z + currentScale.z) / 2f;

        GameObject newTile;
        if (Tiles.Count < totalTiles)
        {
            newTile = Instantiate(TilePrefab, newPos, Quaternion.identity);
            newTile.transform.localScale = currentScale;
            Tiles.Add(newTile);
        }
        else
        {
            newTile = Tiles[0];
            newTile.transform.position = newPos;
            newTile.transform.localScale = currentScale;
            Tiles.Add(newTile);
            Tiles.RemoveAt(0);
        }

        totalTilesCreated++;

        if (totalTilesCreated > startShrinkAfter && totalTilesCreated % shrinkStepInterval == 0)
        {
            currentScale.x = Mathf.Max(minSize, currentScale.x - shrinkAmount);
            currentScale.z = Mathf.Max(minSize, currentScale.z - shrinkAmount);
        }
    }

    void Update()
    {
        if (Tiles.Count > 0 && Vector3.Distance(character.transform.position, Tiles[(Tiles.Count - 1) / 2].transform.position) < 2f)
            AddNewTile();
    }
}
