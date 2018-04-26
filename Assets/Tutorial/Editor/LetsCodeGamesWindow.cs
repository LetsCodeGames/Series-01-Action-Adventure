using UnityEngine;
using UnityEditor;
using System.Collections;

public class LetsCodeGamesWindow : EditorWindow
{
    [MenuItem( "Let's Code Games/Open LCG Window" )]
    public static void OpenWindow()
    {
        GetWindow<LetsCodeGamesWindow>();
    }

    private void OnGUI()
    {

    }
}
