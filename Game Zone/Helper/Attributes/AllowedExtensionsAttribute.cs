namespace Game_Zone.Helper.Attributes
{
    public class AllowedExtensionsAttribute :ValidationAttribute
    {
        private readonly string _allowedExtension;
        private readonly int _maxsize;

        public AllowedExtensionsAttribute(string allowedExtension , int maxsize)
        {
            _allowedExtension = allowedExtension;
            _maxsize = maxsize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            
            var file = value as IFormFile;

            if (file is not null) {

                if(file.Length <_maxsize){
                   var extension=Path.GetExtension(file.FileName).ToLower();

                   var IsAllowed=_allowedExtension.Split(",").Contains(extension);
                    if (!IsAllowed)
                    {
                        return new ValidationResult($"Only{_allowedExtension} are allowed ");
                    }
                }
                else
                {
                    return new ValidationResult($"Max size is allowed {_maxsize} bytes ");
                }
            }
                    return ValidationResult.Success;
        }

    }
}
