using System.Collections.Generic;
using UnityEngine;

public class DB {
    Dictionary<string, Sprite> _illustrations = new Dictionary<string, Sprite>();
    Dictionary<string, TestJson> _testJson = new Dictionary<string, TestJson>();
    Dictionary<string, AudioClip> _sfx = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> _ambients = new Dictionary<string, AudioClip>();

    public Sprite Illustration(string name) => getResource(_illustrations, "Illustrations", name);
    public TestJson TestJson(string name) => getJsonResource(_testJson, "test_json", name);
    public AudioClip Sound(string name) => getResource(_sfx, "Sounds", name);
    public AudioClip Ambient(string name) => getResource(_ambients, "Ambients", name);

    // Private

    public T getResource<T>(Dictionary<string, T> dict, string path, string name) where T: Object {
        if (!dict.ContainsKey(name)) {
            var loadedResource = Resources.Load<T>($"{path}/{name}");
            if (loadedResource == null) {
                Debug.LogError($"Cannot load resource {loadedResource}");
                return null;
            }
            dict.Add(name, loadedResource);
        }

        if (!dict.TryGetValue(name, out T resource)) {
            Debug.LogError($"Cannot find resource {path}/{name}");
        }

        return resource;
    }

    public T getJsonResource<T>(Dictionary<string, T> dict, string path, string name) {
        if (!dict.ContainsKey(name)) {
            var text = Resources.Load<TextAsset>($"{path}/{name}");
            var loadedResource = JsonUtility.FromJson<T>(text.ToString());
            if (loadedResource == null) {
                Debug.LogError($"Cannot load resource {loadedResource}");
                return default;
            }
            dict.Add(name, loadedResource);
        }

        if (!dict.TryGetValue(name, out T resource)) {
            Debug.LogError($"Cannot find resource {path}/{name}");
        }

        return resource;
    }
}
