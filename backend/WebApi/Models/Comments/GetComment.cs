using Domain;

namespace WebApi.Models.Comments
{
    public class GetComment : Model<Comment, GetComment>
    {
        public User Commenter { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

        public GetComment()
        {

        }

        public GetComment(Comment comment)
        {
            SetModel(comment);
        }

        public override Comment ToEntity() => new Comment()
        {
            Text = this.Text,
            Rating = this.Rating,
            Commenter = this.Commenter

        };

        protected override GetComment SetModel(Comment comment)
        {
            Text = comment.Text;
            Rating = comment.Rating;
            Commenter = comment.Commenter;

            return this;
        }
    }
}