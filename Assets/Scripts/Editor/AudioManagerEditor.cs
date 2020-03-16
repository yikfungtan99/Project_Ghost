using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor
{
    public string[] audioNameList;
    public int audioIndex1;
    public int audioIndex2;
    public int audioIndex3;

    float fadeAmount1;
    float fadeAmount2;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AudioManager am = (AudioManager)target;

        //! Initialise size of array & insert string values
        audioNameList = new string[am.audioList.Length];
        for (int i = 0; i < am.audioList.Length; i++)
        {
            audioNameList[i] = am.audioList[i].name;
        }

        audioIndex1 = EditorGUILayout.Popup(new GUIContent("Select AudioClip"), audioIndex1, audioNameList);
        if (GUILayout.Button("Play Audio"))
        {
            am.PlayAudio(am.audioList[audioIndex1].name);
        }
        if (GUILayout.Button("Toggle Pause"))
        {
            if(!am.audioList[audioIndex1].isPaused)
            {
                am.Pause(am.audioList[audioIndex1].name);
            }
            else
            {
                am.Unpause(am.audioList[audioIndex1].name);
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        audioIndex2 = EditorGUILayout.Popup(new GUIContent("Select AudioClip"), audioIndex2, audioNameList);
        fadeAmount1 = EditorGUILayout.FloatField("Custom Fade Amount", fadeAmount1);
        if (GUILayout.Button("Make Audio Fade Out"))
        {
            am.FadeOutAudio(am.audioList[audioIndex2].name, fadeAmount1);
        }
        if (GUILayout.Button("Toggle Pause"))
        {
            if (!am.audioList[audioIndex2].isPaused)
            {
                am.Pause(am.audioList[audioIndex2].name);
            }
            else
            {
                am.Unpause(am.audioList[audioIndex2].name);
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        audioIndex3 = EditorGUILayout.Popup(new GUIContent("Select AudioClip"), audioIndex3, audioNameList);
        fadeAmount2 = EditorGUILayout.FloatField("Custom Fade Amount", fadeAmount2);
        if (GUILayout.Button("Make Audio Fade In"))
        {
            am.FadeInAudio(am.audioList[audioIndex3].name, fadeAmount2);
        }
        if (GUILayout.Button("Toggle Pause"))
        {
            if (!am.audioList[audioIndex3].isPaused)
            {
                am.Pause(am.audioList[audioIndex3].name);
            }
            else
            {
                am.Unpause(am.audioList[audioIndex3].name);
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if(GUILayout.Button("Stop All Audio from Editor"))
        {
            if(am.audioList[audioIndex1].isPlaying)
            {
                am.StopAudio(am.audioList[audioIndex1].name);
            }
            if(am.audioList[audioIndex2].isPlaying)
            {
                am.StopAudio(am.audioList[audioIndex2].name);
            }
            if(am.audioList[audioIndex3].isPlaying)
            {
                am.StopAudio(am.audioList[audioIndex3].name);
            }
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("Force Stop All Audio from Editor"))
        {
            if (am.audioList[audioIndex1].isPlaying)
            {
                am.ForceStopAudio(am.audioList[audioIndex1].name);
            }
            if (am.audioList[audioIndex2].isPlaying)
            {
                am.ForceStopAudio(am.audioList[audioIndex2].name);
            }
            if (am.audioList[audioIndex3].isPlaying)
            {
                am.ForceStopAudio(am.audioList[audioIndex3].name);
            }
        }
    }
}
