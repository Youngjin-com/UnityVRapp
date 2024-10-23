public class StarData : CsvData
{
    public int Hip { get; set; }                    // HIP 번호
    public float RightAscension { get; set; }       // 적경
    public float Declination { get; set; }          // 적위
    public float ApparentMagnitude { get; set; }    // 시등급
    public string ColorType;                        // 색
    public bool UseConstellation;                   // 별자리로 사용되는 별인지

    public override void SetData(string[] data)
    {
        Hip = int.Parse(data[0]);
        RightAscension = RightAscensionToDegree(int.Parse(data[1]), int.Parse(data[2]), float.Parse(data[3]));
        Declination = DeclinationToDegree(int.Parse(data[4]), int.Parse(data[5]), float.Parse(data[6]));
        ApparentMagnitude = float.Parse(data[7]);
        ColorType = data[13].Substring(0, 1);
    }
}