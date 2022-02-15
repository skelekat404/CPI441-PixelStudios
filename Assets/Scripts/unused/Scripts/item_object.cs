using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum item_type
{
    Resource,
    Consumable,
    Equipment,
    Default
}
public abstract class item_object : ScriptableObject
{
    public int Id;
    public Sprite ui_display;
    public item_type type;
    [TextArea(15,20)]
    public string description;
}
