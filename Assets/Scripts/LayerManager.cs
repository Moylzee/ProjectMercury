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
            // Get the tile position from the player's position
            Vector3Int cellPosition = collisionTilemap.WorldToCell(player.transform.position);
            // Get the world position of the tile
            Vector3 tilePosition = collisionTilemap.GetCellCenterWorld(cellPosition);

            if (tilePosition.y > player.transform.position.y) {
                player.GetComponent<SpriteRenderer>().sortingOrder = collisionTilemap.GetComponent<TilemapRenderer>().sortingOrder + 1;
            } else if (tilePosition.y < player.transform.position.y) {
                player.GetComponent<SpriteRenderer>().sortingOrder = collisionTilemap.GetComponent<TilemapRenderer>().sortingOrder - 1;
            }
        } 
    }
}
