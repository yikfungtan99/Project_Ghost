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

    //! cooldown time for text to be called it (in the case that it is spammed)
    [Tooltip("Cooldown before the same line of Monologue text is displayed twice in a row.")]
    public float cooldown = 3f;
    public float cooldownCounter;
}
