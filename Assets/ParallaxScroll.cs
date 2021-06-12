using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public float scrollSpeed;
    public float scrollAcceleration;
    public GameObject sampleTile;
    public List<GameObject> instantiatedTiles;



    private Camera mainCamera;
    private Vector2 screenBounds;
    private Vector2 sampleTileBounds;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        sampleTile = instantiatedTiles[0];
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        sampleTileBounds = sampleTile.GetComponent<BoxCollider2D>().size * sampleTile.transform.localScale;

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
                tile.transform.localPosition = tile.transform.localPosition = new Vector3(tile.transform.localPosition.x, (-screenBounds.y) + (sampleTileBounds.y * 2), tile.transform.localPosition.z);
            }
            scrollSpeed += scrollAcceleration;
        }
    }
}
