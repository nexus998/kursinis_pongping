using UnityEngine;
using UnityEngine.UI;
public class UIManager
{
    //use unity stuff to find objects in the scene and change their UI elements.


    public void DisplayLeftScore(string text)
    {
        GameObject.Find("p1_score").GetComponent<Text>().text = text;
    }
    public void DisplayRightScore(string text)
    {
        GameObject.Find("p2_score").GetComponent<Text>().text = text;
    }
    public void DisplayLeftHighScore(string text)
    {
        GameObject.Find("p1_high_score").GetComponent<Text>().text = "PB: " + text;
    }
    public void DisplayRightHighScore(string text)
    {
        GameObject.Find("p2_high_score").GetComponent<Text>().text = "PB: " + text;
    }
}
