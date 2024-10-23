using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CsvLoader<TCsvData> where TCsvData : CsvData, new()
{
    // TextAsset 데이터 읽기
    public static List<TCsvData> LoadData(TextAsset csvText)
    {
        var data = new List<TCsvData>();                // 리스트 작성
        var reader = new StringReader(csvText.text);    // 문자열 읽기

        // 1행씩 데이터의 끝까지 처리를 실시
        while (reader.Peek() > -1)
        {
            // 1행 읽기
            var line = reader.ReadLine();
            // 데이터 작성
            var csvData = new TCsvData();
            // ,로 구분한 데이터의 배열을 작성하고 데이터를 등록
            csvData.SetData(line.Split(','));
            // 리스트에 등록
            data.Add(csvData);
        }
        return data;
    }
}