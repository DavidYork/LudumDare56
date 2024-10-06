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
    You have arrived in the new world. What do you do?

    + [Onward!]
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