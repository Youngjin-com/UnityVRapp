using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConstellationViewer : MonoBehaviour
{
    // 별자리 CSV 데이터
    [SerializeField]
    TextAsset starDataCSV;
    [SerializeField]
    TextAsset starMajorDataCSV;
    [SerializeField]
    TextAsset constellationNameDataCSV;
    [SerializeField]
    TextAsset constellationPositionDataCSV;
    [SerializeField]
    TextAsset constellationLineDataCSV;

    [SerializeField]
    GameObject constellationPrefab;         // 별자리의 프리팹

    // 별자리 데이터
    List<StarData> starData;
    List<StarMajorData> starMajorData;
    List<ConstellationNameData> constellationNameData;
    List<ConstellationPositionData> constellationPositionData;
    List<ConstellationLineData> constellationLineData;

    // 정리한 별자리의 데이터
    List<ConstellationData> constellationData;

    void Start()
    {
        // CSV 데이터 읽기
        LoadCSV();

        // 별자리 데이터의 정리
        ArrangementData();

        // 별자리의 작성
        CreateConstellation();
    }

    // CSV 데이터 읽기
    void LoadCSV()
    {
        starData = CsvLoader<StarData>.LoadData(starDataCSV);
        starMajorData = CsvLoader<StarMajorData>.LoadData(starMajorDataCSV);
        constellationNameData = CsvLoader<ConstellationNameData>.LoadData(constellationNameDataCSV);
        constellationPositionData = CsvLoader<ConstellationPositionData>.LoadData(constellationPositionDataCSV);
        constellationLineData = CsvLoader<ConstellationLineData>.LoadData(constellationLineDataCSV);
    }

    // 별자리 데이터의 정리
    void ArrangementData()
    {
        // 별 데이터를 통합
        MergeStarData();

        constellationData = new List<ConstellationData>();

        // 별자리 이름으로부터 별자리에 필요한 데이터를 수집
        foreach (var name in constellationNameData)
        {
            constellationData.Add(CollectConstellationData(name));
        }

        // 별자리에 사용되지 않는 별의 수집
        var data = new ConstellationData();
        data.Stars = starData.Where(s => s.UseConstellation == false).ToList();
        constellationData.Add(data);
    }

    // 별 데이터를 통합
    void MergeStarData()
    {
        // 이번에 사용할 필요한 별을 판별한다
        foreach (var star in starMajorData)
        {
            // 같은 데이터가 있는가?
            var data = starData.FirstOrDefault(s => star.Hip == s.Hip);
            if (data != null)
            {
                // 같은 데이터가 있으면, 위치 데이터를 갱신한다
                data.RightAscension = star.RightAscension;
                data.Declination = star.Declination;
            }
            else
            {
                // 같은 데이터가 없는 경우, 5등성보다 밝으면 리스트 목록에 추가
                if (star.ApparentMagnitude <= 5.0f)
                {
                    starData.Add(star);
                }
            }
        }
    }

    // 별자리 데이터의 수집
    ConstellationData CollectConstellationData(ConstellationNameData name)
    {
        var data = new ConstellationData();

        // 별자리의 이름 등록
        data.Name = name;

        // 별자리 ID가 같은 것을 등록
        data.Position = constellationPositionData.FirstOrDefault(s => name.Id == s.Id);

        // 별자리 약칭이 같은 것을 등록
        data.Lines = constellationLineData.Where(s => name.Summary == s.Name).ToList();

        // 별자리 선이 사용하고 있는 별을 등록
        data.Stars = new List<StarData>();
        foreach (var line in data.Lines)
        {
            var start = starData.FirstOrDefault(s => s.Hip == line.StartHip);
            data.Stars.Add(start);
            var end = starData.FirstOrDefault(s => s.Hip == line.EndHip);
            data.Stars.Add(end);

            // 별자리로 사용되는 별
            start.UseConstellation = end.UseConstellation = true;
        }

        return data;
    }

    // 별자리의 작성
    void CreateConstellation()
    {
        // 각 별자리를 작성
        foreach (var data in constellationData)
        {
            var constellation = Instantiate(constellationPrefab);
            var drawConstellation = constellation.GetComponent<DrawConstellation>();

            drawConstellation.ConstellationData = data;

            // 자신의 자식으로 한다
            constellation.transform.SetParent(transform, false);
        }
    }
}