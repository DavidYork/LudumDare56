#nullable disable

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

//----------------------------------------------------------------------------------------------------------------------

public class InfoDatabase<T> where T:IIsInfo, new() {
    string _path;
    Dictionary<string, T> _infosByFilename;
    List<T> _rawInfos = new();

    public T[] Infos => _rawInfos.ToArray();
    public int NumInfos => _rawInfos.Count;
    public Func<T, T, int> SortFunction { get; set; } = null;
    public Action<T, string> Initializer { get; set; } = null;

    public void CreateMissingInfos(string[] names) {
        foreach (var name in names) {
            var fileID = name;
            if (!fileID.ToLower().Contains(".json")) {
                fileID += ".json";
            }
            var fullName = Path.Combine(_path, fileID);
            if (!File.Exists(fullName)) {
                var info = new T();
                info.FileID = fileID;
                if (Initializer != null) {
                    Initializer(info, name);
                }
                Save(info, fileID);
                _rawInfos.Add(info);
            }
        }

        if (SortFunction != null) {
            SortCollection();
        }
    }

    public void CreateInitialEntryIfNoneExist(string fileID) {
        if (_rawInfos.Count == 0) {
            var newInfo = new T() {
                FileID = fileID
            };
            if (Initializer != null) {
                Initializer(newInfo, fileID);
            }
            _rawInfos.Add(newInfo);
            _infosByFilename.Add(fileID, newInfo);
            Save(newInfo, fileID);
        }
    }

    public T GetInfo(int index) => _rawInfos[index];

    public T GetInfo(string filename) => _infosByFilename[filename];

    public T GetInfoOrNull(string filename) {
        _infosByFilename.TryGetValue(filename, out T rv);
        return rv;
    }

    public void Rename(T info, string oldName, string newName) {
        if (oldName == newName) {
            Log.Error($"Error, renaming file without changing name: {oldName}");
            return;
        }
        Save(info, newName);

        var oldFilename = Path.Combine(_path, oldName);
        if (!oldFilename.EndsWith(".json")) {
            oldFilename += ".json";
        }
        if (File.Exists(oldFilename)) {
            File.Delete(oldFilename);
        }

        if (SortFunction != null) {
            SortCollection();
        }
    }

    public void Save(T info, string filenameWithoutPath) {
        if (filenameWithoutPath.IsNil()) {
            throw new ArgumentException("Nil filename for info");
        }
        if (!filenameWithoutPath.ToLower().Contains(".json")) {
            filenameWithoutPath += ".json";
        }
        var json = toJson(info);

        var filename = Path.Combine(_path, filenameWithoutPath);
        File.WriteAllText(filename, json);
    }

    public void SaveNew(T info, string filenameWithoutPath) {
        _rawInfos.Add(info);
        Save(info, filenameWithoutPath);
        info.FileID = Path.GetFileNameWithoutExtension(filenameWithoutPath);
        if (SortFunction != null) {
            SortCollection();
        }
    }

    public void SortCollection() {
        if (SortFunction == null) {
            Log.Error($"Null sort function, cannot sort collection in InfoDatabase<{nameof(T)}>");
            return;
        }
        _rawInfos.Sort((a, b) => SortFunction(a, b));
    }

    public InfoDatabase(string path): this(path, null, null) { }

    public InfoDatabase(string path, Func<T, T, int> sortFunction): this(path, null, sortFunction) { }

    public InfoDatabase(string path, Action<T, string> initializer): this(path, initializer, null) { }

    public InfoDatabase(string path, Action<T, string> initializer, Func<T, T, int> sortFunction) {
        _path = path;
        Initializer = initializer;
        SortFunction = sortFunction;
        _infosByFilename = new Dictionary<string, T>();

        foreach (var file in _path.GetFilesSorted("*.json")) {
            var contents = File.ReadAllText(file);
            var info = fromJson(contents);
            if (info == null) {
                Log.Error($"Null info ({typeof(T)}) in {file}");
                continue;
            }
            info.FileID = Path.GetFileNameWithoutExtension(file);
            _rawInfos.Add(info);
            _infosByFilename.Add(Path.GetFileName(file).Replace(".json", ""), info);
        }

        if (SortFunction != null) {
            SortCollection();
        }
    }

    // Private

    static T fromJson(string json) => JsonConvert.DeserializeObject<T>(json);

    static string toJson(T info) => JsonConvert.SerializeObject(info, Formatting.Indented,
        new JsonSerializerSettings() { ContractResolver = new IgnorePropertiesResolver() });

    // Short helper class to prevent properties from being serialized
    class IgnorePropertiesResolver : DefaultContractResolver {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization) {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            if (member.MemberType == MemberTypes.Property) {
                property.ShouldSerialize = _ => false;
            }
            return property;
        }
    }
}
