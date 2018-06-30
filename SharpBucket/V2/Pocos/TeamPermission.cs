namespace SharpBucket.V2.Pocos
{
    public class TeamPermission
    {
        public string permission { get; set; }
        public User user { get; set; }
        public Team team { get; set; }
    }
}