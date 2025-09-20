namespace ImageManagement.Domain.AggregatesModel.FolderTypeAggregate
{
    public static class FolderTypeFactory
    {
        public static FolderType CreateFolder(string key)
        {
            return key switch
            {
                "chi" => new ChiFolderType($"Folder {key}", $"Contains {key} images in {key} Folder"),
                "thu" => new ThuFolderType($"Folder {key}", $"Contains {key} images in {key} Folder"),
                _ => new OtherFolderType("Folder Other", "Contains uncategorized images in Others Folder")
            };
        }
    }
}
