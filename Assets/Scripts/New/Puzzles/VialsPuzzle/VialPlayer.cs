using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VialPlayer : MonoBehaviour
{
    public Vial selectedVial;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.transform.TryGetComponent(out Vial vial))
                {
                    if (selectedVial != null)
                    {
                        selectedVial.ExportLiquid(vial);
                        selectedVial = null;
                    }
                    else
                    {
                        selectedVial = vial;
                    }
                }
            }
        }
    }
}
