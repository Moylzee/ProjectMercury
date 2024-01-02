using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingRoomManager : MonoBehaviour
{
    public GameObject player;
    public GameObject dialogueBox;
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueBox.activeSelf == false)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
        }else if(dialogueBox.activeSelf == true)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
