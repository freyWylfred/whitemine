using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace whitemine
{
    public class IssueDetailForm : Form
    {
        private readonly string _url;
        private readonly string _apiKey;
        private readonly int _issueId;
        private readonly TextBox _textBoxSubject;
        private readonly ComboBox _comboBoxStatus;
        private readonly ComboBox _comboBoxPriority;
        private readonly NumericUpDown _numericDoneRatio;
        private readonly TextBox _textBoxDescription;
        private readonly TextBox _textBoxNotes;
        private readonly TextBox _textBoxHistory;
        private readonly Button _buttonUpdate;
        private readonly Label _labelStatus;
        private readonly Label _labelHeaderTitle;
        private readonly Label _labelHeaderSubtitle;

        public IssueDetailForm(string url, string apiKey, int issueId)
        {
            _url = url;
            _apiKey = apiKey;
            _issueId = issueId;

            Text = "Issue #" + issueId;
            ClientSize = new Size(820, 660);
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(680, 560);
            Theme.ApplyForm(this);
            SuspendLayout();

            var header = Theme.CreateHeader("Issue #" + issueId, "Loading issue details...");
            _labelHeaderTitle = (Label)header.Controls[0];
            _labelHeaderSubtitle = (Label)header.Controls[1];
            Controls.Add(header);

            var body = new Panel { Dock = DockStyle.Fill, Padding = new Padding(24, 20, 24, 16), BackColor = Theme.Background };
            Controls.Add(body);
            body.BringToFront();

            var footer = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Theme.Surface, Padding = new Padding(24, 0, 24, 0) };
            var footerBorder = new Panel { Dock = DockStyle.Top, Height = 1, BackColor = Theme.Border };
            footer.Controls.Add(footerBorder);
            _buttonUpdate = new Button { Text = "Save Changes", Size = new Size(130, 34), Anchor = AnchorStyles.Right, Enabled = false };
            _buttonUpdate.Location = new Point(footer.ClientSize.Width - 24 - _buttonUpdate.Width, 13);
            _buttonUpdate.Click += buttonUpdate_Click;
            Theme.StylePrimaryButton(_buttonUpdate);
            footer.Controls.Add(_buttonUpdate);
            _labelStatus = new Label { Location = new Point(24, 20), Size = new Size(footer.ClientSize.Width - 200, 20), Anchor = AnchorStyles.Left | AnchorStyles.Right, Text = "Loading...", AutoEllipsis = true, TextAlign = ContentAlignment.MiddleLeft };
            Theme.BindStatus(_labelStatus);
            footer.Controls.Add(_labelStatus);
            Controls.Add(footer);

            var card = Theme.CreateCard();
            card.Dock = DockStyle.Fill;
            card.Padding = new Padding(20, 16, 20, 16);
            body.Controls.Add(card);

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 6, BackColor = Color.Transparent };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 12F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 12F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 42F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 58F));
            card.Controls.Add(layout);

            layout.Controls.Add(Theme.CreateFieldLabel("SUBJECT"), 0, 0);
            layout.Controls.Add(Theme.CreateFieldLabel("STATUS"), 2, 0);
            layout.Controls.Add(Theme.CreateFieldLabel("PRIORITY"), 4, 0);
            layout.Controls.Add(Theme.CreateFieldLabel("DONE %"), 5, 0);

            _textBoxSubject = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(0, 2, 0, 4) };
            Theme.StyleTextBox(_textBoxSubject);
            layout.Controls.Add(_textBoxSubject, 0, 1);

            _comboBoxStatus = new ComboBox { Dock = DockStyle.Fill, Margin = new Padding(0, 2, 0, 4), DropDownStyle = ComboBoxStyle.DropDownList, DisplayMember = "Name", ValueMember = "Id" };
            Theme.StyleComboBox(_comboBoxStatus);
            layout.Controls.Add(_comboBoxStatus, 2, 1);

            _comboBoxPriority = new ComboBox { Dock = DockStyle.Fill, Margin = new Padding(0, 2, 8, 4), DropDownStyle = ComboBoxStyle.DropDownList, DisplayMember = "Name", ValueMember = "Id" };
            Theme.StyleComboBox(_comboBoxPriority);
            layout.Controls.Add(_comboBoxPriority, 4, 1);

            _numericDoneRatio = new NumericUpDown { Dock = DockStyle.Fill, Margin = new Padding(0, 2, 0, 4), Minimum = 0, Maximum = 100, Increment = 10 };
            Theme.StyleNumeric(_numericDoneRatio);
            layout.Controls.Add(_numericDoneRatio, 5, 1);

            var descriptionLabel = Theme.CreateFieldLabel("DESCRIPTION");
            descriptionLabel.Margin = new Padding(0, 8, 0, 0);
            layout.Controls.Add(descriptionLabel, 0, 2);
            layout.SetColumnSpan(descriptionLabel, 6);
            _textBoxDescription = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 4), Multiline = true, ScrollBars = ScrollBars.Vertical, AcceptsReturn = true };
            Theme.StyleTextBox(_textBoxDescription);
            layout.Controls.Add(_textBoxDescription, 0, 3);
            layout.SetColumnSpan(_textBoxDescription, 6);

            var notesLabel = Theme.CreateFieldLabel("ADD NOTE");
            notesLabel.Margin = new Padding(0, 8, 0, 0);
            layout.Controls.Add(notesLabel, 0, 4);
            layout.SetColumnSpan(notesLabel, 6);
            _textBoxNotes = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 4), Multiline = true, ScrollBars = ScrollBars.Vertical, AcceptsReturn = true, PlaceholderText = "Write a comment to append to the issue history..." };
            Theme.StyleTextBox(_textBoxNotes);
            layout.Controls.Add(_textBoxNotes, 0, 5);
            layout.SetColumnSpan(_textBoxNotes, 6);

            var historyLabel = Theme.CreateFieldLabel("DETAILS & HISTORY");
            historyLabel.Margin = new Padding(0, 8, 0, 0);
            layout.Controls.Add(historyLabel, 0, 6);
            layout.SetColumnSpan(historyLabel, 6);
            _textBoxHistory = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(0), Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Both, WordWrap = false, BackColor = Theme.GridAltRow, ForeColor = Theme.SecondaryText, BorderStyle = BorderStyle.FixedSingle, Font = Theme.MonoFont };
            layout.Controls.Add(_textBoxHistory, 0, 7);
            layout.SetColumnSpan(_textBoxHistory, 6);

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ResumeLayout(false);
            PerformLayout();

            Load += IssueDetailForm_Load;
        }

        private HttpClient CreateClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(_url.TrimEnd('/') + "/"), Timeout = TimeSpan.FromSeconds(15) };
            client.DefaultRequestHeaders.Add("X-Redmine-API-Key", _apiKey);
            return client;
        }

        private async void IssueDetailForm_Load(object? sender, EventArgs e)
        {
            await LoadIssueAsync();
        }

        private async Task LoadIssueAsync()
        {
            try
            {
                using var client = CreateClient();

                using (var statusResponse = await client.GetAsync("issue_statuses.json"))
                {
                    if (statusResponse.IsSuccessStatusCode)
                    {
                        var statuses = await JsonSerializer.DeserializeAsync<StatusesResponse>(await statusResponse.Content.ReadAsStreamAsync());
                        _comboBoxStatus.DataSource = statuses?.Statuses ?? new List<NamedRef>();
                    }
                }

                using (var priorityResponse = await client.GetAsync("enumerations/issue_priorities.json"))
                {
                    if (priorityResponse.IsSuccessStatusCode)
                    {
                        var priorities = await JsonSerializer.DeserializeAsync<PrioritiesResponse>(await priorityResponse.Content.ReadAsStreamAsync());
                        _comboBoxPriority.DataSource = priorities?.Priorities ?? new List<NamedRef>();
                    }
                }

                using var response = await client.GetAsync("issues/" + _issueId + ".json?include=journals");
                if (!response.IsSuccessStatusCode)
                {
                    _labelStatus.Text = "Failed to load issue: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                    return;
                }

                var stream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<IssueResponse>(stream);
                var issue = result?.Issue;
                if (issue == null)
                {
                    _labelStatus.Text = "Issue not found.";
                    return;
                }

                Text = "Issue #" + issue.Id + " - " + issue.Subject;
                _labelHeaderTitle.Text = "#" + issue.Id + "  " + issue.Subject;
                _labelHeaderSubtitle.Text = (issue.Project?.Name ?? "") + "  •  " + (issue.Tracker?.Name ?? "") + "  •  " + (issue.Author?.Name ?? "") + "  •  Updated " + issue.UpdatedOn?.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
                _textBoxSubject.Text = issue.Subject ?? string.Empty;
                _textBoxDescription.Text = NormalizeNewLines(issue.Description);
                _numericDoneRatio.Value = Math.Clamp(issue.DoneRatio, 0, 100);
                if (issue.Status != null) _comboBoxStatus.SelectedValue = issue.Status.Id;
                if (issue.Priority != null) _comboBoxPriority.SelectedValue = issue.Priority.Id;
                _textBoxNotes.Text = string.Empty;
                _textBoxHistory.Text = FormatIssue(issue);
                _textBoxHistory.Select(0, 0);
                _labelStatus.Text = "Loaded.";
                _buttonUpdate.Enabled = true;
            }
            catch (Exception ex)
            {
                _labelStatus.Text = "Failed to load issue: " + ex.Message;
            }
        }

        private async void buttonUpdate_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_textBoxSubject.Text))
            {
                _labelStatus.Text = "Subject is required.";
                return;
            }

            _buttonUpdate.Enabled = false;
            _labelStatus.Text = "Updating...";
            try
            {
                var issue = new Dictionary<string, object?>
                {
                    ["subject"] = _textBoxSubject.Text.Trim(),
                    ["description"] = _textBoxDescription.Text.Replace("\r\n", "\n"),
                    ["done_ratio"] = (int)_numericDoneRatio.Value
                };
                if (_comboBoxStatus.SelectedValue is int statusId) issue["status_id"] = statusId;
                if (_comboBoxPriority.SelectedValue is int priorityId) issue["priority_id"] = priorityId;
                if (!string.IsNullOrWhiteSpace(_textBoxNotes.Text)) issue["notes"] = _textBoxNotes.Text.Replace("\r\n", "\n");

                var payload = JsonSerializer.Serialize(new Dictionary<string, object?> { ["issue"] = issue });
                using var client = CreateClient();
                using var content = new StringContent(payload, Encoding.UTF8, "application/json");
                using var response = await client.PutAsync("issues/" + _issueId + ".json", content);
                if (!response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    _labelStatus.Text = "Update failed: " + (int)response.StatusCode + " " + response.ReasonPhrase + " " + body;
                    _buttonUpdate.Enabled = true;
                    return;
                }

                await LoadIssueAsync();
                _labelStatus.Text = "Updated successfully.";
            }
            catch (Exception ex)
            {
                _labelStatus.Text = "Update failed: " + ex.Message;
                _buttonUpdate.Enabled = true;
            }
        }

        private static string NormalizeNewLines(string? text)
        {
            return (text ?? string.Empty).Replace("\r\n", "\n").Replace("\n", "\r\n");
        }

        private static string FormatIssue(IssueDetail issue)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Project:     " + issue.Project?.Name);
            sb.AppendLine("Tracker:     " + issue.Tracker?.Name);
            sb.AppendLine("Author:      " + issue.Author?.Name);
            sb.AppendLine("Assigned to: " + issue.AssignedTo?.Name);
            sb.AppendLine("Start date:  " + issue.StartDate);
            sb.AppendLine("Due date:    " + issue.DueDate);
            sb.AppendLine("Created:     " + issue.CreatedOn?.ToLocalTime().ToString("yyyy-MM-dd HH:mm"));
            sb.AppendLine("Updated:     " + issue.UpdatedOn?.ToLocalTime().ToString("yyyy-MM-dd HH:mm"));

            if (issue.Journals != null && issue.Journals.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("History:");
                foreach (var journal in issue.Journals)
                {
                    if (string.IsNullOrWhiteSpace(journal.Notes))
                    {
                        continue;
                    }
                    sb.AppendLine("----------------------------------------");
                    sb.AppendLine(journal.User?.Name + " (" + journal.CreatedOn?.ToLocalTime().ToString("yyyy-MM-dd HH:mm") + ")");
                    sb.AppendLine(journal.Notes);
                }
            }

            return NormalizeNewLines(sb.ToString());
        }

        private sealed class StatusesResponse
        {
            [JsonPropertyName("issue_statuses")]
            public List<NamedRef>? Statuses { get; set; }
        }

        private sealed class PrioritiesResponse
        {
            [JsonPropertyName("issue_priorities")]
            public List<NamedRef>? Priorities { get; set; }
        }

        private sealed class IssueResponse
        {
            [JsonPropertyName("issue")]
            public IssueDetail? Issue { get; set; }
        }

        private sealed class IssueDetail
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("subject")]
            public string? Subject { get; set; }

            [JsonPropertyName("description")]
            public string? Description { get; set; }

            [JsonPropertyName("project")]
            public NamedRef? Project { get; set; }

            [JsonPropertyName("tracker")]
            public NamedRef? Tracker { get; set; }

            [JsonPropertyName("status")]
            public NamedRef? Status { get; set; }

            [JsonPropertyName("priority")]
            public NamedRef? Priority { get; set; }

            [JsonPropertyName("author")]
            public NamedRef? Author { get; set; }

            [JsonPropertyName("assigned_to")]
            public NamedRef? AssignedTo { get; set; }

            [JsonPropertyName("start_date")]
            public string? StartDate { get; set; }

            [JsonPropertyName("due_date")]
            public string? DueDate { get; set; }

            [JsonPropertyName("done_ratio")]
            public int DoneRatio { get; set; }

            [JsonPropertyName("created_on")]
            public DateTime? CreatedOn { get; set; }

            [JsonPropertyName("updated_on")]
            public DateTime? UpdatedOn { get; set; }

            [JsonPropertyName("journals")]
            public List<Journal>? Journals { get; set; }
        }

        private sealed class Journal
        {
            [JsonPropertyName("user")]
            public NamedRef? User { get; set; }

            [JsonPropertyName("notes")]
            public string? Notes { get; set; }

            [JsonPropertyName("created_on")]
            public DateTime? CreatedOn { get; set; }
        }

        private sealed class NamedRef
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("name")]
            public string? Name { get; set; }
        }
    }
}
