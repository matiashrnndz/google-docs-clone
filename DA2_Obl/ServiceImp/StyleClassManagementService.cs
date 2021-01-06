using Domain;
using Exception;
using Repository;
using Service;
using System;
using System.Collections.Generic;

namespace ServiceImp
{
    public class StyleClassManagementService : IStyleClassManagementService
    {
        internal IStyleClassRepository StyleClassRepository { get; set; }

        public IEnumerable<StyleClass> GetAll()
        {
            return StyleClassRepository.GetAll();
        }

        public StyleClass GetByName(string styleClassName)
        {
            if (StyleClassRepository.Exists(styleClassName))
            {
                return StyleClassRepository.GetByName(styleClassName);
            }
            else
            {
                throw new MissingStyleClassException("The style class is not in the database.");
            }
        }

        public StyleClass Add(StyleClass styleClass)
        {
            if(styleClass == null)
            {
                throw new InvalidStyleClassException("The style class is null.");
            }
            if (!StyleClassRepository.Exists(styleClass.Name))
            {
                if (NoRedundantDependency(styleClass))
                {
                    StyleClassRepository.Add(styleClass);
                    return styleClass;
                }
                else
                {
                    throw new RedundantStyleClassException("This style class has redundant dependencies");
                }
            }
            else
            {
                throw new ExistingStyleClassException("This style class already exists on the database.");
            }
        }

        public void Delete(string styleClassName)
        {
            if (StyleClassRepository.Exists(styleClassName))
            {
                IEnumerable<StyleClass> styleClassesInRepository = StyleClassRepository.GetAll();
                foreach(StyleClass styleClassInRepository in styleClassesInRepository)
                {
                    if(styleClassInRepository.BasedOn != null && styleClassInRepository.BasedOn.Name == styleClassName)
                    {
                        throw new InvalidStyleClassException("The style class is someone´s father.");
                    }
                }

                StyleClassRepository.Delete(styleClassName);
            }
            else
            {
                throw new MissingStyleClassException("The style class is not in the database.");
            }
        }

        private bool NoRedundantDependency(StyleClass styleClass)
        {
            List<StyleClass> recursiveList = new List<StyleClass>();

            recursiveList.Add(styleClass);

            if (!VerifyRedundantDependency(recursiveList, styleClass))
            {
                return true;
            }
            else return false;

        }

        private bool VerifyRedundantDependency(List<StyleClass> recursiveList, StyleClass currentStyleClass)
        {
            bool ok;
            if (currentStyleClass.BasedOn == null)
            {
                ok = false;
            }
            else if (recursiveList.Contains(currentStyleClass.BasedOn))
            {
                ok = true;
            }
            else
            {
                recursiveList.Add(currentStyleClass.BasedOn);
                ok = VerifyRedundantDependency(recursiveList, currentStyleClass.BasedOn);
            }
            return ok;
        }

        public bool Exists(string styleClassName)
        {
            return StyleClassRepository.Exists(styleClassName);
        }

        public void Update(string styleClass_name, StyleClass styleClass)
        {
            StyleClass styleClassToUpdate = StyleClassRepository.GetByName(styleClass_name);

            if(styleClass != null)
            {
                if (styleClass.BasedOn == null || (styleClass.BasedOn != null && StyleClassRepository.Exists(styleClass.BasedOn.Name)))
                {
                    styleClassToUpdate.BasedOn = styleClass.BasedOn;
                }
            }

            StyleClassRepository.Update(styleClassToUpdate);
        }
    }
}
