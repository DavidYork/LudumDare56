using System.Collections.Generic;
using UnityEngine;

public class FairyParty: MonoBehaviour {
    GameObject[] _fairies;

    public void Hide() => gameObject.SetActive(false);

    public void Show(int numFairies) {
        for (var i=0; i<_fairies.Length; i++) {
            _fairies[i].gameObject.SetActive(i <= numFairies);
        }
        gameObject.SetActive(true);
    }

    // Private

    void Awake() {
        var children = new List<GameObject>();
        foreach (Transform child in transform) {
            children.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
        _fairies = children.ToArray();
    }
}