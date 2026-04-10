# Unity Layout Picker

A tool to quickly switch layouts just by keyboard shortcut...


https://github.com/user-attachments/assets/a9de771b-89e9-4d08-9097-af042d39b362


---

## 📥 Installation

1. Download the latest `.unitypackage` from the [Releases](../../releases) page.
2. Drag and drop it into your Unity project.

---

## ⌨️ How to Use

1. **Open the Picker:** Press **Alt + Shift + L** (Default).
2. **Navigate:** Use the **Up/Down Arrow Keys** to highlight a layout.
3. **Confirm:** Press **Enter** to apply the layout immediately.
4. **Close:** Press **Escape** to cancel.

### Customising the Shortcut
Don't like the default keys? You can change them easily:
1. Go to **Edit > Shortcuts...**.
2. Search for **"Layout Picker"**.
3. Assign your preferred key combination.

---

## 🛠 Troubleshooting

### "No Layouts Found"
The tool scans Unity's internal preferences folder for `.wlt` files. If your layouts are not appearing:
1. Ensure you have actually **saved** your layout in the top-right corner of Unity (**Layout > Save Layout**).
2. **Check Paths:** If you use a non-standard Unity installation, open the `LayoutFinder.cs` script and verify that the `layoutsPath` matches your local machine's directory:
   - **Windows:** `%AppData%\Roaming\Unity\Editor-5.x\Preferences\Layouts`
   - **macOS:** `~/Library/Preferences/Unity/Editor-5.x/Layouts`
   - **Linux:** `~/.config/unity3d/Editor-5.x/Layouts`

---


