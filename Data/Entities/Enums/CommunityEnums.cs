namespace Data.Enums
{
    public enum PostStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2,
        Deleted = 3
    }

    public enum CommentStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2,
        Deleted = 3
    }

    public enum ReactionType
    {
        Heart = 0,
        Hug = 1,
        Pray = 2,
        ThumbsUp = 3
    }

    public enum ReactionTargetType
    {
        Post = 0,
        Comment = 1
    }

    public enum ReportTargetType
    {
        Post = 0,
        Comment = 1
    }

    public enum ReportStatus
    {
        Open = 0,
        InReview = 1,
        Resolved = 2
    }
}

