INCLUDE Encounters.ink
INCLUDE Exploration.ink
INCLUDE Locations.ink
INCLUDE Settling.ink

//- Interop ------------------------------------------------------------------------------------------------------------
EXTERNAL chooseMapDestination       ()
EXTERNAL showSummaryAndEndGame      ()
EXTERNAL gain                       (resource, amount)
EXTERNAL get                        (resource)
EXTERNAL lose                       (resource, amount)

//- Lists --------------------------------------------------------------------------------------------------------------
LIST Res                            =   Coins,
                                        Dust,
                                        Fairies,
                                        Fruit,
                                        Trinkets

//----------------------------------------------------------------------------------------------------------------------
=== Start
    You are the leader of the fairy people.
    You and 2000 fairies have fled your homes as large, clumsy beings cut down the forests and destroyed your land.

    + [Continue]
    -

    You have brought with you meager resources that can be used to build a new colony in this new, unknown land.
    Hover the mouse over the resources (top of the screen) to learn more about them.

    + [Continue]
    -

    Remember! Fairies like to collect stories, so if you find those that can tell you stories about this new land you will find an easier time making a home here.

    Good luck!

    + [Onward to a new home!]
        ~ chooseMapDestination()
        -> DONE

//- Functions ----------------------------------------------------------------------------------------------------------
=== function RandLoss(resource, min, max)
    ~ temp amount = RANDOM(min, max)
    ~ temp startAmount = get(resource)
    { amount > startAmount:
        ~ amount = startAmount
    }
    ~ lose(resource, amount)
    ~ return amount

=== function Score(amount, multiplier)
    ~ score += (amount * multiplier)
    ~ return "{amount} x {multiplier} = {amount * multiplier}"
