using UnityEngine;

public class DragBlock : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        // ������������ ���������� ��'���� � �������� ���� � ������ ������
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        // ����������� ������� �� �������� ��'���� �� �������� ����
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        // ����������� ���� ������� ��'���� �� ����� ������� ����
        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;

        // ��������� ��'��� �� ���� �������
        transform.position = cursorPosition;
    }
}
