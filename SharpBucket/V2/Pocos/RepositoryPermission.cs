namespace SharpBucket.V2.Pocos
{
    public class RepositoryPermission
    {
        public string permission { get; set; }
        public User user { get; set; }
        public Repository repository { get; set; }
    }
}