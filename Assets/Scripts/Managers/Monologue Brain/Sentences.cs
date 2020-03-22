using UnityEngine;

[System.Serializable]
public class Sentences
{
    //! Brief description of what the sentence says or when it should be shown
    [Tooltip("Brief description of what the sentence implies or when it should be shown.")]
    public string description;

    //! Index for sentence
    [Tooltip("Sentence with the respective index will be shown in Monologue when called by a function.")]
    public int index;

    //! What will actually show in the Monologue text in game
    [Tooltip("Will be used to display Monoglogue text in the game.")]
    [TextArea(0, 3)]
    public string sentenceText;

    //! custom display time for each sentence before disappearing and put on cooldown
    [Tooltip("Custom display time for each Monologue sentence in the screen before disappearing and put on cooldown.")]
    public float displayMonologueTimer = 3f;
    [ReadOnly]
    public float displayMonologueTimerCounter;
    [ReadOnly]
    public bool displayMonologue = false;
}
