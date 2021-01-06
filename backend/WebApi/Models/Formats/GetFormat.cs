using Domain;

namespace WebApi.Models.Formats
{
    public class GetFormat : Model<Format, GetFormat>
    {
        public string Name { get; set; }

        public GetFormat()
        {

        }

        public GetFormat(Format format)
        {
            SetModel(format);
        }

        public override Format ToEntity() => new Format()
        {
            Name = this.Name
        };

        protected override GetFormat SetModel(Format format)
        {
            Name = format.Name;

            return this;
        }
    }
}