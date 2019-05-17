using UnityEngine;
using UnityEngine.UI;
public class UIManager
{
    public void DisplayLeftScore(string text)
    {
        GameObject.Find("p1_score").GetComponent<Text>().text = text;
    }
    public void DisplayRightScore(string text)
    {
        GameObject.Find("p2_score").GetComponent<Text>().text = text;
    }
}
