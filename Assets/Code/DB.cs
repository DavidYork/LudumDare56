using System.Collections.Generic;
using UnityEngine;

public class DB {
    Dictionary<string, Sprite> _illustrations;
    Dictionary<string, TestJson> _testJson;

    public Sprite Illustration(string name) => getResource(ref _illustrations, "illustrations", name);
    public TestJson TestJson(string name) => getJsonResource(ref _testJson, "test_json", name);

    // Private

    public T getResource<T>(ref Dictionary<string, T> dict, string path, string name) where T: Object {
        if (dict == null) {
            Debug.Log($"Creating dictionary for resource {name}");
            dict = new Dictionary<string, T>();
        }

        if (!dict.ContainsKey(name)) {
            var loadedResource = Resources.Load<T>($"{path}/{name}");
            dict.Add(name, loadedResource);
        }

        if (!dict.TryGetValue(name, out T resource)) {
            Debug.LogError($"Cannot find resource {path}/{name}");
        }

        return resource;
    }

    public T getJsonResource<T>(ref Dictionary<string, T> dict, string path, string name) {
        if (dict == null) {
            Debug.Log($"Creating dictionary for resource {name}");
            dict = new Dictionary<string, T>();
        }

        if (!dict.ContainsKey(name)) {
            var text = Resources.Load<TextAsset>($"{path}/{name}");
            var loadedResource = JsonUtility.FromJson<T>(text.ToString());
            dict.Add(name, loadedResource);
        }

        if (!dict.TryGetValue(name, out T resource)) {
            Debug.LogError($"Cannot find resource {path}/{name}");
        }

        return resource;
    }
}
