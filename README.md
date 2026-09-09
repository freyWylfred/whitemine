# whitemine

A modern, lightweight Windows desktop client for Redmine, built with .NET 10 and Windows Forms.

## Features

- **Connection settings** – Configure your Redmine URL and API key, with a one-click connection test.
- **My Issues** – Fetch the 20 most recently updated issues assigned to you (open issues only).
- **Issue details** – Double-click any issue to open a dedicated detail window.
- **Edit & update** – Modify subject, status, priority, done ratio, description, and add notes, then push the changes back to Redmine.
- **Modern UI** – A clean, color-rich, high-DPI-aware interface with a shared theming layer.

## Requirements

- Windows
- [.NET 10 SDK](https://dotnet.microsoft.com/) (or later)
- A Redmine instance with the REST API enabled and a personal API key

## Getting Started

1. Clone the repository:
   ```powershell
   git clone <repository-url>
   cd whitemine
   ```
2. Build and run:
   ```powershell
   dotnet run --project whitemine
   ```
3. Enter your **Redmine URL** and **API Key**, then click **Test Connection** and **Save Settings**.
4. Click **Refresh** under *My Issues* to load your assigned issues.
5. Double-click an issue to view and edit its details, then click **Save Changes** to update Redmine.

## Configuration

Settings are stored locally at:

```
%AppData%\whitemine\settings.json
```

This file contains your API key and is excluded from source control via `.gitignore`.

## Project Structure

- `whitemine/Program.cs` – Application entry point.
- `whitemine/Form1.cs` / `Form1.Designer.cs` – Main window (settings & issue list).
- `whitemine/IssueDetailForm.cs` – Issue detail view and update window.
- `whitemine/Theme.cs` – Shared visual styling and DPI-aware helpers.

## License

This project is provided as-is, without warranty of any kind.
