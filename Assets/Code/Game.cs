public static class Game {
    public static void DoChooseMapDestination() {
        SetState(GameData.GameState.ChoosingDestination);
    }

    public static void DoPlayerChoice(InkChoice choice) {
        LD.Ink.DoChoice(choice);
        UI.ChoiceMgr.RebuildChoices();
    }

    public static void DoStartIntro() {
        LD.UI.ShowIntro();
    }

    public static void DoStartNewGame(string inkJson) {
        LD.OnStartNewGame(inkJson);
        UI.ResourcesMgr.Refresh();
        UI.MapMgr.OnStartNewGame();
        LD.Ink.DoKnot("Start");
        UI.ChoiceMgr.RebuildChoices();
        LD.UI.ShowGameplay();
    }

    public static void DoStartPrelude() {
        LD.UI.ShowPrelude();
    }

    public static void SetState(GameData.GameState newState) {
        LD.Data.State = newState;

        switch (newState) {
        case GameData.GameState.ChoosingDestination:
            UI.MapMgr.ShowValidPaths();
            break;
        case GameData.GameState.Traveling:
            break;
        case GameData.GameState.Encounter:
            break;
        case GameData.GameState.Explore:
            break;
        case GameData.GameState.FutureSight:
            break;
        case GameData.GameState.Settling:
            break;
        case GameData.GameState.Gameover:
            break;
        default:
            throw new System.Exception($"Cannot understand {newState}");
        }
    }
}
