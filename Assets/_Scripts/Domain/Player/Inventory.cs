using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

[Serializable]
public class Inventory : ISerializationCallbackReceiver {
    
    [NonSerialized]
    public List<Item> items;
    [NonSerialized]
    public List<Scroll> scrolls;
    public string[] itemTypes;
    public string[] scrollTypes;

    public Inventory() {
        items = new List<Item>(8);
        scrolls = new List<Scroll>(8);
    }

    public void AddItem(Item item) {
        if(items.Count == 8) {

        } else {
            items.Add(item);
        }
    }

    public void AddScroll(Scroll scroll) {
        if(scrolls.Count == 8) {

        } else {
            scrolls.Add(scroll);
        }
    }

    public void OnBeforeSerialize() {
        itemTypes = new string[items.Count];
        int i = 0;
        foreach(Item item in items) {
            itemTypes[i++] = item.GetType().ToString();
        }
        i = 0;
        scrollTypes = new string[scrolls.Count];
        foreach(Scroll scroll in scrolls) {
            scrollTypes[i++] = scroll.GetType().ToString();
        }
    }

    public void OnAfterDeserialize() {
        items = new List<Item>(8);
        if(itemTypes != null) {
            foreach(string type in itemTypes) {
                items.Add((Item) Activator.CreateInstance(Assembly.GetExecutingAssembly().GetType(type)));
            }
        }
        scrolls = new List<Scroll>(8);
        if(scrollTypes != null) {
            foreach(string type in scrollTypes) {
                scrolls.Add((Scroll) Activator.CreateInstance(Assembly.GetExecutingAssembly().GetType(type)));
            }
        }
    }
}