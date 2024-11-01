using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsPowerUp : MonoBehaviour
{
    public Button[] Buttons;
    private void Start() {
        foreach (Button button in Buttons)
        {
            button.onClick.AddListener(DesactiveButton);
        }
    }

    void Update()
    {
        // Verifica se o mouse foi clicado ou se houve um toque na tela
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            foreach (Button button in Buttons)
            {
                button.interactable = true;
            }
        }
    }

    private void DesactiveButton() {
        foreach (Button button in Buttons)
        {
            button.interactable = false;
        }
    }
}
