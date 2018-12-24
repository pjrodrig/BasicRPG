using UnityEngine;
using System.Collections.Generic;

public class TestMap : MonoBehaviour {
    
    List<Edge> edges;
    SortedDictionary<string, Space> spaces;
    Space start;

    public GameObject spacesObject;

    public void Init() {
        InitSpaces();
        InitEdges();
        InitSpaceTypes();
    }

    void InitSpaces() {
        this.spaces = new SortedDictionary<string, Space>();
        string[] stringSpaces = {
            "s10_5", "s10_7", "s11_7", "s11_8", "s12_2", "s12_3", "s12_5", "s13_3",
            "s13_8", "s14_1", "s14_2", "s14_3", "s14_4", "s14_8", "s14_10", "s16_2",
            "s16_4", "s16_5", "s16_7", "s16_8", "s16_10", "s18_5", "s18_6", "s18_7"
        };
        foreach(string stringSpace in stringSpaces) {
            this.spaces.Add(stringSpace, new Space(stringSpace, spacesObject.transform.Find(stringSpace).position, 1));
        }
        this.spaces.TryGetValue("s14_1", out this.start);
    }

    void InitEdges() {
        string[,] edges = {
            {"s10_5", "s10_7"}, {"s10_5", "s12_5"}, {"s10_7", "s11_7"}, {"s11_7", "s11_8"}, 
            {"s11_8", "s13_8"}, {"s12_2", "s12_3"}, {"s12_2","s14_2"}, {"s12_3", "s12_5"},
            {"s12_3", "s13_3"}, {"s13_3", "s14_3"}, {"s13_8", "s14_8"}, {"s14_1", "s14_2"}, 
            {"s14_2", "s14_3"}, {"s14_2", "s16_2"}, {"s14_3", "s14_4"}, {"s14_4", "s16_4"},
            {"s14_8", "s14_10"}, {"s14_8", "s16_8"}, {"s14_10", "s16_10"}, {"s16_2", "s16_4"}, 
            {"s16_4", "s16_5"}, {"s16_5", "s16_7"}, {"s16_5", "s18_5"}, {"s16_7", "s16_8"},
            {"s16_7", "s18_7"}, {"s16_8", "s16_10"}, {"s18_5", "s18_6"}, {"s18_6", "s18_7"}
        };

        this.edges = new List<Edge>();
        Space a, b;
        for(int i = 0; i < edges.GetLength(0); i++) {
            this.spaces.TryGetValue(edges[i, 0], out a);
            this.spaces.TryGetValue(edges[i, 1], out b);
            this.edges.Add(new Edge(a, b));
        }
    }

    void InitSpaceTypes() {
        object[,] types = {
            {"s12_3", Space.Type.ITEM},
            {"s14_3", Space.Type.SCROLL},
            {"s16_4", Space.Type.SHIELD},
            {"s16_7", Space.Type.WEAPON}
        };
        Space space;
        for(int i = 0; i < types.GetLength(0); i++) {
            this.spaces.TryGetValue((string) types[i, 0], out space);
            space.type = (Space.Type) types[i, 1];
        }
    }

    public Space GetStart() {
        return this.start;
    }

    public Space GetSpace(string id) {
        Space space;
        spaces.TryGetValue(id, out space);
        return space;
    }
}