namespace SmartHome.Services;

public static class DataBasePathFinder
{
    public static string GetPath(string databaseName = "DataBase.sqlite.db")
    {
        var path = "";

        if(DeviceInfo.Platform == DevicePlatform.Android)
        {
            path = Path.Combine(FileSystem.AppDataDirectory, databaseName);
        }
        else if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            SQLitePCL.Batteries_V2.Init();
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "..",
                "Library",
                databaseName);
        }
        else
        {
            path = databaseName;
        }
        return path;
    }
}
