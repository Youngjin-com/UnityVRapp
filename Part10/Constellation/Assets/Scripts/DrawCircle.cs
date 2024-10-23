using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    [SerializeField]
    int lineCount = 100;        // 라인을 그리는 수
    [SerializeField]
    Color color = Color.white;  // 라인의 색
    [SerializeField]
    float radius = 1500.0f;     // 원의 반경

    Material lineMaterial;      // 라인의 머터리얼

    // 라인의 머터리얼 작성
    void CreateLineMaterial()
    {
        // 한 번만 작성한다
        if (lineMaterial == null)
        {
            // 유니티의 표준 셰이더를 취득
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            // 머터리얼을 작성해서 셰이더를 설정
            lineMaterial = new Material(shader);
            // 이 머터리얼을 하이어라키에 표시하지 않는다, 씬에 저장하지 않는다
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
        }
    }

    // 모든 카메라의 씬을 그린 후에 호출되는 그리기 함수
    void OnRenderObject()
    {
        // 머터리얼의 작성
        CreateLineMaterial();

        // 머터리얼을 설정
        lineMaterial.SetPass(0);

        // 현재 매트릭스 정보를 저장
        GL.PushMatrix();

        // 현재 매트릭스 정보를 이 게임 오브젝트의 매트릭스 정보로 갱신
        GL.MultMatrix(transform.localToWorldMatrix);

        // 라인 그리기를 시작한다
        GL.Begin(GL.LINES);

        // 컬러 설정
        GL.Color(color);

        // XZ 평면에 원을 그린다
        {
            // 처음의 정점 위치
            var startPoint = new Vector3(Mathf.Cos(0.0f) * radius, 0.0f, Mathf.Sin(0.0f) * radius);
            // 하나 전의 정점 위치
            var oldPoint = startPoint;
            for (var Li = 0; Li < lineCount; ++Li)
            {
                // 이번의 각도
                var angleRadian = (float)Li / (float)lineCount * (Mathf.PI * 2.0f);
                // 이번의 정점 위치
                var newPoint = new Vector3(Mathf.Cos(angleRadian) * radius, 0.0f, Mathf.Sin(angleRadian) * radius);
                // 전의 정점 위치에서 이번 위치로 라인을 긋는다
                GL.Vertex(oldPoint);
                GL.Vertex(newPoint);

                // 이번 위치를 저장
                oldPoint = newPoint;
            }
            // 마지막의 정점에서 처음 정점으로 라인을 긋는다
            GL.Vertex(oldPoint);
            GL.Vertex(startPoint);
        }
        // 라인 그리기를 종료한다
        GL.End();

        // 저장한 매트릭스 정보로 되돌린다 
        GL.PopMatrix();
    }
}