using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class Tooltip : MonoBehaviour
{


    private readonly float hoverTimeToEnableTooltip = 2f;
    private float hoverTime;

    public bool isHovering = false;
    public GameObject tooltip;
    private string ToolTipText;

    private void Start()
    {
        tooltip = GameObject.FindWithTag("ToolTip");
    }

    private void Update()
    {
        if (!isHovering) return;

        hoverTime += Time.deltaTime;

        if (hoverTime > hoverTimeToEnableTooltip)
        {


            // Enable Tooltip
            tooltip.SetActive(true);
            tooltip.GetComponent<TextMeshProUGUI>().text = ToolTipText;
        }

    }

    /* Enable prompt on pointer enter*/
    public void OnMouseEnter()
    {
        hoverTime = 0f;
        isHovering = true;
        Debug.Log("ayyy oyoo");
    }

    /* Disable the prompt on pointer exit*/
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        tooltip.SetActive(false);
    }


    public void SetText(string txt)
    {
        ToolTipText = txt;
    }


}