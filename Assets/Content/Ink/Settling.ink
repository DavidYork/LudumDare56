//----------------------------------------------------------------------------------------------------------------------
=== SettlementOptions
    + [Consider settling here] -> MaybeSettleHere
    + [Move on] -> DONE

= MaybeSettleHere
    Settling is a big decision! If you decide this is where the colony should live it will end the game.

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

    { animals:
    - 0: Violent conflicts with the {animalsName} kill {RandLoss(Res.Fairies, 250, 500)} fairies!
    - 1: Tensions grow with the {animalsName} until a bloody conflict insues off. The {animalsName} are driven away but {RandLoss(Res.Fairies, 50, 150)} fairies perish in the conflict.
    - 2: Unable to make friends with the {animalsName} occasional fights break out killing {RandLoss(Res.Fairies, 5, 49)} fairies decide the area isn't attractive enough for them and leave the colony.
    - else: Many fairies make long-lasting friendships with the {animalsName}.
    }

    { plants:
    - 0: The very poor {plantsName} of the area makes it hard to plant the feyberries to grow food for the fairies. {RandLoss(Res.Fruit, 25, 50)} feyberries fail to grow into feyberry trees.
    - 1: The {plantsName} make it difficult to grow feyberry trees. {RandLoss(Res.Fruit, 10, 24)} feyberries fail to grow into feyberry trees.
    - 2: The farmers plant the feyberries and they grow into feyberry trees, but {RandLoss(Res.Fruit, 2, 9)} of them die.
    - else: The feyberries are planted amidst the {plantsName} and grow into large, healthy feyberry trees yielding delicious fruit.
    }

    { magic:
    - 0: Fairy colonies need a lot of magic to thrive and this location has none, so {RandLoss(Res.Dust, 40, 75)} pinches of magic dust were used to enchant the area.
    - 1: The magic of the area needs strengthening so {RandLoss(Res.Dust, 20, 39)} pinches of fairy dust were used.
    - 2: The magic of the area is good but not quite strong enough so {RandLoss(Res.Dust, 10, 19)} pinches of fairy dust were sprinkled on the ground.
    - else: The strong magic of the area strengthens the building of the fairy homes.
    }

    { beauty:
    - 0: Disliking the ugliness of the area {RandLoss(Res.Fairies, 150, 300)} fairies are disheartened and leave the area.
    - 1: Finding the area too ugly for them {RandLoss(Res.Fairies, 50, 99)} fairies leave to find a better home.
    - 2: {RandLoss(Res.Fairies, 10, 49)} fairies decide the area isn't attractive enough for them and leave the colony.
    - else: Often fairies take time out of their day to admire the beauty of this location.
    }

    + [Continue] ->->

=== DoSettlementEmpire
    Then empire
    + [Continue] ->->

=== DoSettlementQoL
    Then QoL
    + [Continue] ->->

=== ShowSummary
    ~ showSummaryAndEndGame()
    Then summary
    + [Continue]
    - -> DONE
