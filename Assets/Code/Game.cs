public static class Game {
    public static void DoPlayerChoice(InkChoice choice) {
        LD.Ink.DoChoice(choice);
        UI.ChoiceMgr.RebuildChoices();
    }

    public static void DoStartNewGame(string inkJson) {
        LD.OnStartNewGame(inkJson);
        LD.Ink.DoKnot("Start");
        UI.ChoiceMgr.RebuildChoices();
    }
}
