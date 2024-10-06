//----------------------------------------------------------------------------------------------------------------------
// Quality of location
VAR animals = 0
VAR plants = 0
VAR magic = 0
VAR beauty = 0

// Names of things used for settling
VAR animalName = "animal"
VAR plantName = "plant"
VAR badPlantName = "plant"
VAR locationName = "location"
VAR locationDescription = ""

//----------------------------------------------------------------------------------------------------------------------
=== Location_Well
    {locationDescription}
    + [Look around]

    <- Show_Animals
    <- Show_Plants
    <- Show_Magic
    <- Show_Beauty
    <- SettlementOptions
    - -> DONE

= Show_Animals
    There are many little {animalName}s that like to visit the well. <>
    {
    - animals <= 0: They are mean and cruel and snap at your fairies.
    - animals == 1: They don't like you very much and sometimes chase your fairies away.
    - animals == 2: They mostly ignore you.
    - animals == 3: They are friendly and fun to chase.
    - else: They are so wonderful and many of your fairies are making friends with them.
    }
    -> DONE

= Show_Plants
    {
    - plants <= 0: The only plants are some nasty {badPlantName}s and smelly well algae.
    - plants == 1: The only plants are just a bunch of {badPlantName}s and a few small flowers.
    - plants == 2:
        The well is surrounded by green grass and a few nice {plantName}s.
    - plants == 3: There are lovely colorful {plantName}s around the well.
    - else: The well is surrounded by beautiful purple and pink dream lilies and your fairies love them.
    }
    -> DONE

= Show_Magic
    {
    - magic <= 0: There is no magic energy here.
    - magic == 1: Echoes of the wishes of travelers that tossed coins into the well linger as a faint mystic energy.
    - magic == 2: The well has an aura of magic around it, no doubt created by travelers making wishes here.
    - magic == 3: This well has been the site of a blessed ritual once. Your fairies are pleased by its energy.
    - else: The well is enchanted and filled with wonderful magic energy.
    }
    -> DONE

= Show_Beauty
    {
    - beauty <= 0: The well is ugly and its site displeases your fairies.
    - beauty == 1: The well isn't a complete eyesore but it looks very boring.
    - beauty == 2: The well looks uninteresting but flying over it you have a wonderful view of the surrounding area.
    - beauty == 3: The well has a view of beautiful mountains and a wonderful forest in the distance.
    - else: Your fairies love the beauty of the surrounding area and keep talking about it excitedly.
    }
    -> DONE

//----------------------------------------------------------------------------------------------------------------------
=== Location_Tree
    {locationDescription}
    + [Look around]

    <- Show_Animals
    <- Show_Plants
    <- Show_Magic
    <- Show_Beauty
    <- SettlementOptions
    - -> DONE

= Show_Animals
    {
    - animals <= 0: Casually sitting on a branch you see a {animalName}. It glares at you menancingly and when one of your fairies approaches it chases the fairy away.
    - animals == 1: Sitting on a branch you see a {animalName}. It stares at you but doesn't move. When a fairy approaches and says "hello" it makes a strange sound and flees.
    - animals == 2: Amidst the branches and leaves you see a {animalName}. Some fairies approach and introduce themselves but it seems uninterested.
    - animals >= 3: You see a pair of {animalName}s. At first they seem skittish but eventually the allow a fairy to approach. Before long they seemed to be friends.
    }
    -> DONE

= Show_Plants
    {
    - plants <= 0: {locationName} sits amongst thorny bushes, sticky spiderwebs, and lots of {badPlantName}s. Some of your fairies cut themselves on the plantlife.
    - plants == 1: {locationName} is covered in slimy algae and surrounded by {badPlantName}s.
    - plants == 2: {locationName} is surrounded by many other trees, {badPlantName}s and {plantName}s. It's a very crowded area.
    - plants >= 3: {locationName} is amidst wonderful and abundant {plantName}s.
    }
    -> DONE

= Show_Magic
    {
    - magic <= 0: All trees should have nature magic but there is an evil energy here that seems to counter it.
    - magic == 1: All trees have nature magic and {locationName} is no exception, although the aura is surprisingly faint.
    - magic == 2: {locationName} pulls nature magic from the surrounding forest, although it's not a great abundance.
    - magic >= 3: {locationName} is humming with magic energy.
    }
    -> DONE

