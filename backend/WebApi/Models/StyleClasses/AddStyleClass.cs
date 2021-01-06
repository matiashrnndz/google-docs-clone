using Domain;

namespace WebApi.Models.StyleClasses
{
    public class AddStyleClass : Model<StyleClass, AddStyleClass>
    {
        public string Name { get; set; }
        public StyleClass BasedOn { get; set; }

        public AddStyleClass()
        {

        }

        public AddStyleClass(StyleClass styleClass)
        {
            SetModel(styleClass);
        }

        public override StyleClass ToEntity() => new StyleClass()
        {
            Name = this.Name,
            BasedOn = this.BasedOn
        };

        protected override AddStyleClass SetModel(StyleClass styleClass)
        {
            Name = styleClass.Name;
            BasedOn = styleClass.BasedOn;

            return this;
        }
    }
}