using Domain;

namespace WebApi.Models.Comments
{
    public class AddComment : Model<Comment, AddComment>
    {
        
        public string Text { get; set; }
        public int Rating { get; set; }

        public AddComment()
        {

        }

        public AddComment(Comment comment)
        {
            SetModel(comment);
        }

        public override Comment ToEntity() => new Comment()
        {
            Text = this.Text,
            Rating = this.Rating

        };

        protected override AddComment SetModel(Comment comment)
        {
            Text = comment.Text;
            Rating = comment.Rating;

            return this;
        }
    }
}