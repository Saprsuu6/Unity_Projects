using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameObject Player;
    private Vector3 rod;
    private Vector3 rodBase;

    private float camAngleX;   // текущее значение поворота камеры по горизонтали
    private float camAngleX0;  // начальное значение поворота камеры (из редактора)
    private float camAngleY;
    public Vector2 CamMinMax_Y = new Vector2(-40, 40);    // ограничение по оси Y

    private float zoom;
    private const float MAX_ZOOM = 3f;
    private const float MIN_ZOOM = 1.2f;
    private const float SENSITIVITY_ZOOM = 10;

    void Start()
    {
        Player = GameObject.Find("Player");
        //transform.position = Player.transform.position;
        rod = transform.position - Player.transform.position;
        //Cursor.lockState = CursorLockMode.Locked;
        camAngleX0 =                         // угол поворота по горизонтали -
            camAngleX =                      // это угол поворота вокруг
            transform.eulerAngles.y;    // оси Y
        camAngleY = transform.eulerAngles.x;
    }
    private void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * GameSettings.Sencitivity * Time.timeScale;
        float mouseX = Input.GetAxis("Mouse X") * GameSettings.Sencitivity * Time.timeScale;
        camAngleY += mouseY * (GameSettings.InverceMouseVertical ? 1 : -1);
        camAngleX += mouseX;

        float scroll = Input.mouseScrollDelta.y *
            (GameSettings.InverseWheelZoom ? -1 : 1);    // учитываем настройки (инверсия Zoom)
        if (scroll > 0)
        {
            if (rod.magnitude < 0.1f)
            {
                rod = Vector3.zero;
            }
            else
            {
                rod /= 1.5f;
            }
        }
        else if (scroll < 0)
        {
            if (rod.magnitude > rodBase.magnitude)
            {
                rod = rodBase;
            }
            else
            {
                rod *= 1.5f;
            }
        }
    }
    void LateUpdate()
    {
        if (Input.mouseScrollDelta != Vector2.zero)
        {
            zoom -= Input.mouseScrollDelta.y / SENSITIVITY_ZOOM * Time.timeScale;
            if (zoom < MIN_ZOOM) zoom = MIN_ZOOM;
            if (zoom > MAX_ZOOM) zoom = MAX_ZOOM;
        }

        transform.position = Player.transform.position
            + Quaternion.Euler(0, camAngleX - camAngleX0, 0) * rod * zoom;

        transform.eulerAngles = new Vector3(camAngleY, camAngleX, 0);

        Vector3 pf =
                Quaternion.Euler(0, -camAngleX0, 0)
                * transform.forward;
        pf.y = 0;
        Player.transform.forward = pf.normalized;
    }
}
/* Управление курсором - движения мыши
 * Вращение мира - ЛКМ + движение мыши
 * Персонаж идет по камере - ЛКМ + движение мыши
 * Камера крутится не влияя на движение персонажа (обзор) - ПКМ + движение мыши
 */