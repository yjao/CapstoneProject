using UnityEngine;
using System.Collections;

public class Item{
    public string Name;
    public string Description;
    public string Filename;

    public Item(string name, string description, string filename)
    {
        Name = name;
        Description = description;
        Filename = filename;
    }
}
