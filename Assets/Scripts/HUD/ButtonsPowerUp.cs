using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsPowerUp : MonoBehaviour
{
    private void Start() {
        this.gameObject.GetComponent<Button>().onClick.AddListener(DesactiveButton);
    }

    void Update()
    {
        // Verifica se o mouse foi clicado ou se houve um toque na tela
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            this.gameObject.GetComponent<Button>().interactable = true; // Ativa o botão
        }
    }

    private void DesactiveButton() {
        this.gameObject.GetComponent<Button>().interactable = false;
    }
}
