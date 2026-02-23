using TMPro;
using UnityEngine;

public class ChangeHeading : MonoBehaviour
{
    public TMP_Text heading;
    public void GuideStartChangeText()
    {
        heading.text = "Overview";
    }
    public void GuideNextChangeText()
    {
        heading.text = "Game Layout";
    }
    public void MenuChangeText()
    {
        heading.text = "Game Name";
    }
}
