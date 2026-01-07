namespace HighCourtRajCauseList.Dto.shared;

public class ResponseWithoutPaginationModel
{
    public bool Status { get; set; }
    public string? Message { get; set; }
    public IEnumerable<object>? Data { get; set; }
}
