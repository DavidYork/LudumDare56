=== GainStory
    { shuffle:
    - -> GainAnimalStory ->
    - -> GainPlantStory ->
    - -> GainMagicStory ->
    - -> GainBeautyStory ->
    }

    ->->

= GainAnimalStory
    ~ gainStory(Story.Animal)
    { shuffle:
        - You hear a wonderful story about the animals of the land
    }
    ->->

= GainPlantStory
    ~ gainStory(Story.Plant)
    { shuffle:
        - You hear a wonderful story about the plants of the land
    }
    ->->

= GainMagicStory
    ~ gainStory(Story.Magic)
    { shuffle:
        - You hear a wonderful story about the magic of the land
    }
    ->->

= GainBeautyStory
    ~ gainStory(Story.Beauty)
    { shuffle:
        - You hear a wonderful story about the beauty of the land
    }
    ->->
