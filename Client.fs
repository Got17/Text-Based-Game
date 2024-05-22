namespace GentleGame

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.UI.Html
open WebSharper.Sitelets

type IndexTemplate = Template<"wwwroot/index.html", ClientLoad.FromDocument>

type EndPoint = 
    | [<EndPoint "/">] Home
    | [<EndPoint "/gamePage">] GamePageIntro
    | [<EndPoint "/gamePage/start">] GamePageStart
    | [<EndPoint "/gamePage/start/ForestEntrance">] GamePageForestEntrance
    | [<EndPoint "/gamePage/start/Map">] GamePageMap
    | [<EndPoint "/gamePage/start/DeepForest">] GamePageDeepForest
    | [<EndPoint "/gamePage/start/HiddenTrail">] GamePageHiddenTrail
    | [<EndPoint "/gamePage/start/BabblingBrook">] GamePageBabblingBrook
    | [<EndPoint "/gamePage/start/RuneStone">] GamePageRuneStone
    | [<EndPoint "/gamePage/start/CaveEntrance">] GamePageCaveEntrance
    | [<EndPoint "/gamePage/start/AncientCave">] GamePageAncientCave
    | [<EndPoint "/gamePage/start/DarkPath">] GamePageDarkPath
    | [<EndPoint "/gamePage/start/LightPath">] GamePageLightPath
    | [<EndPoint "/gamePage/start/FightBeast">] GamePageFightBeast
    | [<EndPoint "/gamePage/start/ChamberOfAmulet">] GamePageChamberOfAmulet

[<JavaScript>]
module Pages =
    open GameModel

    let gameState = Var.Create initialGame

    let Homepage() = 
        IndexTemplate.homepage()
            
            .ToGamePage("/#/gamePage")
            .Doc()
        
    let GamePageIntro() = 
        IndexTemplate.gamepageIntro()
            .toGamePageStart("/#/gamePage/start")
            .Doc()

    let GamePageStart() = 
        IndexTemplate.gamepageStart()
            .toGamePageLocation("/#/gamePage/start/ForestEntrance")
            .Doc()

    let updateGame (action: string) =
        gameState.Value <- updateGameState gameState.Value action

    let GamePageLocation() =         
        IndexTemplate.gamepageLocation()
            .location(gameState.Value.State.Location)
            .description(gameState.Value.State.Description)
            .gameImage(
                gameState.Value.State.Location.Replace(" ","")
            )
            .continueButtonSection(
                gameState.Value.State.Options
                |> List.map (fun (desc, action) ->
                    a [attr.href (sprintf "/#/gamePage/start/%s" action)] [
                        button [ attr.``class`` "continue-button"; on.click (fun _ _ -> updateGame action)] [text desc]
                    ]
                )
                |> Doc.Concat
            )
            .Doc()
        

[<JavaScript>]
module Client =
    let router = Router.Infer<EndPoint>()
    // Install our client-side router and track the current page
    let currentPage = Router.InstallHash Home router


    [<SPAEntryPoint>]
    let Main =
        let renderInnerPage (currentPage: Var<EndPoint>) =
            currentPage.View.Map (fun endpoint ->
                match endpoint with
                | Home                      -> Pages.Homepage()
                | GamePageIntro             -> Pages.GamePageIntro()
                | GamePageStart             -> Pages.GamePageStart()
                | GamePageForestEntrance    -> Pages.GamePageLocation()
                | GamePageMap               -> Pages.GamePageLocation()
                | GamePageDeepForest        -> Pages.GamePageLocation()
                | GamePageHiddenTrail       -> Pages.GamePageLocation()
                | GamePageBabblingBrook     -> Pages.GamePageLocation()
                | GamePageRuneStone         -> Pages.GamePageLocation()
                | GamePageCaveEntrance      -> Pages.GamePageLocation()
                | GamePageAncientCave       -> Pages.GamePageLocation()
                | GamePageDarkPath          -> Pages.GamePageLocation()
                | GamePageLightPath         -> Pages.GamePageLocation()
                | GamePageFightBeast        -> Pages.GamePageLocation()
                | GamePageChamberOfAmulet   -> Pages.GamePageLocation()

            )
            |> Doc.EmbedView

        IndexTemplate()
            .PageContent(renderInnerPage currentPage)
            .Bind()
