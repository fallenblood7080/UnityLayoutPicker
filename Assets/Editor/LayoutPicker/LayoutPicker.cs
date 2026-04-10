using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

using UnityEngine.UIElements;

namespace FallenEditorTool
{
    /*
     * Is there any way to make this work without a ScriptableObject?
     * If no, how can make SO path dynamic/relative?
     * And how should I add new layout automatically to the list?
     * BUG: Currently, when window pops up, first button is not focused but selected and works fine after that.
     */
    public class LayoutPicker : EditorWindow
    {
        private const string LAYOUTPATHSO = "Assets/Editor/LayoutPicker/LayoutSO.asset";
        private List<string> layoutNames = new();
        //TODO: Open a mouse position
        //& - alt, # - shift, % - ctrl 
        [Shortcut("LayoutPicker",KeyCode.L,ShortcutModifiers.Alt|ShortcutModifiers.Shift)]// Alt + Shift + L
        public static void ShowWindow()
        {
            var win = CreateInstance<LayoutPicker>();
            win.titleContent = new GUIContent("Layout Switcher");
            win.position = new Rect(Screen.width / 2, Screen.height / 2, 200, 200);
            win.ShowPopup();
        }
        private void CreateGUI()
        {
            //LayoutSO layoutSO = AssetDatabase.LoadAssetAtPath<LayoutSO>(LAYOUTPATHSO);
            layoutNames = LayoutFinder.GetCustomLayoutNames();
            Label label = new("Select Layout");
            label.style.unityFontStyleAndWeight = FontStyle.Bold;
            label.style.alignSelf = Align.Center;
            label.style.marginTop = 8;
            label.style.marginBottom = 8;
            label.style.fontSize = 14;

            var list = new ScrollView();
            list.verticalScroller.style.display = DisplayStyle.None;
            list.verticalScroller.style.width = 0;
            for (int i = 0; i < layoutNames.Count; i++)
            {
                int index = i;
                var btn = new Button(() => Select(index))
                {
                    text = layoutNames[index],
                    focusable = true
                };
                btn.style.marginBottom = 4;
                list.Add(btn);
            }
            rootVisualElement.Add(label);
            rootVisualElement.Add(list);

            int selectedIndex = 0;
            //list.ElementAt(selectedIndex).Focus();

            rootVisualElement.focusable = true;
            list.ElementAt(selectedIndex).Focus();
            rootVisualElement.RegisterCallback<KeyDownEvent>(evt =>
            {
                if (evt.keyCode == KeyCode.UpArrow || evt.keyCode == KeyCode.DownArrow)
                {
                    if (evt.keyCode == KeyCode.UpArrow)
                    {
                        selectedIndex = (selectedIndex - 1 + layoutNames.Count) % layoutNames.Count;
                    }
                    else
                    {
                        selectedIndex = (selectedIndex + 1) % layoutNames.Count;
                    }

                    var targetElement = list.ElementAt(selectedIndex);
                    targetElement.Focus();

                    // THIS IS THE KEY LINE:
                    // Automatically adjust the scrollbar to show the focused element
                    list.ScrollTo(targetElement);

                    evt.StopPropagation();
                }
                else if (evt.keyCode == KeyCode.Return)
                {
                    Select(selectedIndex);
                    evt.StopPropagation();
                }
                else if (evt.keyCode == KeyCode.Escape)
                {
                    this.Close();
                    evt.StopPropagation();
                }
            });
        }

        void Select(int index)
        {
            //LayoutSO layoutSO = AssetDatabase.LoadAssetAtPath<LayoutSO>(LAYOUTPATHSO);
            EditorApplication.ExecuteMenuItem($"Window/Layouts/{layoutNames[index]}");
            this.Close();
        }
        private void OnLostFocus()
        {
            this.Close();
        }
    } 
}
