using System.Net.Http;
using System.Text.Json;

namespace whitemine
{
    public partial class Form1 : Form
    {
        private static readonly string SettingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "whitemine", "settings.json");

        public Form1()
        {
            InitializeComponent();
            Theme.ApplyForm(this);
            Theme.StyleTextBox(textBoxUrl);
            Theme.StyleTextBox(textBoxApiKey);
            Theme.StyleSecondaryButton(buttonSave);
            Theme.StylePrimaryButton(buttonTest);
            Theme.StylePrimaryButton(buttonMyIssues);
            Theme.StyleGrid(dataGridViewIssues);
            Theme.BindStatus(labelStatus);
            dataGridViewIssues.DataBindingComplete += dataGridViewIssues_DataBindingComplete;
        }

        private void dataGridViewIssues_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dataGridViewIssues.Columns["Id"] is DataGridViewColumn idColumn)
            {
                idColumn.HeaderText = "#";
                idColumn.FillWeight = 8;
            }
            if (dataGridViewIssues.Columns["Project"] is DataGridViewColumn projectColumn) projectColumn.FillWeight = 16;
            if (dataGridViewIssues.Columns["Tracker"] is DataGridViewColumn trackerColumn) trackerColumn.FillWeight = 12;
            if (dataGridViewIssues.Columns["Status"] is DataGridViewColumn statusColumn) statusColumn.FillWeight = 14;
            if (dataGridViewIssues.Columns["Subject"] is DataGridViewColumn subjectColumn) subjectColumn.FillWeight = 34;
            if (dataGridViewIssues.Columns["Updated"] is DataGridViewColumn updatedColumn) updatedColumn.FillWeight = 16;
            dataGridViewIssues.ClearSelection();
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            try
            {
                if (File.Exists(SettingsPath))
                {
                    var json = File.ReadAllText(SettingsPath);
                    var settings = JsonSerializer.Deserialize<RedmineSettings>(json);
                    if (settings != null)
                    {
                        textBoxUrl.Text = settings.Url ?? string.Empty;
                        textBoxApiKey.Text = settings.ApiKey ?? string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                labelStatus.Text = "Failed to load settings: " + ex.Message;
            }
        }

        private void buttonSave_Click(object? sender, EventArgs e)
        {
            try
            {
                var settings = new RedmineSettings
                {
                    Url = textBoxUrl.Text.Trim(),
                    ApiKey = textBoxApiKey.Text.Trim()
                };
                Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath)!);
                File.WriteAllText(SettingsPath, JsonSerializer.Serialize(settings));
                labelStatus.Text = "Settings saved.";
            }
            catch (Exception ex)
            {
                labelStatus.Text = "Failed to save settings: " + ex.Message;
            }
        }

        private async void buttonTest_Click(object? sender, EventArgs e)
        {
            var url = textBoxUrl.Text.Trim();
            var apiKey = textBoxApiKey.Text.Trim();
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(apiKey))
            {
                labelStatus.Text = "URL and API Key are required.";
                return;
            }

            buttonTest.Enabled = false;
            labelStatus.Text = "Testing connection...";
            try
            {
                using var client = new HttpClient { BaseAddress = new Uri(url.TrimEnd('/') + "/"), Timeout = TimeSpan.FromSeconds(15) };
                client.DefaultRequestHeaders.Add("X-Redmine-API-Key", apiKey);
                using var response = await client.GetAsync("users/current.json");
                labelStatus.Text = response.IsSuccessStatusCode
                    ? "Connection succeeded."
                    : "Connection failed: " + (int)response.StatusCode + " " + response.ReasonPhrase;
            }
            catch (Exception ex)
            {
                labelStatus.Text = "Connection failed: " + ex.Message;
            }
            finally
            {
                buttonTest.Enabled = true;
            }
        }

        private async void buttonMyIssues_Click(object? sender, EventArgs e)
        {
            var url = textBoxUrl.Text.Trim();
            var apiKey = textBoxApiKey.Text.Trim();
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(apiKey))
            {
                labelStatus.Text = "URL and API Key are required.";
                return;
            }

            buttonMyIssues.Enabled = false;
            labelStatus.Text = "Loading issues...";
            try
            {
                using var client = new HttpClient { BaseAddress = new Uri(url.TrimEnd('/') + "/"), Timeout = TimeSpan.FromSeconds(15) };
                client.DefaultRequestHeaders.Add("X-Redmine-API-Key", apiKey);
                using var response = await client.GetAsync("issues.json?assigned_to_id=me&status_id=open&sort=updated_on:desc&limit=20");
                if (!response.IsSuccessStatusCode)
                {
                    labelStatus.Text = "Failed to load issues: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                    return;
                }

                var stream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<IssuesResponse>(stream);
                var issues = result?.Issues ?? new List<Issue>();
                dataGridViewIssues.DataSource = issues.Select(i => new
                {
                    Id = i.Id,
                    Project = i.Project?.Name,
                    Tracker = i.Tracker?.Name,
                    Status = i.Status?.Name,
                    Subject = i.Subject,
                    Updated = i.UpdatedOn?.ToLocalTime().ToString("yyyy-MM-dd HH:mm")
                }).ToList();
                labelStatus.Text = issues.Count + " issue(s) assigned to me.";
            }
            catch (Exception ex)
            {
                labelStatus.Text = "Failed to load issues: " + ex.Message;
            }
            finally
            {
                buttonMyIssues.Enabled = true;
            }
        }

        private void dataGridViewIssues_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var idValue = dataGridViewIssues.Rows[e.RowIndex].Cells["Id"].Value;
            if (idValue is not int issueId)
            {
                return;
            }

            var detailForm = new IssueDetailForm(textBoxUrl.Text.Trim(), textBoxApiKey.Text.Trim(), issueId);
            detailForm.Show(this);
        }

        private sealed class RedmineSettings
        {
            public string? Url { get; set; }
            public string? ApiKey { get; set; }
        }

        private sealed class IssuesResponse
        {
            [System.Text.Json.Serialization.JsonPropertyName("issues")]
            public List<Issue>? Issues { get; set; }
        }

        private sealed class Issue
        {
            [System.Text.Json.Serialization.JsonPropertyName("id")]
            public int Id { get; set; }

            [System.Text.Json.Serialization.JsonPropertyName("subject")]
            public string? Subject { get; set; }

            [System.Text.Json.Serialization.JsonPropertyName("project")]
            public NamedRef? Project { get; set; }

            [System.Text.Json.Serialization.JsonPropertyName("tracker")]
            public NamedRef? Tracker { get; set; }

            [System.Text.Json.Serialization.JsonPropertyName("status")]
            public NamedRef? Status { get; set; }

            [System.Text.Json.Serialization.JsonPropertyName("updated_on")]
            public DateTime? UpdatedOn { get; set; }
        }

        private sealed class NamedRef
        {
            [System.Text.Json.Serialization.JsonPropertyName("id")]
            public int Id { get; set; }

            [System.Text.Json.Serialization.JsonPropertyName("name")]
            public string? Name { get; set; }
        }
    }
}
