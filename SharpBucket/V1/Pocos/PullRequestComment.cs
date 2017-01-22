using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    public class PullRequestComment
    {
        public int? parent_id { get; set; }
        public string filename { get; set; }
        public string content { get; set; }
        public int? line_from { get; set; }
        public int? line_to { get; set; }
        public bool is_spam { get; set; }
    }
}