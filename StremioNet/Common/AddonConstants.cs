namespace StremioNet.Common;

public static class AddonConstants
{
    public static class Type
    {
        public const string Movie = "movie";
        public const string Series = "series";
        public const string Channel = "channel";
        public const string Anime = "anime";
        public const string Tv = "tv";
        public const string Game = "games";
    }
    
    public static class ResourcesName
    {
        public const string Catalog = "catalog";
        public const string Meta = "meta";
        public const string AddonCatalog = "addon_catalog";
        public const string Stream = "stream";
        public const string Subtitle = "subtitles";
    }

    public static class ExtraName
    {
        public const string Genre = "genre";
        public const string Search = "search";
        public const string Skip = "skip";

    }

    public static Dictionary<CatalogTypeEnum, string> CatalogTypeDict = new()
    {
        { CatalogTypeEnum.Movie, Type.Movie },
        { CatalogTypeEnum.Series, Type.Series },
        { CatalogTypeEnum.Channel, Type.Channel },
        { CatalogTypeEnum.Anime, Type.Anime},
        { CatalogTypeEnum.Tv, Type.Tv },
        { CatalogTypeEnum.Games, Type.Game }
    };
}