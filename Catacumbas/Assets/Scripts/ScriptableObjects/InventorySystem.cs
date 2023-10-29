using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Inventory", menuName = "Inventory System/Inventory")]
public class InventorySystem : ScriptableObject
{
   public List<ItemObject> container = new List<ItemObject>();
    public void AddItem(ItemObject item)
    {
        if (!container.Contains(item))
        {
            container.Add(item);
        }
    }
}
