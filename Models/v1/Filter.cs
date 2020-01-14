namespace StudentsAPI.Models.v1
{
    public class Filter
    {
        public FilterType Type { get; set; }

        public string[] Values { get; set; }
    }

    public enum FilterType
    {
        None,
        Equals,
        Contains,
        StartsWith,
        EndsWith
    }
}
