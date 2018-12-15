using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Inventory : ISerializationCallbackReceiver {
    
    [NonSerialized]
    public List<Item> items;
    [NonSerialized]
    public List<Scroll> scrolls;
    public Type[] itemTypes;
    public Type[] scrollTypes;

    public Inventory() {
        items = new List<Item>(8);
        scrolls = new List<Scroll>(8);
    }

    public void OnBeforeSerialize() {
        itemTypes = new Type[items.Count];
        int i = 0;
        foreach(Item item in items) {
            itemTypes[i++] = item.GetType();
        }
        i = 0;
        scrollTypes = new Type[scrolls.Count];
        foreach(Scroll scroll in scrolls) {
            scrollTypes[i++] = scroll.GetType();
        }
    }

    public void OnAfterDeserialize() {
        items = new List<Item>(8);
        if(itemTypes != null) {
            foreach(Type type in itemTypes) {
                items.Add((Item) Activator.CreateInstance(type));
            }
        }
        scrolls = new List<Scroll>(8);
        if(scrollTypes != null) {
            foreach(Type type in scrollTypes) {
                scrolls.Add((Scroll) Activator.CreateInstance(type));
            }
        }
    }
}