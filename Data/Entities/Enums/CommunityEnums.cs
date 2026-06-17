namespace Data.Enums
{
    /// <summary>
    /// Defines the moderation status of a post in the community
    /// </summary>
    public enum PostStatus
    {
        /// <summary>
        /// Post is awaiting moderator approval
        /// </summary>
        Pending = 0,
        /// <summary>
        /// Post has been approved and is visible to the community
        /// </summary>
        Approved = 1,
        /// <summary>
        /// Post has been rejected by a moderator
        /// </summary>
        Rejected = 2,
        /// <summary>
        /// Post has been deleted by the author or moderator
        /// </summary>
        Deleted = 3
    }

    /// <summary>
    /// Defines the moderation status of a comment in the community
    /// </summary>
    public enum CommentStatus
    {
        /// <summary>
        /// Comment is awaiting moderator approval
        /// </summary>
        Pending = 0,
        /// <summary>
        /// Comment has been approved and is visible to the community
        /// </summary>
        Approved = 1,
        /// <summary>
        /// Comment has been rejected by a moderator
        /// </summary>
        Rejected = 2,
        /// <summary>
        /// Comment has been deleted by the author or moderator
        /// </summary>
        Deleted = 3
    }

    /// <summary>
    /// Defines the types of reactions users can add to posts or comments
    /// </summary>
    public enum ReactionType
    {
        /// <summary>
        /// Heart reaction - shows love or appreciation
        /// </summary>
        Heart = 0,
        /// <summary>
        /// Hug reaction - shows support and comfort
        /// </summary>
        Hug = 1,
        /// <summary>
        /// Pray reaction - shows prayers or spiritual support
        /// </summary>
        Pray = 2,
        /// <summary>
        /// Thumbs up reaction - shows agreement or approval
        /// </summary>
        ThumbsUp = 3
    }

    /// <summary>
    /// Defines the types of content that can receive reactions
    /// </summary>
    public enum ReactionTargetType
    {
        /// <summary>
        /// Reaction is on a post
        /// </summary>
        Post = 0,
        /// <summary>
        /// Reaction is on a comment
        /// </summary>
        Comment = 1
    }

    /// <summary>
    /// Defines the types of content that can be reported
    /// </summary>
    public enum ReportTargetType
    {
        /// <summary>
        /// Report is for a post
        /// </summary>
        Post = 0,
        /// <summary>
        /// Report is for a comment
        /// </summary>
        Comment = 1
    }

    /// <summary>
    /// Defines the status of a content report
    /// </summary>
    public enum ReportStatus
    {
        /// <summary>
        /// Report has been submitted and is awaiting review
        /// </summary>
        Open = 0,
        /// <summary>
        /// Report is currently being reviewed by moderators
        /// </summary>
        InReview = 1,
        /// <summary>
        /// Report has been resolved and action has been taken
        /// </summary>
        Resolved = 2
    }
}

