using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Properties
    public Transform column;
    public Item item;

    [Header("Size")]
    public float maxSize = 1;
    public float minSize = 0.3f;
    public float maxHeight = 10;
    public float maxWidth = 10;
    [Header("Column")]
    public List<int> breakCounts = new List<int>();
    List<Item> items = new List<Item>();
    List<Transform> columns = new List<Transform>();



    #endregion

    #region Behaviour

    public void AddItem(int count)
    {
        var newItem = Instantiate(item);
        newItem.Init(count);
        items.Add(newItem);
        CalculateSizeAndPos();
    }

    public void CalculateSizeAndPos()
    {
        float GetSize(int rowCount,float max)
        {
            float heigth = Mathf.Clamp(max / rowCount, minSize, maxSize);
            return heigth;
        }
        int getColumnCount(int itemCount)
        {
            if (itemCount == 0) return 0;
            int counter = 1;
            foreach (var item in breakCounts)
            {
                if (itemCount > item)
                    counter++;
            }
            if (counter > columns.Count)
                for (int i = columns.Count; i < counter; i++)
                {
                    columns.Add(Instantiate(column, transform));
                }

            return counter;
        }


        //SortItems();
        int columnCount = getColumnCount(items.Count);
        int rowCount = (items.Count) / columnCount;
        if (items.Count % columnCount != 0)
            rowCount += 1;
        float scale = GetSize(rowCount,maxHeight);
        int coulumnIndex = 0;

        int maxWidthInColumn = 0;
        int sumWidthAllColumn = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if ((i != 0 && (i % rowCount) == 0))
            {
                if (coulumnIndex + 1 < columns.Count)
                    coulumnIndex++;
                
            }

            if (items[i].count > maxWidthInColumn)
                maxWidthInColumn = items[i].count;
            print("count" + columnCount);
            print("index" + coulumnIndex);
            items[i].transform.parent = columns[coulumnIndex];

            if ((i % rowCount) == 0 || i == items.Count - 1)
            {
                sumWidthAllColumn += maxWidthInColumn;
                maxWidthInColumn = 0;
            }
        }
        float scaleWidth = GetSize(sumWidthAllColumn,maxWidth);
        if (scaleWidth < scale)
        {
            scale = scaleWidth;
        }
        foreach (var item in items)
        {
            item.SetSize(scale);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item.GetComponent<RectTransform>());

        }

        foreach (var item in columns)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(item.GetComponent<RectTransform>());
        }
        

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

    }

    public void Restart()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Destroy(items[i]);
        }
        for (int i = 0; i < columns.Count; i++)
        {
            Destroy(columns[i]);
        }
    }

    void SortItems()
    {
        int n = items.Count;
        bool swapped;

        for (int i = 0; i < n - 1; i++)
        {
            swapped = false;

            for (int j = 0; j < n - i - 1; j++)
            {
                if (items[j].count > items[j + 1].count)
                {
                    // Swap items[j] and items[j + 1]
                    Item temp = items[j];
                    items[j] = items[j + 1];
                    items[j + 1] = temp;

                    swapped = true;
                }
            }

            // If no two elements were swapped in the inner loop, then the list is sorted
            if (!swapped)
                break;
        }
    }

    #endregion
}
