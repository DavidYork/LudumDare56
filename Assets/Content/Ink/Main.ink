INCLUDE Globals.ink
INCLUDE Exploration.ink
INCLUDE Encounters.ink
INCLUDE Locations.ink

//----------------------------------------------------------------------------------------------------------------------
=== Start
    You have arrived in the new world. What do you do?

    + [Onward!]
        ~ chooseMapDestination()
        -> DONE
