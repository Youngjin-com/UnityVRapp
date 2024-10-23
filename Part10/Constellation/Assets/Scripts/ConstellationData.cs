using System.Collections.Generic;

public class ConstellationData
{
    public ConstellationNameData Name;          // 별자리 이름의 데이터
    public ConstellationPositionData Position;  // 별자리 위치의 데이터
    public List<StarData> Stars;                // 별자리 데이터
    public List<ConstellationLineData> Lines;   // 별자리 선의 데이터
}