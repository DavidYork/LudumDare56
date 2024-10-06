//----------------------------------------------------------------------------------------------------------------------
// Quality of location
VAR animals = 0
VAR plants = 0
VAR magic = 0
VAR beauty = 0

// Names of things used for settling
VAR animalsName = "animals"
VAR plantsName = "plants"

//----------------------------------------------------------------------------------------------------------------------
=== Location_Well
    ~ animalsName = "blue birds"
    ~ plantsName = "plants"

    Hello. This is the well.
    <- Show_Animals
    <- Show_Plants
    <- Show_Magic
    <- Show_Beauty
    <- SettlementOptions
    - -> DONE

= Show_Animals
    There are many little blue birds that like to visit the well. <>
    { animals:
    - 0: They are mean and cruel and snap at your fairies!
    - 1: They don't like you very much and sometimes chase your fairies away.
    - 2: They mostly ignore you.
    - 3: They are friendly and fun to chase.
    - else: They are so wonderful and many of your fairies are making friends with them!
    }
    -> DONE

= Show_Plants
    { plants:
    - 0: The only plants are some nasty weeds and smelly well algae.
        ~ plantsName = "weeds"
    - 1: The only plants are just a bunch of weeds and a few small flowers.
        ~ plantsName = "weeds"
    - 2:
        The well is surrounded by green grass and a few nice flowers.
        ~ plantsName = "flowers"
    - 3:
        There are lovely colorful flowers around the well.
        ~ plantsName = "flowers"
    - else: The well is surrounded by beautiful purple and pink dream lilies and your fairies love them!
    }
    -> DONE

= Show_Magic
    { magic:
    - 0: There is no magic energy here.
    - 1: Echoes of the wishes of travelers that tossed coins into the well linger as a faint mystic energy.
    - 2: The well has an aura of magic around it, no doubt created by travelers making wishes here.
    - 3: This well has been the site of a blessed ritual once. Your fairies are pleased by its energy.
    - else: The well is enchanted and filled with wonderful magic energy!
    }
    -> DONE

= Show_Beauty
    { beauty:
    - 0: The well is ugly and its site displeases your fairies.
    - 1: The well isn't a complete eyesore but it looks very boring.
    - 2: The well looks fine but flying over it you have a wonderful view of the surrounding area.
    - 3: The well has a view of beautiful mountains and a wonderful forest in the distance.
    - else: Your fairies love the beauty of the surrounding area and keep talking about it excitedly.
    }
    -> DONE
