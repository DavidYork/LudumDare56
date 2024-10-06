//----------------------------------------------------------------------------------------------------------------------
VAR qol = 0
VAR epicness = 0
VAR score = 0

//----------------------------------------------------------------------------------------------------------------------
=== SettlementOptions
    + [Consider settling here] -> MaybeSettleHere
    + [Move on] -> DONE

= MaybeSettleHere
    Settling is a big decision!
    If you decide this is where the colony should live it will end the game.

    + [Settle here (ends the game)] -> DoSettle
    + [Nevermind, move on] -> DONE

//----------------------------------------------------------------------------------------------------------------------
=== DoSettle
    - -> DoSettlementBuilding ->
    - -> DoSettlementEmpire ->
    - -> DoSettlementQoL ->
    - -> ShowSummary

=== DoSettlementBuilding
    Your fairies start building a new colony using the magic dust to keep the construction hidden.

    + [Continue]

    -

    { animals:
        - 0: Violent conflicts with the {animalName}s kill {RandLoss(Res.Fairies, 500, 1000)} fairies!
        - 1: Tensions grow with the {animalName}s until a bloody conflict ensues. The {animalName}s are driven away but {RandLoss(Res.Fairies, 100, 499)} fairies perish in the conflict.
        - 2: Unable to make friends with the {animalName}s occasional fights break out killing {RandLoss(Res.Fairies, 25, 99)} fairies decide the area isn't attractive enough for them and leave the colony.
        - else: Many fairies make long-lasting friendships with the {animalName}s.
    }

    { plants:
        - 0: The very poor {badPlantName}s of the area makes it hard to plant the feyberries to grow food for the fairies. {RandLoss(Res.Fruit, 50, 75)} feyberries fail to grow into feyberry trees. {RandLoss(Res.Fairies, 100, 200)} fairies starve.
        - 1: The {badPlantName}s make it difficult to grow feyberry trees. {RandLoss(Res.Fruit, 35, 50)} feyberries fail to grow into feyberry trees.
        - 2: The farmers plant the feyberries and they grow into feyberry trees, but {RandLoss(Res.Fruit, 20, 35)} of the trees die.
        - else: The feyberries are planted amidst the {plantName}s and grow into large, healthy feyberry trees yielding delicious fruit.
    }

    { magic:
        - 0: Fairy colonies need a lot of friendly magic to thrive and this location has none, so {RandLoss(Res.Dust, 40, 75)} pinches of magic dust were used to enchant the area. {RandLoss(Res.Fairies, 100, 200)} fairies perish attempting a secret forbidden ritual to enchant the land.
        - 1: The magic of the area needs strengthening so {RandLoss(Res.Dust, 20, 39)} pinches of fairy dust were used.
        - 2: The magic of the area is good but not quite strong enough so {RandLoss(Res.Dust, 10, 19)} pinches of fairy dust were sprinkled on the ground.
        - else: The strong magic of the area strengthens the building of the fairy homes.
    }

    { beauty:
        - 0: Disliking the ugliness of the area {RandLoss(Res.Fairies, 500, 750)} fairies are disheartened and leave the area.
        - 1: Finding the area too ugly for them {RandLoss(Res.Fairies, 250, 499)} fairies leave to find a better home.
        - 2: {RandLoss(Res.Fairies, 100, 249)} fairies decide the area isn't attractive enough for them and leave the colony.
        - else: Often fairies take time out of their day to admire the beauty of this location.
    }

    {
        - get(Res.Dust) < 10: Your lack of fairy dusts fails to protect your colony during construction and predators and the elements take {RandLoss(Res.Fairies, 500, 1000)} lives!
        - get(Res.Dust) < 20: You have so little fairy dust in barely protects your colony from predators and the elements. {RandLoss(Res.Fairies, 250, 500)} die during the construction.
        - get(Res.Dust) < 40: You must use what little fairy dust you have sparingly. You combine its invisibility powers with fairy guards to protect the colony. Nevertheless {RandLoss(Res.Fairies, 100, 249)} still perish as beasts find the colony during construction.
        - get(Res.Dust) < 60: Your fairy dust helps protect you from the elements but some wandering predators stiff you out and eat {RandLoss(Res.Fairies, 50, 99)} of you.
        - get(Res.Dust) < 80: Your fairy dust is effective at protecting you from the elements and most wandering predators, but one wolf finds you and slays {RandLoss(Res.Fairies, 10, 49)} before being driven off.
        - else: Your abundance of fairy dust keeps the colony invisible during the construction.
    }

    + {get(Res.Fairies) <= 0} [Oh no, the colony is gone!] -> ShowSummary
    + {get(Res.Fairies) > 0} [Continue] ->->

