namespace Audit.CRUD.Settings
{
    /// <summary>
    /// Connection settings with elasticsearch pool.
    /// </summary>
    public class ElasticsearchSettings
    {
        /// <summary>
        /// Name of the index that will be created in elasticsearch.
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// Uri where elasticsearch is hosted.
        /// </summary>
        public string Uri { get; set; }
    }
}
