namespace TestFramework
{
    /// <summary>
    /// Use one or more data objects as a way to pass data between features and the back end page objects.
    /// For more information on context injection, see: http://www.specflow.org/documentation/Context-Injection/
    /// </summary>
    public class AmazonData
    {
        //public string data1;
        //public bool data2 {get; set;}
        public string searchTerm { get; set; }
        public int searchResultNum { get; set; }
        public string searchResultTitle { get; set; }
    }
}
