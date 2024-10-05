using TMPro;
using UnityEngine;

public class ResourceView: MonoBehaviour {
    [SerializeField] TextMeshProUGUI _value;

    public void Set(int val) => _value.text = $"{val}";
}
