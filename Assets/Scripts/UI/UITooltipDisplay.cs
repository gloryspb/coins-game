using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITooltipDisplay : MonoBehaviour
{
    public GameObject tooltipPrefab;
    private GameObject tooltip;
    public void ShowTooltip(Transform _transform)
    {
        if (tooltip == null)
        {
            tooltip = Instantiate(tooltipPrefab, _transform.position, Quaternion.identity);
            tooltip.transform.SetParent(_transform);
            tooltip.transform.localPosition = new Vector3(0f, 1f, 0f);
        }
    }

    public void HideTooltip()
    {
        if (tooltip != null)
        {
            Destroy(tooltip);
            tooltip = null;
        }
    }
}
