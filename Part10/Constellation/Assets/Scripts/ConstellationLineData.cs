public class ConstellationLineData : CsvData
{
    public string Name { get; set; }    // 별자리 약칭
    public int StartHip { get; set; }   // 선분 시작 HIP 번호
    public int EndHip { get; set; }     // 선분 종료 HIP 번호

    public override void SetData(string[] data)
    {
        Name = data[0];
        StartHip = int.Parse(data[1]);
        EndHip = int.Parse(data[2]);
    }
}