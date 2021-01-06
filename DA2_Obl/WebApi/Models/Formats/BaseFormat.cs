using Domain;

namespace WebApi.Models.Formats
{
    public class BaseFormat : Model<Format, BaseFormat>
    {
        public string Name { get; set; }

        public BaseFormat()
        {

        }

        public BaseFormat(Format format)
        {
            SetModel(format);
        }

        public override Format ToEntity() => new Format()
        {
            Name = this.Name
        };

        protected override BaseFormat SetModel(Format format)
        {
            Name = format.Name;

            return this;
        }
    }
}