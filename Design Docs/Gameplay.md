# Gameplay
Description of the game mechanics.

## Overview
Heavily inspired by Seedship this game gives you a resource to protect, and end goal of the "best outcome for the resource", and two other resources that influence the outcome. The natural progress of the world gives you advantages towards your final home but also takes the resources from you.

## Resources
- **Fairies**. You have a ton of fairies. You're leading them as the kind of the fairies. These you want to protect but they die over time.
- **Fairy dust**. You need this to construct your new magic land. Or maybe to hide your new magic land from predators.
- **Feyfruit**. A perishable resource that determines quality of life perhaps.
- **Gold coins**. You need this to bypass various encounters and see future events about a location if considering colonizing it
- **Stories**. You learn stories and secrets of the world and these stories and secrets "upgrade" locations.

## Game loop
This is the game loop.

1. Go to a location
2. Have an encounter at that location
   1. Encounter gives choices to (usually) drain resources
   2. Encounter maybe can be skipped by spending coins
   3. Encounter has a chance of giving you a **story** which upgrades locations
3. Interact with the location
   1. Learn how good it is for settling down
   2. Take one of these actions:
      1. Settle there.
         1. Spend a coin, see the future events (good and bad)
         2. Decide to commit to settling (game ends) or abort (take different action)
      2. Explore more. Go to #2
      3. Move on. Go to #1

## Strategy
Explore a lot. Push your luck. More exploration means more resource drain but also more stories so you have better locations but you're "more fucked" when you're there. Don't let all the fairies die.

# Details

## Locations
Locations have 4 components. They have numeric values (1-4) based on how good they are. The stories give bonuses to these values (+1 per story for the relevant attribute).

Example: Stories about animals. An area could have squirrels. They look like this:

- **Bad** squirrels are territorial, fighting over the trees and scaring the fairies.
- **Uninteresting** either mention nothing or state there are squirrels buy they seem to mind their own business.
- **Good** squirrel friends! Give them nuts and hang with them in their squirrel homes.

### Location attributes
These are the location attributes:
- **Animals**
- **Plants**
- **Magic**
- **Beauty**

## Encounters
Each location will have a series of scripted encounters which can be either repeated or not depending on design. The encounters are specifically authored to the location. Basically "exploring the forest/ruins/bridge/mountains/grove/whatever" has a collection of encounters authored to each place (and maybe some reusable location-independent ones) and the player makes choices about the encounters which almost always result in spending resources, but sometimes give them a story.

## Stories
These are numeric upgrades to the location attributes.

## Resources
- **Fairies** - these are what you protect. The more = the more successful civilization and the greater impact they have on the world
- **Trinkets** - These determine the happiness of the fairies long-term.
- **Feyberries** - These determine the health and "civilization infrastructure" the fairies get.
- **Fairy Dust** - These are used to make a location safe. They heavily minimize the negative impacts of the bad *future events* and bad aspects about the colonized lands.
- **Coins** - These are used both to "get out of jail free" for encounters and are used in a magic ritual to see the future right before settling in a place. This tells you about the *future events* in the location. You get one chance to decide whether to commit to settling or run away.

## Settling and Objective / win condition
When settling a location you already have a sense of whether the 4 attributes are good or bad. You then consume a coin (if you have one) and learn about the *future events* that will occur. They are random and will be either good or bad. You then decide whether or not to settle. If you settle then the game ends and the quality of the location and quantity of non-coin resources determines your epilogue and score.

Bad aspects of a location, bad future events and low fairy dust can result in fairy death.

### Future events
Just short descriptions of things that will happen in the future at a location if the player settles there. Civil disturbance, floods, storms, fires, beasts, generous visitors, happy festival, etc. They are completely reusable for different locations.

### Score
Score is just a number representing how good or bad the description was in absolute terms.
