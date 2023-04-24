using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject userMenu;
    [SerializeField]
    private TMPro.TextMeshProUGUI resumeButtonText;

    // Start is called before the first frame update
    void Start()
    {
        if (userMenu.activeInHierarchy)
        {
            ShowMenu(userMenu.activeInHierarchy, "Start");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenu(!userMenu.activeInHierarchy);
        }
    }

    public void ResumeButtonClick()
    {
        ShowMenu(false); 
    }

    private void  ShowMenu(bool mode, string text = "Resume")
    {
        if (mode)
        {
            userMenu.SetActive(true);
            Time.timeScale = 0;
            resumeButtonText.text = text;
        }
        else
        {
            userMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
