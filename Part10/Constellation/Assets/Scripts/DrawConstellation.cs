using System.Linq;
using UnityEngine;

public class DrawConstellation : MonoBehaviour
{
    static float SpaceSize = 1500.0f;    　　　 // 별자리 구의 반경
    static float StarBaseSize = 8.0f;           // 별의 크기 기준

    [SerializeField]
    GameObject starPrefab;                      // 별의 프리팹
    [SerializeField]
    GameObject linePrefab;                      // 별자리 선의 프리팹
    [SerializeField]
    GameObject namePrefab;                      // 별자리 이름의 프리팹

    public ConstellationData ConstellationData { get; set; }    // 그리는 별자리 데이터

    GameObject linesParent;                     // 라인을 합하는 게임 오브젝트

    // 라인을 합하는 게임 오브젝트의 프로퍼티
    public GameObject LinesParent { get { return linesParent; } }

    void Start()
    {
        // GameObject의 이름을 별자리 이름으로 변경
        if (ConstellationData.Name != null)
        {
            gameObject.name = ConstellationData.Name.Name;
        }

        // 데이터로부터 별자리를 작성
        CreateConstellation();
    }

    // 별자리의 작성
    void CreateConstellation()
    {
        // 리스트로부터 별을 작성
        foreach (var star in ConstellationData.Stars)
        {
            // 별의 작성
            var starObject = CreateStar(star);
            // 자신의 자식에게 접속
            starObject.transform.SetParent(transform, false);
        }

        if (ConstellationData.Lines != null)
        {
            // 별자리 선의 부모를 작성
            linesParent = new GameObject("Lines");
            // 자신의 자식에게 접속
            linesParent.transform.SetParent(transform, false);
            var parent = linesParent.transform;

            // 리스트로부터 별자리 선을 작성
            foreach (var line in ConstellationData.Lines)
            {
                // 별자리 선의 작성
                var lineObject = CreateLine(line);
                // 별자리 선의 부모의 자식에게 접속
                lineObject.transform.SetParent(parent, false);
            }
        }

        if (ConstellationData.Name != null)
        {
            // 별자리 이름을 작성
            var nameObject = CreateName(ConstellationData.Name, ConstellationData.Position);
            // 자신의 자식에게 접속
            nameObject.transform.SetParent(transform, false);
        }
    }

    // 별의 작성
    GameObject CreateStar(StarData starData)
    {
        // 별의 프리팹으로부터 인스턴스 작성
        var star = Instantiate(starPrefab);
        var starTrans = star.transform;

        // 별이 보이는 방향으로 회전시킨다
        starTrans.localRotation = Quaternion.Euler(starData.Declination, starData.RightAscension, 0.0f);
        // 별의 이름을 HIP 번호로 한다
        star.name = string.Format("{0}", starData.Hip);

        var child = starTrans.GetChild(0);
        // 자식의 구의 위치를 천구의 위치로 이동시킨다
        child.transform.localPosition = new Vector3(0.0f, 0.0f, SpaceSize);

        // 시등급을 별의 크기로 한다
        var size = StarBaseSize - starData.ApparentMagnitude;
        child.transform.localScale = new Vector3(size, size, size);

        // Renderer의 취득
        var meshRanderer = child.GetComponent<Renderer>();
        var color = Color.white;

        // 별의 컬러 타입에 따라 색을 설정한다
        switch (starData.ColorType)
        {
            case "O":   // 파랑
                color = Color.blue;
                break;
            case "B":   // 청백
                color = Color.Lerp(Color.blue, Color.white, 0.5f);
                break;
            default:
            case "A":   // 흰색
                color = Color.white;
                break;
            case "F":   // 황백
                color = Color.Lerp(Color.white, Color.yellow, 0.5f);
                break;
            case "G":   // 노랑
                color = Color.yellow;
                break;
            case "K":   // 주황
                color = new Color(243.0f / 255.0f, 152.0f / 255.0f, 0.0f);
                break;
            case "M":   // 빨강
                color = new Color(200.0f / 255.0f, 10.0f / 255.0f, 0.0f);
                break;
        }

        // 머터리얼에 색을 설정한다
        meshRanderer.material.SetColor("_Color", color);

        return star;
    }

    // 별자리 선의 작성
    GameObject CreateLine(ConstellationLineData lineData)
    {
        // 시작점의 별의 정보를 취득
        var start = GetStar(lineData.StartHip);
        // 끝점의 별의 정보를 취득
        var end = GetStar(lineData.EndHip);
        // 별자리 선의 프리팹으로부터 인스턴스 작성
        var line = Instantiate(linePrefab);
        // LineRenderer의 취득
        var lineRenderer = line.GetComponent<LineRenderer>();

        // LineRenderer의 시작점과 끝점의 위치를 등록(별이 보이는 방향으로 회전시킨 후, 천구의 위치까지 이동시킨다)
        lineRenderer.SetPosition(0, Quaternion.Euler(start.Declination, start.RightAscension, 0.0f) * new Vector3(0.0f, 0.0f, SpaceSize));
        lineRenderer.SetPosition(1, Quaternion.Euler(end.Declination, end.RightAscension, 0.0f) * new Vector3(0.0f, 0.0f, SpaceSize));

        return line;
    }

    // StarData의 데이터 검색
    StarData GetStar(int hip)
    {
        // 같은 HIP 번호를 검색
        return ConstellationData.Stars.FirstOrDefault(s => hip == s.Hip);
    }

    // 별자리 이름의 작성
    GameObject CreateName(ConstellationNameData nameData, ConstellationPositionData positionData)
    {
        // 별자리 이름의 프리팹으로부터 인스턴스 작성
        var text = Instantiate(namePrefab);
        var textTrans = text.transform;

        // 별이 보이는 방향으로 회전시킨다
        textTrans.localRotation = Quaternion.Euler(positionData.Declination, positionData.RightAscension, 0.0f);
        text.name = nameData.Name;

        // 자식의 3D Text 위치를 천구의 위치로 이동시킨다
        var child = textTrans.GetChild(0);
        child.transform.localPosition = new Vector3(0.0f, 0.0f, SpaceSize);

        // TextMesh를 취득해서 별자리 이름으로 변경한다 
        var textMesh = child.GetComponent<TextMesh>();
        textMesh.text = string.Format("{0}좌", nameData.KoreanName);

        return text;
    }
}