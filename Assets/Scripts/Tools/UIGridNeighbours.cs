using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UIGridNeighbours
{
    public static void SetChildNeighbours(Transform gridRoot)
    {
        if (gridRoot == null) 
            return;

        GridLayoutGroup grid = gridRoot.GetComponent<GridLayoutGroup>();

        if (grid == null || grid.constraint != GridLayoutGroup.Constraint.FixedColumnCount) 
            return;

        SetChildNeighbours(gridRoot, grid.constraintCount);
    }


    public static void SetChildNeighbours(Transform gridRoot, int columns)
    {
        if (gridRoot == null || columns <= 0) 
            return;

        List<UISelectable> selectables = new List<UISelectable>(gridRoot.childCount);

        foreach (Transform c in gridRoot)
        {
            var sel = c.GetComponent<UISelectable>();
            if (sel != null) selectables.Add(sel);
        }

        int sCount = selectables.Count;

        if (sCount == 0) 
            return;

        for (int i = 0; i < sCount; i++)
        {
            int col = i % columns;

            UISelectable top = (i - columns >= 0) ? selectables[i - columns] : null;
            UISelectable bottom = (i + columns < sCount) ? selectables[i + columns] : null;
            UISelectable left = (col > 0) ? selectables[i - 1] : null;
            UISelectable right = (col < columns - 1 && i + 1 < sCount) ? selectables[i + 1] : null;

            selectables[i].SetNeighbours(top, bottom, left, right);
        }
    }
}