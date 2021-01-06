using Domain;

namespace WebApi.Models.Formats
{
    public class AddFormat : Model<Format, AddFormat>
    {
        public string Name { get; set; }

        public AddFormat()
        {

        }

        public AddFormat(Format format)
        {
            SetModel(format);
        }

        public override Format ToEntity() => new Format()
        {
            Name = this.Name
        };

        protected override AddFormat SetModel(Format format)
        {
            Name = format.Name;

            return this;
        }
    }
}