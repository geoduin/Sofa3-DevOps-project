namespace Sofa3Devops.Domain
{
    public class CommentThread
    {
        public BacklogItem RelevantBacklogItem { get; set; }
        public List<Response> Responses { get; set; }
        public Member PostedMember { get; set; }
    }
}