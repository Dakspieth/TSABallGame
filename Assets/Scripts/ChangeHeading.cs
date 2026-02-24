using TMPro;
using UnityEngine;

public class ChangeHeading : MonoBehaviour
{
    public MainMenuScript mms;
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
    public void LevelsChangeText()
    {
        heading.text = "Levels";
    }
    public void TurnOff()
    {
        mms.on = false;
    }
    public void TurnOn()
    {
        mms.on=true;
    }
}
