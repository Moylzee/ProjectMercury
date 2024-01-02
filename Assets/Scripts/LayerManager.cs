using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LayerManager : MonoBehaviour
{
    public Tilemap collisionTilemap;
    public GameObject player;
    void OnTriggerStay2D(Collider2D collider){
        if (collider.gameObject.CompareTag("Collision")) {
            // Get the approximate contact point
            var contactPoint = collider.ClosestPoint(transform.position);
            // Get the tile position from the contact point
            Vector3Int cellPosition = collisionTilemap.WorldToCell(contactPoint);
            // Get the world position of the tile
            Vector3 tilePosition = collisionTilemap.GetCellCenterWorld(cellPosition);

            if (tilePosition.y > player.transform.position.y) {
                Debug.Log("Player In Front");
                player.GetComponent<SpriteRenderer>().sortingOrder = collisionTilemap.GetComponent<TilemapRenderer>().sortingOrder + 1;
            } else if (tilePosition.y < player.transform.position.y) {
                Debug.Log("Player Behind");
                player.GetComponent<SpriteRenderer>().sortingOrder = collisionTilemap.GetComponent<TilemapRenderer>().sortingOrder - 1;
            }
        } 
    }
}