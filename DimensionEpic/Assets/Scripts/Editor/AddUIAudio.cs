using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AddUIAudio : ScriptableWizard
{
    public AudioClip m_UseAudio;

    [MenuItem("Editor/UIAudio/Button")]
    public static void CreateWindow()
    {
        ScriptableWizard.DisplayWizard("UI音效替换", typeof(AddUIAudio), "添加音效", "删除音效");
    }

    void OnWizardUpdate()
    {
        if (m_UseAudio == null)
        {
            isValid = false;
            errorString = "拖拽想要添加的音效";
        }
        else
        {
            isValid = true;
            errorString = "";
        }
    }

    private void OnWizardCreate()
    {
        //遍历
        var allObject = Resources.FindObjectsOfTypeAll(typeof(GameObject));
        var previousSelection = Selection.objects;
        Selection.objects = allObject;
        var selectedTransforms = Selection.GetTransforms(SelectionMode.Editable | SelectionMode.ExcludePrefab);
        Selection.objects = previousSelection;
        foreach(var trans in selectedTransforms)
        {
            Button button = trans.GetComponent<Button>();
            if (button != null && trans.GetComponent<AudioPlay>() == null)
            {
                Debug.Log("给" + button.name + "添加音效");
                AudioPlay audio = trans.gameObject.AddComponent<AudioPlay>();
                audio.m_UseAudio = m_UseAudio;
            }
        }
    }

    private void OnWizardOtherButton()
    {
        //遍历
        var allObject = Resources.FindObjectsOfTypeAll(typeof(GameObject));
        var previousSelection = Selection.objects;
        Selection.objects = allObject;
        var selectedTransforms = Selection.GetTransforms(SelectionMode.Editable | SelectionMode.ExcludePrefab);
        Selection.objects = previousSelection;
        foreach (var trans in selectedTransforms)
        {
            Button button = trans.GetComponent<Button>();
            if (button != null)
            {
                Debug.Log("给" + button.name + "添加音效");
                DestroyImmediate(trans.GetComponent<AudioPlay>());
                DestroyImmediate(trans.GetComponent<AudioSource>());
            }
        }
    }
}
