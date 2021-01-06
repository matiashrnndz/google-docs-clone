using Domain;

namespace WebApi.Models.StyleClasses
{
    public class UpdateStyleClass : Model<StyleClass, UpdateStyleClass>
    {
        public StyleClass BasedOn { get; set; }

        public UpdateStyleClass()
        {

        }

        public UpdateStyleClass(StyleClass styleClass)
        {
            SetModel(styleClass);
        }

        public override StyleClass ToEntity() => new StyleClass()
        {
            BasedOn = this.BasedOn
        };

        protected override UpdateStyleClass SetModel(StyleClass styleClass)
        {
            BasedOn = styleClass.BasedOn;

            return this;
        }
    }
}