using System.Collections.Generic;

public class MapUtil {

    public static List<List<Space>> FindPaths(int roll, Player player) {
        Space currentSpace = player.PlayerModel.GetSpace();
        return FindPaths(currentSpace, roll, currentSpace,
            new List<Space>(), new SortedDictionary<string, bool>());
    }

    //TODO: break up this method
    private static List<List<Space>> FindPaths(Space location, int steps, Space previous, List<Space>
        endpoints, SortedDictionary<string, bool> checkedSteps) {
        List<List<Space>> paths = new List<List<Space>>();
        if(steps == 0) {
            if(endpoints.IndexOf(location) == -1) {
                endpoints.Add(location);
                List<Space> newList = new List<Space>();
                newList.Add(location);
                paths.Add(newList);
            }
        } else {
            Space otherSpace;
            string key;
            //TODO: reduce all of the toStrings in here to one
            foreach(Edge edge in location.edges) {
                otherSpace = edge.GetOther(location);
                key = previous.ToString() + "-" + otherSpace.ToString() + "-" + steps;
                //checked steps ensures that only one branch of previous -> otherSpace at n steps will be explored
                if(otherSpace != previous && !checkedSteps.ContainsKey(key)) {
                    checkedSteps.Add(key, true);
                    foreach(List<Space> path in FindPaths(otherSpace, steps - 1, location, endpoints, checkedSteps)) {
                        if(path.Count > 0) {
                            path.Insert(0, location);
                            paths.Add(path);
                        }
                    };
                }
            }
        }
        return paths;
    }
}