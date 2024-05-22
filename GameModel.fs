namespace GentleGame

open WebSharper

[<JavaScript>]
module GameModel =

    type Player = {
        Name: string
        Health: int
        Inventory: string list
    }

    type GameState = {
        Location: string
        Description: string
        Options: (string * string) list
    }

    type Game = {
        Player: Player
        State: GameState
    }

    let initialPlayer = {
        Name = "Adventurer"
        Health = 100
        Inventory = []
    }

    let forestEntrance = {
        Location = "Forest Entrance"
        Description = "You stand at the entrance of the Forest of Whispers. The path ahead is obscured by thick foliage. You can feel an eerie presence around you."
        Options = [("Go North", "DeepForest"); ("Check Map", "Map")]
    }

    let deepForest = {
        Location = "Deep Forest"
        Description = "You venture deeper into the forest. The trees are tall and ancient, their branches intertwining to form a natural canopy. You notice a faint trail leading to the east and hear water flowing to the west."
        Options = [("Follow the Trail East", "HiddenTrail"); ("Head West towards the Sound of Water", "BabblingBrook"); ("Return to the Forest Entrance", "ForestEntrance")]
    }

    let hiddenTrail = {
        Location = "Hidden Trail"
        Description = "You follow the trail east and come across a hidden clearing. In the center of the clearing stands a large, moss-covered stone with strange runes etched into it."
        Options = [("Inspect the Stone", "RuneStone"); ("Return to the Deep Forest", "DeepForest")]
    }

    let babblingBrook = {
        Location = "Babbling Brook"
        Description = "You follow the sound of water to a small brook. The water is crystal clear, and you can see fish swimming in it. On the other side of the brook, you notice a cave entrance."
        Options = [("Cross the Brook to Enter the Cave", "CaveEntrance"); ("Return to the Deep Forest", "DeepForest")]
    }

    let runeStone = {
        Location = "Rune Stone"
        Description = "Upon closer inspection, the runes on the stone begin to glow faintly. You feel a surge of energy, and the ground beneath you trembles. A hidden compartment opens, revealing a piece of an ancient map."
        Options = [("Take the Map Piece", "DeepForest"); ("Return to the Hidden Trail", "HiddenTrail")]
    }

    let caveEntrance = {
        Location = "Cave Entrance"
        Description = "You carefully cross the brook and stand at the entrance of the cave. It's dark and foreboding, with a cold breeze flowing out. You can just make out a faint light deeper inside."
        Options = [("Enter the Cave", "AncientCave"); ("Return to the Babbling Brook", "BabblingBrook")]
    }

    let ancientCave = {
        Location = "Ancient Cave"
        Description = "Inside the cave, you find ancient carvings on the walls depicting the story of the amulet. The cave branches into two paths: one leading deeper into darkness, the other towards a shimmering light."
        Options = [("Follow the Path into Darkness", "DarkPath"); ("Follow the Path to the Light", "LightPath")]
    }

    let lightPath = {
        Location = "Light Path"
        Description = "You follow the light path, and it becomes increasingly narrow and treacherous. Suddenly, you encounter a fearsome beast guarding a treasure chest. Now you have to fight with him."
        Options = [("Fight the Beast", "FightBeast")]
    }

    let darkPath = {
        Location = "Dark Path"
        Description = "You follow the dark path and find a hidden chamber filled with ancient artifacts. In the center, on a pedestal, lies the Lost Amulet, glowing with a mystical light."
        Options = [("Take the Amulet", "ChamberOfAmulet"); ("Return to the Cave Entrance", "CaveEntrance")]
    }

    let chamberOfAmulet = {
        Location = "Chamber of the Amulet"
        Description = "As you take the amulet, you feel a surge of power coursing through you. You have completed your quest and can now return to the Forest Entrance with the amulet."
        Options = [("Return to the Forest Entrance", "ForestEntrance")]
    }

    let fightBeast = {
        Location = "Fight with Beast"
        Description = "You engage in a fierce battle with the beast. Despite your best efforts, the beast proves too strong, and you are defeated."
        Options = [("Return to the Forest Entrance", "ForestEntrance")]
    }

    let map = {
        Location = "Map"
        Description = "You check the map. It shows a rough layout of the forest with markings for trails and notable locations."
        Options = [("Return to the Forest Entrance", "ForestEntrance")]
    }

    let initialState = {
        Location = forestEntrance.Location
        Description = forestEntrance.Description
        Options = forestEntrance.Options
    }

    let initialGame = {
        Player = initialPlayer
        State = initialState
    }

    let updateGameState (game: Game) action =
        match action with
        | "ForestEntrance" -> { game with State = forestEntrance }
        | "DeepForest" -> { game with State = deepForest }
        | "HiddenTrail" -> { game with State = hiddenTrail }
        | "BabblingBrook" -> { game with State = babblingBrook }
        | "RuneStone" -> { game with State = runeStone }
        | "CaveEntrance" -> { game with State = caveEntrance }
        | "AncientCave" -> { game with State = ancientCave }
        | "DarkPath" -> { game with State = darkPath }
        | "LightPath" -> { game with State = lightPath }
        | "ChamberOfAmulet" -> { game with State = chamberOfAmulet }
        | "FightBeast" -> { game with State = fightBeast }
        | "Map" -> { game with State = map }
        | _ -> game
