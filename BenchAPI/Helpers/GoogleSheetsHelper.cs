using benchAPI.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace benchAPI.Helpers;

public class GoogleSheetsHelper
{
    private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    public SheetsService? Service { get; set; }

    private const string ApplicationName = "Bench Project";
    public string SpreadsheetId = "19T0Er8gcFeGsE-ZlXlNQQcLb8HCojqtIdZNxUjAKgl0";
    public string SheetName = "stocks";

    public GoogleSheetsHelper()
    {
        InitializeService();
    }

    private void InitializeService()
    {
        var credential = GetCredentialsFromFile();
        Service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName
        });
    }

    private GoogleCredential GetCredentialsFromFile()
    {
        GoogleCredential credential;
        using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
        }

        return credential;
    }
}