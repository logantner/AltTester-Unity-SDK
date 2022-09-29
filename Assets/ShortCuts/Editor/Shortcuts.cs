using Altom.AltTesterEditor;

namespace altunitytester.Assets.ShortCuts.Editor
{
    public class Shortcuts
    {
        [UnityEditor.MenuItem("AltTester Tools/Add AltTesterSymbol &#]", false, 80)]
        public static void AddAUTSymbol()
        {
            var scriptingDefineSymbolsForGroup = UnityEditor.PlayerSettings.GetScriptingDefineSymbolsForGroup(UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup);
            if (!scriptingDefineSymbolsForGroup.Contains("ALTUNITYTESTER"))
                scriptingDefineSymbolsForGroup += ";ALTUNITYTESTER";
            UnityEditor.PlayerSettings.SetScriptingDefineSymbolsForGroup(UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup, scriptingDefineSymbolsForGroup);
        }

        [UnityEditor.MenuItem("AltTester Tools/Deselect All Tests &#[", false, 80)]
        public static void DeselectAllTests()
        {
            foreach (var test in AltTesterEditorWindow.EditorConfiguration.MyTests)
            {
                test.Selected = false;
            }
        }

    }
}