using System;
using TMPro;
using UnityEngine;

public class Tooltip: MonoBehaviour {
    [SerializeField] TextMeshProUGUI _content;
    [SerializeField] GameObject _tooltipAnchor;

    public void Hide() => _tooltipAnchor.SetActive(false);

    public void Show(Resource res) {
        _content.text = res switch {
            Resource.Coins => describeCoins(),
            Resource.Dust => describeDust(),
            Resource.Fairies => describeFairies(),
            Resource.Fruit => describeFruit(),
            Resource.Trinkets => describeTrinkets(),
            Resource.Stories => describeStories(),
            _ => throw new ArgumentException($"Cannot understand {res}")
        };
        _tooltipAnchor.SetActive(true);
    }

    // Private

    string describeCoins() {
        return "Coins are a currently useless to fairies but useful to many of the creatures of the land.\nSometimes a coin can get you out of a lot of trouble!";
    }

    string describeDust() {
        return "Fairy dust is a magic substance with many uses. The most common use of it is to make things invisible.\nKeep your dust! You'll need it to hide your colony from predators once you start building it.";
    }

    string describeFairies() {
        return "These are your fairies you have been tasked to protect.\nFind a good new home for them.";
    }

    string describeFruit() {
        return "Feyfruit is delicious and is said to have many magical and medicinal properties. It's the primary food of fairies.\nWhen you create your colony you'll plant your fruit to create new trees.";
    }

    string describeStories() {
        return "Stories about this new land will help your fairies understand, love, and live with the land around them. Stories must be given to you."
            + $"\nYou have {LD.Data.AnimalStories} stories about this land's animals."
            + $"\nYou have {LD.Data.PlantStories} stories about this land's plants."
            + $"\nYou have {LD.Data.MagicStories} stories about this land's magic."
            + $"\nYou have {LD.Data.BeautyStories} stories about this land's beauty.";
    }

    string describeTrinkets() {
        return "Trinkets are things that fairies play with and make games around.\nThese are a major source of happiness for your fairies";
    }
}