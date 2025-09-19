namespace ImageManagement.Domain.FolderType
{
    public static class FolderFactory
    {
        public static BaseFolder CreateFolder(string key)
        {
            return key switch
            {
                "chi" => new ChiFolder($"Folder {key}", $"Contains {key} images in {key} Folder"),
                "thu" => new ThuFolder($"Folder {key}", $"Contains {key} images in {key} Folder"),
                _ => new OtherFolder("Folder Other", "Contains uncategorized images in Others Folder")
            };
        }
    }
}
