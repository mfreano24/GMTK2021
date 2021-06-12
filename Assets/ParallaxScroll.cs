﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public float width;
    public float scrollSpeed;
    public float scrollAcceleration;
    public GameObject sampleTile;
    
    
    
    private List<GameObject> instantiatedTiles;
    private Camera mainCamera;
    private Vector2 screenBounds;
    private Vector2 sampleTileBounds;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        sampleTileBounds = sampleTile.GetComponent<BoxCollider2D>().size * sampleTile.transform.localScale;

        FillSpace(sampleTileBounds,width);

        //for (int i = 0; i < screenBounds.y / sampleTileBounds.y; i++)
        //{
        //    instantiatedTiles.Add(Instantiate(sampleTile, this.transform));
        //    instantiatedTiles[i].transform.position = Vector3.Scale(new Vector3(1,0,1),transform.position) + (Vector3.up * (-screenBounds.y + (screenBounds.y * ((float)i / (screenBounds.y / sampleTileBounds.y)))));
        //}
    }

    // Update is called once per frame
    void LateUpdate()
    {
        foreach (GameObject tile in instantiatedTiles)
        {
            tile.transform.position -= Vector3.up * scrollSpeed;
            if (tile.transform.localPosition.y < -(screenBounds.y + (sampleTileBounds.y / 2)))
            {
                //tile.transform.localPosition = instantiatedTiles[instantiatedTiles.Count - 1].transform.position + (Vector3.up * sampleTileBounds.y);
                tile.transform.localPosition = tile.transform.localPosition = new Vector3(tile.transform.localPosition.x, (-screenBounds.y) + (sampleTileBounds.y * (instantiatedTiles.Count - 1) + (sampleTileBounds.y/2f)), tile.transform.localPosition.z);
                tile.transform.position -= Vector3.up * scrollSpeed;
            }
            scrollSpeed += scrollAcceleration;
        }
    }


    void FillSpace(Vector2 tileDimensions, float width)
    {
        float horizontalTiles = width / tileDimensions.x;
        float verticalTiles = (screenBounds.y * 2) / tileDimensions.y;
        for (int i = 0; i <= verticalTiles + 1; i++)
        {
            instantiatedTiles.Add(new GameObject("Tile Row"));
            instantiatedTiles[i].transform.localPosition = transform.up * (-screenBounds.y + (tileDimensions.y * i) + (tileDimensions.y / 2));

            for (int j = 0; j < horizontalTiles; j++)
            {
                    GameObject funkyFreshTile = Instantiate(sampleTile, instantiatedTiles[i].transform);
                    funkyFreshTile.transform.localPosition = Vector3.right * ((-screenBounds.x + tileDimensions.x / 2) + (tileDimensions.x * j));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 previewBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        Debug.Log(previewBounds);
        Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        Gizmos.DrawCube(new Vector3(transform.position.x, 0, transform.position.z), new Vector2(width, previewBounds.z));
    }
}
