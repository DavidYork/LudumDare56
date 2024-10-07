VAR encounterName = ""

//----------------------------------------------------------------------------------------------------------------------
// Well
//----------------------------------------------------------------------------------------------------------------------
=== Encounter_Well_A_Voice_In_The_Well
    You approach the well curious about what it may hide.

    "WHO GOES THERE AND WHAT HAVE YOU FOR ME?!" a voice echoes from below.

    + [Tell the voice who you are]
        "I CARE NOT FOR YOU FEY CREATURE!"

        ++ [Tell the voice fun stories about fairy life] -> FeyStories
        ++ [Offer the voice a gift as an apology] -> Gift
        ++ [Leave the voice alone] -> Nope

    + [Apologize for disturbing the voice] -> Grovel
    + [Offer the voice a gift as an apology] -> Gift
    + [Demand the voice apologize to you] -> Demand
    + [Leave the voice alone] -> Nope

= Nope
    You decide it's time to leave the voice alone.
    -> Finish

= Happy
    Satisfied with this encounter you decide it's time to go.
    -> Finish

= Gift
    "A GIFT YOU SAY? I LIKE GIFTS!"

    + {get(Res.Fruit) > 0} [Toss in a feyberry (-1 feyberry)]
        ~ lose(Res.Fruit, 1)
        Yuck! I hate these things! Go away!
        ++ [Leave the voice alone] -> Nope

    + {get(Res.Coins) > 0} [Toss in a coin (-1 coin)]
        ~ lose(Res.Coins, 1)
        { shuffle:
        - The voice becomes silent -> Nope
        - "Another coin, how original!? I hope you made a wish!"
            And with that the voice goes silent.
            -> Nope
        - "Well, if you are going to be so generous I guess I can repay the favor. Let me tell you a story."
            -> GainStory ->
            -> Happy
        }

    + {get(Res.Trinkets) > 0} [Toss in a trinket (-1 trinkey)]
        ~ lose(Res.Trinkets, 1)
        { shuffle:
        - "Ugh, you throw in trash! Be off with you!" With that the voice goes silent. -> Nope
        - You hear nothing more from the voice. -> Nope
        }

    + [Nevermind. Leave the voice alone] -> Nope

= Grovel
    -> DONE

= Demand
    { shuffle:
        - "How DARE you! Insolent fey!"
            -> Sludge
    }
    -> DONE

= FeyStories
    You recount several fun stories about fairy games, tricking cats into falling off surfaces and other exciting things.
    { shuffle:
        - Oooh, what wonderful stories! Thank you for sharing. You must let me tell you a story of my own.
            -> GainStory ->
            -> Happy
        - "ARGH! WHY are you so TEDIOUS! BEGONE!""
            -> Nope
        - -> Sludge
    }

= Sludge
    A furious roar erupts from the well followed by a torrent of hot, black sludge being shot into the air!
    { RandLoss(Res.Fairies, 2, 5) } fairies are burned to death by the disgusting liquid.
    -> Nope

//----------------------------------------------------------------------------------------------------------------------
=== Encounter_Well_A_Lovers_Well
    You approach a well covered in markings. On closer inspection many of them are the initials of lovers etched into the stone, often surrounded by a heart.
    + [Ignore the well] -> DONE
    + [Toss in a coin]
        ~ lose(Res.Coins, 1)
        -> Resolve
    + [Toss in a trinket]
        ~ lose(Res.Trinkets, 1)
        -> Resolve
    + [Toss in a feyberry]
        ~ lose(Res.Fruit, 1)
        -> Resolve
    + [Etch the initials of two fairies in love] -> Resolve

= Resolve
    { shuffle:
    - You do this but nothing happens.
    - A burbling sound comes up, but then silence.
    - A mass of butterflies fly up from the well.
    - A bat flies out of the well.
    }
    - -> Finish


//----------------------------------------------------------------------------------------------------------------------
=== Encounter_Well_A_Boarded_Up_Well
    You approach a well that has been boarded up. There is a wood covering over the top and a metal bar to hold it in place.
    In fact it's even locked with a padlock!

    - (top)
    + [Pick the lock and open it!] You try and try but the lock is too difficult to pick. -> top
    + [Break it open!] You try and try and try but you are tiny creatures, strength is not your strong suit. -> top
    + {get(Res.Dust) > 0} [Sprinkle a few pinches of fairy dust onto it (-3 fairy dust)]
        ~ lose(Res.Dust, 3)
        The lock vanishes!
        -> OpenWell
    + [Leave it alone and explore the area] -> DONE

= OpenWell
    { shuffle:
    - A deep, thunderous boom comes from inside the the well. Then the clouds begin to darken and you hear thunder in the distance.
        You flee the well but soon a swarm of massive insects arrive and attack your fairies! { RandLoss(Res.Fairies, 3, 30)} perish!
        What have you done? -> Finish
    - You open the well but there is nothing inside other than a nasty stench. -> Finish
    - You open the well and it's completely dry but there is a skeleton of a very large animal inside. -> Finish
    - -> GainFairy
    }