=== DoSettlementEmpire
    ~ epicness = get(Res.Fruit) / 10
    {
        - get(Res.Fairies) < 200:
            Your tiny population prevents your colony from reaching it's potential. <>
            ~ epicness -= 7
        - get(Res.Fairies) < 500:
            Your very small numbers result in significant setbacks developing and growing the colony. <>
            ~ epicness -= 5
        - get(Res.Fairies) < 1000:
            Your smaller numbers hold back the colony's development. <>
            ~ epicness -= 3
        - get(Res.Fairies) < 1500:
            Your reduced numbers makes it more difficult for your fairy colony to flourish. <>
            ~ epicness -= 2
        - get(Res.Fairies) < 1800:
            Your reduced numbers slightly hinder your colony's growth. <>
            ~ epicness -= 1
    }

    {
        - get(Res.Fruit) < 20:
            Your tiny feytree orchard makes survival your colony's primary goal.
        - get(Res.Fruit) < 40:
            Your very small feytree orchard means your colony is mostly focused on its basic needs.
        - get(Res.Fruit) < 60:
            Your limited feytree orchard means there is enough food to go around but none is ever traded and some fairies get more than others.
        - get(Res.Fruit) < 80:
            Your ample feytree orchard means all eat well.
        - get(Res.Fruit) <= 100:
            Your excellent abundance of fruit means you have many great feasts and sometimes give berries to your friends.
        - else:
            Your incredible abundance of feyberry fruit enables many feasts, celebrations and donations to friends.
            ~ epicness += 2
    }

    {
        - epicness < 2:
            The dream of this fairy colony is short lived. The attempt to build a colony failed and what fairies were left quickly became disillusioned with their fate.
            The hope for a stable, long-lasting new home fails and before too long the colony completely disbands.
            ~ RandLoss(Res.Fairies, 10000, 99999)
        - epicness < 5:
            The colony mostly keeps to itself. The world never learns about the colony other than a few lucky fey that happen upon it.
            The colony survives but never truly flourishes and the fairies always dream of the better life they had to leave behind.
            Some art and music is created by the colony but it's only enjoyed by few.
        - epicness < 8:
            The colony is stable and takes care of itself. A strong hierarchy appears as some fairies become more powerful and "important" than others.
            Over time some fairies venture out to create new settlements but they are small and don't expand far.
            The colony creates some beautiful works of art, music and writing which are enjoyed by fairy and fey alike. Most fairies say the life they live in this new land is neither better nor worse than in the old land, only different.
        - epicness < 10:
            The colony is very prosperous and grows in strengh and identity.
            Most fairies lead either very impactful and appreciated lives for the colony or they create beautiful works of art which are enjoyed by fey all over the land.
            Eventually the life left behind is forgotten.
        - else:
            The colony thrives to such an amazing degree that none miss the old life they had to leave behind.
            So much amazing art, music and writing are created that creatures come for far and wide to enjoy the delights of the colony.
    }

    + {get(Res.Fairies) <= 0} [Oh no, the colony is gone!] -> ShowSummary
    + {get(Res.Fairies) > 0} [Continue] ->->

=== DoSettlementQoL
    Fairies are happiest in large numbers with many shiny trinkets to play with. Numbers bring about a strong sense of safety and community.

    ~ qol = (get(Res.Fairies) / 200) * (get(Res.Trinkets) / 10)

    {
        - qol <= 20:
            Your colony is very unhappy. Jealousy, pettiness and greed rule the day with many fairies becoming surly and mean.
            Over time the colony turns into a "dark fey" colony befriending the spiders, snakes, and tricksters of the land. The culture created by the colony brings melancholy to those that consume it and all that know your fairies feel great despair.
        - qol <= 40:
            The colony is a nervous one with many fairies feeling paranoid that they could be eaten or have their meager possessions taken from them.
            Trinkets are horded and often stolen. Their value is mostly lost as guarding them becomes more important than playing with them.
        - qol <= 60:
            The colony lives an honest life with most feeling safe in their new home. Some fairies are discontent but most go about their day playing game with trinkets, befriending visiting animals and leading decent lives.
            A few fairies thrive in this environment and are known as the "greatest of us", they are the ones creating the best art and the most fun games. Most live their lives a little bit envious of them.
        - qol <= 80:
            The colony is a very happy one. Most fairies have found their lives fulfilling and most spend their time doing what brings them the most satisfaction.
            Many create art, many farm the feyberry trees, some simply play with their animal friends for most of the days.
        - else:
            The colony is a thriving paragon of happiness and creative satisfaction. Every fairy has found their true joys in life and spend most of their energy pursuing them.
            Maintenance tasks are turned into fun games.
            The fairies all call each other "friend" and conflict inside the colony is unheard of.
    }

    + {get(Res.Fairies) <= 0} [Oh no, the colony is gone!] -> ShowSummary
    + {get(Res.Fairies) > 0} [Continue] ->->

=== ShowSummary
    ~ showSummaryAndEndGame()
    Population:$t$t$t{Score(get(Res.Fairies), 5)}
    Colony:$t$t$t{Score(epicness, 100)}
    Quality of Life:$t$t{Score(qol, 100)}
    Trinkets:$t$t$t{Score(get(Res.Trinkets), 2)}
    Feyberries:$t$t$t{Score(get(Res.Fruit), 2)}
    Fairy Dust:$t$t$t{Score(get(Res.Dust), 2)}
    Animals:$t$t$t{Score(animals, 30)}
    Plants:$t$t$t{Score(plants, 30)}
    Magic:$t$t$t{Score(magic, 30)}
    Beauty:$t$t$t{Score(beauty, 30)}
    $t
    Total Score:$t$t$t{score}
    + [Continue]
    - -> DONE
