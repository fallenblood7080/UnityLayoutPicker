using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UIElements;

namespace FallenEditorTool
{
    public class LayoutPicker : EditorWindow
    {
        private List<string> layoutNames = new();

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
            EditorApplication.ExecuteMenuItem($"Window/Layouts/{layoutNames[index]}");
            this.Close();
        }
        private void OnLostFocus()
        {
            this.Close();
        }
    } 
}
