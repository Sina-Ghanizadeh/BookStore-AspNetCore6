namespace UtilityProject.Application
{
    public static class ValidationMessage
    {
        public const string IsRequired = "این مقدار نمی تواند خالی باشد .";
        public const string MaxFileSize = "حجم فایل بیش از حد مجاز است . ";
        public const string FileExtension = "فرمت فایل غیر مجاز است . ";

        public const string NotMatchPassword = "رمز عبور با تکرار آن مغایرت دارد .";
    }
}
