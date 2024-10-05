using System;
using UnityEngine;

public class ResourcesManager: MonoBehaviour {
    [SerializeField] ResourceView _coins;
    [SerializeField] ResourceView _dust;
    [SerializeField] ResourceView _fairies;
    [SerializeField] ResourceView _fruit;
    [SerializeField] ResourceView _stories;
    [SerializeField] ResourceView _trinkets;

    public void Refresh() {
        var data = LD.Data;
        _coins.Set(data.Coins);
        _dust.Set(data.Dust);
        _fairies.Set(data.Fairies);
        _fruit.Set(data.Fruit);
        _stories.Set(data.Stories);
        _trinkets.Set(data.Trinkets);
    }

    public void Modify(Resource resource, int amount) {
        Refresh();
        switch (resource) {
        case Resource.Coins:
            _coins.Wiggle(amount);
            break;
        case Resource.Dust:
            _dust.Wiggle(amount);
            break;
        case Resource.Fairies:
            _fairies.Wiggle(amount);
            break;
        case Resource.Fruit:
            _fruit.Wiggle(amount);
            break;
        case Resource.Trinkets:
            _trinkets.Wiggle(amount);
            break;
        default:
            throw new ArgumentException($"Cannot handle {resource}");
        }
    }
}
