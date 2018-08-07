using System.Collections.Generic;
using UnityEngine;

public class TestMap {

    private List<Edge> edges;
    private SortedDictionary<string, Space> spaces;
    private Space start;

    public TestMap(GameObject spacesObject) {
        initSpaces(spacesObject);
    }

    void initSpaces(GameObject spacesObject) {
        this.spaces = new SortedDictionary<string, Space>();
        string[] stringSpaces = new string[]{
            "s10_5", "s10_7", "s11_7", "s11_8", "s12_2", "s12_3", "s12_5", "s13_3",
            "s13_8", "s14_1", "s14_2", "s14_3", "s14_4", "s14_8", "s14_10", "s16_2",
            "s16_4", "s16_5", "s16_7", "s16_8", "s16_10", "s18_5", "s18_6", "s18_7"
        };
        foreach(string stringSpace in stringSpaces) {
            this.spaces.Add(stringSpace, new Space(spacesObject.transform.Find(stringSpace).position));
        }
        this.spaces.TryGetValue("s14_1", out this.start);
    }

    void initEdges() {
        string[] from = new string[]{
            "s10_5", "s10_5", "s10_7", "s11_7", "s11_8", "s12_2", "s12_2", "s12_3",
            "s12_3", "s13_3", "s13_8", "s14_1", "s14_2", "s14_2", "s14_3", "s14_4",
            "s14_8", "s14_8", "s14_10", "s16_2", "s16_4", "s16_5", "s16_5", "s16_7",
            "s16_7", "s16_8", "s18_5", "s18_6"
        };
        string[] to = new string[]{
            "s10_7", "s12_5", "s11_7", "s11_8", "s13_8", "s12_3", "s14_2", "s12_5",
            "s13_3", "s14_3", "s14_8", "s14_2", "s14_3", "s16_2", "s14_4", "s16_4",
            "s14_10", "s16_8", "s16_10", "s16_4", "s16_5", "s16_7", "s18_5", "s16_8",
            "s18_7", "s16_10", "s18_6", "s18_7"
        };

        this.edges = new List<Edge>();
        Space a, b;
        for(int i = 0; i < from.Length; i++) {
            this.spaces.TryGetValue(from[i], out a);
            this.spaces.TryGetValue(to[i], out b);
            this.edges.Add(new Edge(a, b));
        }
    }

    public Space GetStart() {
        return this.start;
    }
}
