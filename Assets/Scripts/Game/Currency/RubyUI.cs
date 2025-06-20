using UnityEngine;
using TMPro;

public class RubyUI : MonoBehaviour
{
    private TMP_Text rubyText;

    private void Awake()
    {
        rubyText = GetComponent<TMP_Text>();
    }

    public void UpdateRubies(RubyController rubyController)
    {
        rubyText.text = $"Rubies: {rubyController.GetRubies()}";
    }
}