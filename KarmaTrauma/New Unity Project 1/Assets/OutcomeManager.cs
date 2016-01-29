using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OutcomeManager : MonoBehaviour
{

    private class outcome
    {
        public string BooleanName;
        public string True_text;
        public string False_text;
        public outcome(string boolname, string true_text, string false_text)
        {
            BooleanName = boolname;
            True_text = true_text;
            False_text = false_text;
        }

        public string getText(bool state)
        {
            if (state == true)
            {
                return True_text;
            }
            else
            {
                return False_text;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        GameManager.instance.transform.Find("Menu_layout").gameObject.SetActive(false);
        outcome[] outcomes = new outcome[]{
            new outcome ("TestJewelOutcome",
                        "You have found the jewel of ultimate power. Now you set off on your quest to rule the world",
                        "The jewel broke and made you sad."),
            new outcome ("TestPeanutButterOutcome",
                        "You ate some delicious peanut butter. Yum",
                        "Without peanut butter, you fall into a deep depression and starved to death"),
            new outcome ("NoOutcomeHere",
                        "blank",
                        "blank")
        };
        for (int i = 0; i < outcomes.Length; i++)
        {
            if (GameManager.instance.HasData(outcomes[i].BooleanName))
            {
                WriteToOutcome(outcomes[i].getText(GameManager.instance.GetData(outcomes[i].BooleanName)));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void WriteToOutcome(string text)
    {
        transform.Find("Text").GetComponent<Text>().text += text + "\n\n";
    }
}