= Show_Beauty
    {
    - beauty <= 0: All the trees around here block the sun with their dark canopy there are scary, twisted shadows everywhere.
    - beauty == 1: A forest should be truly beautiful but this area is filled with dead stumps and slimy algae-filled pools.
    - beauty == 2: The surrounding forest is as beautiful as you would expect such an area to be. Nothing exceptional, but still attractive.
    - beauty >= 3: The fairies love admiring the forest. The trees are so beautiful and the sun casts volumous rays through gaps in the canopy.
    }
    -> DONE

//----------------------------------------------------------------------------------------------------------------------
=== Location_Cave
    {locationDescription}
    + [Look around]

    <- Show_Animals
    <- Show_Plants
    <- Show_Magic
    <- Show_Beauty
    <- SettlementOptions
    - -> DONE

= Show_Animals
    {
    - animals <= 0: Jumping out at some fairies is a dangerous {animalName}. This place is dark and awful.
    - animals == 1: Fairy scouts report many {animalName}s hiding in the shadows. This feels dangerous.
    - animals == 2: Caves are a hard place to make friends, and try though they might the fairies haven't befriended any of the local {animalName}s.
    - animals >= 3: Who says a fairy cannot be friends with a {animalName}? Several have appeared and, as it turns out, they are fun to play with and have great senses of humor.
    }
    -> DONE

= Show_Plants
    {
    - plants <= 0: There is almost no plant life here, only a couple {badPlantName}s. A fairy tried eating one and became violently ill.
    - plants == 1: There is an abundance of smelly, gross {badPlantName}s here.
    - plants == 2: The cave is home to a few {plantName}.
    - plants >= 3: The cave is filled with wonderful {plantName}s.
    }
    -> DONE

= Show_Magic
    {
    - magic <= 0: The magic in this cave is very strong, but it's the wrong kind. It's an evil, dark magic that makes fairy magic almost impossible.
    - magic == 1: There is a little bit of welcome earth magic here, but it's very faint.
    - magic == 2: There is a large presence of both wonderful earth magic and horrible dark magic. If you settle here it will be a lot of effort to wield.
    - magic >= 3: The cave positively glows with the aura of earth magic. What a wonderful energy.
    }
    -> DONE

= Show_Beauty
    {
    - beauty <= 0: {locationName} is dark, dank, and hideously ugly. Caves are no place for a fairy.
    - beauty == 1: {locationName} is dark and hard to admire, but there are some glowing insects that are fun to watch and chase.
    - beauty == 2: {locationName} is made of stone with its interesting stone fingers reaching floor to ceiling, but it's otherwise quite plain.
    - beauty >= 3: {locationName} has a beautiful underground stream filled with glowing swimming life.
    }
    -> DONE

//----------------------------------------------------------------------------------------------------------------------
=== Location_Cliff
    {locationDescription}
    + [Look around]

    <- Show_Animals
    <- Show_Plants
    <- Show_Magic
    <- Show_Beauty
    <- SettlementOptions
    - -> DONE

= Show_Animals
    {
    - animals <= 0: Fairies love to make friends with the local animals, but save for one very territorial {animalName} there aren't any to be found.
    - animals == 1: A family of {animalName}s was found and some fairies approached and said hello. Angry squawks drove the fairies away.
    - animals == 2: There seem to be an abundance of {animalName}s here but they are very wary of you. Perhaps they will become friends in time?
    - animals >= 3: Almost immediately after arriving the fairies found a baby {animalName} and now they are fast friends.
    }
    -> DONE

= Show_Plants
    {
    - plants <= 0: The harsh winds of this cliff make it tough for plants to survive here. Only a few {badPlantName}s can be found.
    - plants == 1: The only plant life your fairies find is {badPlantName}s which is very unfortunate.
    - plants == 2: The area is filled with both {badPlantName}s and {plantName}s. Perhaps you could build a home here?
    - plants >= 3: The fairies are excited to report finding a lot of wonderful {plantName}s.
    }
    -> DONE

