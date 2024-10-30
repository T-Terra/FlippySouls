using UnityEngine;

public class BibleRotation : MonoBehaviour
{
    private float angle = 0f;
    public float radius = 5f; // Raio do movimento circular
    public float angularSpeed; // Velocidade angular (em radianos por segundo)
    // Update is called once per frame
    void FixedUpdate()
    {
        RotationBible();
    }

    private void RotationBible() {
        // Calcula o ângulo baseado no tempo e na velocidade angular
        float direction = -1f;
        angle = direction * angularSpeed * radius;

        // Cria um novo vetor para representar a rotação no eixo Z (ou qualquer outro eixo)
        Vector3 rotationAxis = new Vector3(0, 0, 1 * direction); // Eixo Z como exemplo

        // Aplica a rotação ao redor do eixo definido
        transform.Rotate(rotationAxis, angle * direction);
    }
}
