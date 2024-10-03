using UnityEngine;

public class ChoiceManager: MonoBehaviour {
    public void OnClick() {
        Debug.Log("Executing Ink, to see what's happening use the debugger with a breakpoint on this line.");
        LD.Ink.DoKnot("Start");
        var state = LD.Ink.CurrentState;
        LD.Ink.DoChoice(state.Choices[0]);
        state = LD.Ink.CurrentState;
    }
}