= GainFairy
    A fairy flies out of the well. "Thank you for saving me!" the tiny voice squeaks.
    Happily your new friend accepts your invitation to join the colony. Is is fairy custom an exchange of stories takes place.
    ~ gain(Res.Fairies, 1)
    -> GainStory ->
    -> Finish

//----------------------------------------------------------------------------------------------------------------------
// Tree
//----------------------------------------------------------------------------------------------------------------------
=== Encounter_Tree_Giant_Boulder
    As you pass through the forest you discover a massive boulder in the middle of a grove. It is quiet and nothing large grows near it.

    + [Inspect the boulder]
        { shuffle:
        - It's a nondescript boulder. The fairies get bored. -> Finish
        - The boulder hums with a magic energy. What type of magic is unclear.
            ++ {get(Res.Coins) > 0} [Perform a divination ritual to learn more (-1 coin)] -> Ritual
        - The boulder provides shade for a friendly chipmunk. She plays with one of the fairies for a bit before going on her way. -> Finish
        }
    + [Ignore it and explore the area] -> DONE

= Ritual
    33 fairies sit in a circle and chant words of ancient fey magic. The coin disappears and the fairies go into a deep trance.
    {shuffle:
    - One speaks "this is a blessed stone, placed here by those that love the forest. It will bring fortune to a colony should we choose to found one nearby."
        ~ plants += 2
        ~ magic += 2
    - One speaks "this stone traps a vile, evil creature. This place is cursed, we should leave."
        ~ magic -= 2
        ~ plants -= 2
    - One speaks "this stone has shared its wisdow. It has sat here for so long and seen so many things."
        With newfound knowledge of the area your fairies discover a hidden cache of {RandGain(Res.Trinkets, 5, 10)} trinkets buried nearby!
    - A look of horror covers the face of one of the fairies. "NO! This stone is evil!"
        Dark, shadowy hands reach up from the ground and start grabbing the fairies and pulling them under the earth.
        {RandLoss(Res.Fairies, 2, 33)} fairies perish.
    }
    - -> Finish

//----------------------------------------------------------------------------------------------------------------------
// Ruins
//----------------------------------------------------------------------------------------------------------------------
=== Encounter_Ruins_Old_Battlefield
    As you approach the {locationName} you discover the remains of an old battleground. Rusted remains of weapons and armor lie around you.
    + [Look for trinkets]
        { shuffle:
        - Your fairies look around but find nothing interesting.
        - Your fairies look around and discover {RandGain(Res.Trinkets, 2, 8)} trinkets!
        - Your fairies look around and discover a mouse playing with a shiny arrowhead. -> Mouse
        }
        - {RANDOM(0, 3) == 0} While looking around an a fairy discovers an angry snake as is immediately devoured!
            ~ lose(Res.Fairies, 1)
            -> Finish
        - {RANDOM(0, 3) == 0} While looking around your fairies stumble upon a wolf! Chaos ensues and {RandLoss(Res.Fairies, 3, 20)} fairies are lost.
            -> Finish
        - -> Finish
    + [Ignore the battlefield]
        -> Finish

= Mouse
    + [Offer to trade the arrowhead for a story]
        { shuffle:
            - "No thank you" says the mouse and it scurries away. -> Finish
            - "Sure thing, I love stories!" The mouse gives you the arrowhead and you proceed to tell him a story.
                ~ gain (Res.Trinkets, 1)
                -> MaybeStory
        }
    + [Offer to trade the arrowhead for a feyberry] Ooh, I love berries! The mouse agrees to the trade. -> MaybeStory

= MaybeStory
    + [Care to tell us a story?]
        { shuffle:
        - "No" says the mouse. "I gave you the arrow head, we're even." He scurries away.
        - "Okay" says the mouse. -> GainStory ->
        }
        - -> Finish

//----------------------------------------------------------------------------------------------------------------------
// Cave
//----------------------------------------------------------------------------------------------------------------------
// === Encounter_Cave_

//----------------------------------------------------------------------------------------------------------------------
// Cliff
//----------------------------------------------------------------------------------------------------------------------
// === Encounter_Cliff_

//----------------------------------------------------------------------------------------------------------------------
// Bridge
//----------------------------------------------------------------------------------------------------------------------
// === Encounter_Bridge_

//----------------------------------------------------------------------------------------------------------------------
// General
//----------------------------------------------------------------------------------------------------------------------
=== Encounter_General_A_Feast_for_Valor
As you approach the {locationName} you discover a feast is going an around it. There are so many visitors - rabbits wearing their best suits, toads in tophats and mice wearing nice coats.

+ [Introduce yourselves] -> Introduction
+ [Leave the feast alone and explore the area] You decide this is not productive to founding a colony. -> DONE

