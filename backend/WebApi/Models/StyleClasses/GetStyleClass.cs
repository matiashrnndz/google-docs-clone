using Domain;

namespace WebApi.Models.StyleClasses
{
    public class GetStyleClass : Model<StyleClass, GetStyleClass>
    {
        public string Name { get; set; }
        public StyleClass BasedOn { get; set; }

        public GetStyleClass()
        {

        }

        public GetStyleClass(StyleClass styleClass)
        {
            SetModel(styleClass);
        }

        public override StyleClass ToEntity() => new StyleClass()
        {
            Name = this.Name,
            BasedOn = this.BasedOn
        };

        protected override GetStyleClass SetModel(StyleClass styleClass)
        {
            Name = styleClass.Name;
            BasedOn = styleClass.BasedOn;

            return this;
        }
    }
}