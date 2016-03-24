using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class OutcomeManager : MonoBehaviour
{
	[Serializable]
    public class outcome
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
        for (int i = 0; i < GameManager.instance.outcomeList.Count; i++)
        {
            if (GameManager.instance.HasData(GameManager.instance.outcomeList[i].BooleanName))
            {
                WriteToOutcome(GameManager.instance.outcomeList[i].getText(GameManager.instance.GetData(GameManager.instance.outcomeList[i].BooleanName)));
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