= Introduction
    You walk up to the feast and introduce yourself
    -
    { shuffle:
    - The feast fades away. It was all an illusion! You scramble to escape whatever trap this is but { RandLoss(Res.Fairies, 10, 20) } are never seen again. -> Finish
    - A very self-impressed rabbit walks up to you. "You are not welcome here! Begone with you."
        Disappointed you leave the feast. -> Finish
    - You are welcome to join the feast. Your fairies have a great time feasting with their new friends.
        ++ {get(Res.Fruit) >= 20} [Donate some feyberries (-20 feyberries)]
            ~ lose(Res.Fruit, 20)
            -> Berries
        ++ [Thank them and be on your way] You thank them for the wonder time and go on your way.
            +++ [Explore the area] -> Finish
    }
    -> DONE

= Berries
    The guests are excited to try the feyberries. <>
    { shuffle:
    - They love them and start behaving very strangely.
        Before long they start greedily staring at your pouches asking more and more questions about how many berries you have left.
        { shuffle:
        - You decide to escape while you still have the chance. -> Finish
        - You try to escape before things get out of control but chaos ensues! They try to steal your berries!
            ++ [On no!] -> Badness
        }
    - They love them and start getting happy and nostalgic. One of them offers to tell you a story.
        ++ [Listen to the story] -> GainStory ->
            -> Finish
    }
    -> DONE

= Badness
    { shuffle:
    - They manage to steal { RandLoss(Res.Fruit, 5, 30)} berries before you can escape!
    - Things get very violent and { RandLoss(Res.Fairies, 5, 30)} fairies are lost in the struggle!
    - You barely escape! It seems not everybody can handle their feyberries.
    }
    - -> Finish

//----------------------------------------------------------------------------------------------------------------------
=== Encounter_General_Wandering_Fox
{ shuffle:
    - Approaching the area you run into a fox. It pops out of a hole in the ground right in the middle of your fairies.
        + [Foxes are dangerous. Run away!]
            { shuffle:
            - -> Escape
            - -> FoxAttack
            }
        + {get(Res.Fruit) >= 10} [Throw food at the fox and run away! -10 feyberries]
            {
            - RANDOM(0, 2) == 0: -> FoxChomp
            - else: -> Escape
            }

    - Approaching the area you see a fox nearby. It doesn't seem to have noticed you. Foxes are very dangerous and known to eat fairies.
        + [Foxes are dangerous. Sneak around it]
            { shuffle:
                - You sneak past it safely. -> Finish
                - The fox notices you and changes right for the nearest fairies! -> FoxAttack
            }
        + [Leave some food so it won't follow us and sneak away]
            { shuffle:
            - You sneak past it safely. -> Finish
            - The fox notices you and the berries but decides the berries are less trouble to eat. -> Escape
            - The fox notices you and changes right for the nearest fairies! -> FoxAttack
            }
        + [Send two fairies to lure him away while the rest escape]
            {
            - RANDOM(0, 3) == 0: The distraction is successful and all the fairies escape. -> Finish
            - RANDOM(0, 3) == 0: The fox is quick and leaps into the air chomping one of the fairies.
                ~ lose(Res.Fairies, 1)
                -> FoxAttack
            - else: The distraction is successful but the two fairies were never seen again.
                ~ lose(Res.Fairies, 2)
                -> Finish
            }
    - As you approach the area a fox leaps out at you and charges your fairies! -> FoxAttack
}

= FoxAttack
    The fox leaps into the fray <>
    { shuffle:
        - but the fairies are too fast and manage to escape! What luck. -> Finish
        - and catches and kills {RandLoss(Res.Fairies, 3, 30)} fairies before the rest can escape. -> Finish
    }

= FoxChomp
    The fox catches up with the fairies and manages to catch and kill {RandLoss(Res.Fairies, 3, 30)} fairies before the rest escape. -> Finish

= Escape
    What a relief, you managed to get away! -> Finish

//----------------------------------------------------------------------------------------------------------------------
// === Encounter_General_Wandering_Wolves

// //----------------------------------------------------------------------------------------------------------------------
// === Encounter_General_Family_of_Rats

// //----------------------------------------------------------------------------------------------------------------------
// === Encounter_General_A_Murder_of_Crows

// //----------------------------------------------------------------------------------------------------------------------
// === Encounter_General_A_Hungry_Hawk

// //----------------------------------------------------------------------------------------------------------------------
// === Encounter_General_The_Angry_Boar

// //----------------------------------------------------------------------------------------------------------------------
// === Encounter_General_The_Squirrel_Race

// //----------------------------------------------------------------------------------------------------------------------
// === Encounter_General_The_Great_Insulter

// //----------------------------------------------------------------------------------------------------------------------
// === Encounter_General_The_Hungry_Frog

// //----------------------------------------------------------------------------------------------------------------------
// === Encounter_General_The_Vengeful_Spirit




//----------------------------------------------------------------------------------------------------------------------
// Utilities
//----------------------------------------------------------------------------------------------------------------------
=== Finish
    + [Explore the area] -> DONE
