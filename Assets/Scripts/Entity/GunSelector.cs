using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelector : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gun1")
        {
            Debug.Log("gun1");
            DisableGuns("Gun2", "Gun3");
        }
        else if (collision.gameObject.tag == "Gun2")
        {
            Debug.Log("gun2");
            DisableGuns("Gun1", "Gun3");
        }
        else if (collision.gameObject.tag == "Gun3")
        {
            Debug.Log("gun3");
            DisableGuns("Gun1","Gun2");
        }
    }

    void DisableGuns(string gun1, string gun2)
    {
        GameObject g1 = GameObject.FindGameObjectWithTag(gun1);
        GameObject g2 = GameObject.FindGameObjectWithTag(gun2);

        if (g1 != null && g1.GetComponent<BoxCollider2D>() != null)
        {
            g1.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (g2 != null && g2.GetComponent<BoxCollider2D>() != null)
        {
            g2.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
