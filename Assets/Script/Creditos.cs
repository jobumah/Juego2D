using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    
    public void VolverMenu(string escena)
    {
        SceneManager.LoadScene(escena);
    }

}
