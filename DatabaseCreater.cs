using Postgrest.Responses;

public class DatabaseCreater
{
    public static async Task CreateDatabase()
    {
        ModeledResponse<SettingsDataModel> response = await Supabase.Client.Instance.From<SettingsDataModel>().Get();
        SettingsDataModel databaseActiveKeyValuePair = response.Models[0];

        if(databaseActiveKeyValuePair.KeyValue == "false")
            throw new Exception();
    }
}