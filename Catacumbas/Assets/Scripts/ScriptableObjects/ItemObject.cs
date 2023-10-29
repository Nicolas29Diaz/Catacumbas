using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    biblia,
    crucifijo,
    velas,
    corazon,
}

[CreateAssetMenu(fileName = "New Item", menuName = "RitualItems/Item")]
public class ItemObject : ScriptableObject
{
    
    public ItemType itemType;

    
}
