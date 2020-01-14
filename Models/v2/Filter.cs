namespace StudentsAPI.Models.v2
{
    public class Filter
    {
        public FilterType Type { get; set; }

        public string[] Values { get; set; }

        public string Field { get; set; }
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