= Show_Magic
    {
    - magic <= 0: No magic can be found here in this desolate place.
    - magic == 1: The magic here is very faint.
    - magic == 2: The wind magic here can be seen and felt by all but it isn't as abundant as you would like.
    - magic >= 3: Your fairies detect both wind and earth magic in great quantities here.
    }
    -> DONE

= Show_Beauty
    {
    - beauty == 0: Clouds and fog prevent any kind of interesting view from here, and the cliff itself is nothing to look at.
    - beauty == 1: The view is nice but the cliff is plain and boring.
    - beauty == 2: The cliff is boring but there is a wonderful view of the surrounding area.
    - beauty == 3: The cliff has a wonderful, sweeping view of forests and hills and animals below. Many fairies stop to admire it and make games of counting animals they see below.
    - beauty >= 4: The view is majestic, absolutely wonderful. You can see forever and imagine all the adventures you will have in all of these beautiful lands.
    }
    -> DONE

//----------------------------------------------------------------------------------------------------------------------
// === Location_Bridge
//     {locationDescription}
//     + [Look around]

//     <- Show_Animals
//     <- Show_Plants
//     <- Show_Magic
//     <- Show_Beauty
//     <- SettlementOptions
//     - -> DONE

// = Show_Animals
//     {
//     - animals <= 0: .
//     - animals == 1: .
//     - animals == 2: .
//     - animals >= 3: .
//     }
//     -> DONE

// = Show_Plants
//     {
//     - plants <= 0: .
//     - plants == 1: .
//     - plants == 2: .
//     - plants >= 3: .
//     }
//     -> DONE

// = Show_Magic
//     {
//     - magic <= 0: .
//     - magic == 1: .
//     - magic == 2: .
//     - magic >= 3: .
//     }
//     -> DONE

// = Show_Beauty
//     {
//     - beauty <= 0: .
//     - beauty == 1: .
//     - beauty == 2: .
//     - beauty >= 3: .
//     }
//     -> DONE

//----------------------------------------------------------------------------------------------------------------------
=== Location_Ruins
    {locationDescription}
    + [Look around]

    <- Show_Animals
    <- Show_Plants
    <- Show_Magic
    <- Show_Beauty
    <- SettlementOptions
    - -> DONE

= Show_Animals
    {
    - animals <= 0: As you explore {locationName} several angry {animalName}s jump and and chase you away.
    - animals == 1: Your scounts have discovered very large and scary {animalName}s here.
    - animals == 2: There are a few {animalName}s here, but who knows if they are friendly?
    - animals >= 3: As you explore {locationName} a {animalName} wanders up to you and wants to play. Hooray a friend.
    }
    -> DONE

= Show_Plants
    {
    - plants <= 0: This place is hard to explore as {badPlantName}s are everywhere blocking your path.
    - plants == 1: This place doesn't have many interesting plants, just some {badPlantName}s around.
    - plants == 2: The area is filled with both {badPlantName}s and {plantName}s. Perhaps you could build a home here?
    - plants >= 3: The area is surrounded by wonderful {plantName}s.
    }
    -> DONE

= Show_Magic
    {
    - magic <= 0: This place has strong echoes of darkness. Something horrible happened here long ago. Fey magic cannot come to this land.
    - magic == 1: This place has faint traces of fey magic but they are fleeting.
    - magic == 2: There is an aura of magic energy here, but it is erratic and unpredictable.
    - magic == 3: The magic here is very strong. Many echoes of fantastic past events resonate in the air.
    - else: The feeling of magic energy here is overwhelming. It makes your hair stand up just being in its presence.
    }
    -> DONE

= Show_Beauty
    {
    - beauty <= 0: Ugly broken buildings that remind you of the ones that pushed you from your homeland.
    - beauty == 1: Everything here is broken and decaying, and fairies do not like to be reminded of death. But nature is slowing taking it back.
    - beauty == 2: The surrounding area is very beautiful with views of mountains and trees.
    - beauty >= 3: This area is so interesting to look at. Who made it? Where did they go? What is their story? The fairies make many games of this.
    }
    -> DONE
