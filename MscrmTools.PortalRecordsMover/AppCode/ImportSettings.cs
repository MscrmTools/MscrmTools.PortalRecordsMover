namespace MscrmTools.PortalRecordsMover.AppCode
{
    internal class ImportSettings
    {
        public bool CleanWebFiles { get; internal set; }
        public bool CreateOnlyNewSiteSettings { get; internal set; }
        public bool DeactivateWebPagePlugins { get; set; }
        public bool RemoveJavaScriptFileRestriction { get; internal set; }
    }
}