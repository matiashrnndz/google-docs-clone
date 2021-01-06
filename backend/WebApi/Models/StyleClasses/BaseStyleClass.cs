using Domain;

namespace WebApi.Models.StyleClasses
{
    public class BaseStyleClass : Model<StyleClass, BaseStyleClass>
    {
        public string Name { get; set; }
        public StyleClass BasedOn { get; set; }

        public BaseStyleClass()
        {

        }

        public BaseStyleClass(StyleClass styleClass)
        {
            SetModel(styleClass);
        }

        public override StyleClass ToEntity() => new StyleClass()
        {
            Name = this.Name,
            BasedOn = this.BasedOn
        };

        protected override BaseStyleClass SetModel(StyleClass styleClass)
        {
            Name = styleClass.Name;
            BasedOn = styleClass.BasedOn;

            return this;
        }
    }
}