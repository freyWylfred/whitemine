using System.Drawing.Drawing2D;

namespace whitemine
{
    internal static class Theme
    {
        public static readonly Color Background = Color.FromArgb(244, 246, 250);
        public static readonly Color Surface = Color.White;
        public static readonly Color Border = Color.FromArgb(226, 232, 240);
        public static readonly Color HeaderBackground = Color.FromArgb(30, 41, 59);
        public static readonly Color HeaderText = Color.White;
        public static readonly Color HeaderSubText = Color.FromArgb(148, 163, 184);
        public static readonly Color Primary = Color.FromArgb(37, 99, 235);
        public static readonly Color PrimaryHover = Color.FromArgb(29, 78, 216);
        public static readonly Color PrimaryText = Color.White;
        public static readonly Color SecondaryBackground = Color.White;
        public static readonly Color SecondaryHover = Color.FromArgb(241, 245, 249);
        public static readonly Color SecondaryText = Color.FromArgb(51, 65, 85);
        public static readonly Color Text = Color.FromArgb(15, 23, 42);
        public static readonly Color MutedText = Color.FromArgb(100, 116, 139);
        public static readonly Color Success = Color.FromArgb(22, 163, 74);
        public static readonly Color Danger = Color.FromArgb(220, 38, 38);
        public static readonly Color Info = Color.FromArgb(37, 99, 235);
        public static readonly Color GridHeader = Color.FromArgb(241, 245, 249);
        public static readonly Color GridHeaderText = Color.FromArgb(71, 85, 105);
        public static readonly Color GridAltRow = Color.FromArgb(248, 250, 252);
        public static readonly Color GridSelection = Color.FromArgb(219, 234, 254);
        public static readonly Color GridSelectionText = Color.FromArgb(30, 58, 138);

        public static readonly Font BaseFont = new Font("Segoe UI", 9.75F);
        public static readonly Font LabelFont = new Font("Segoe UI Semibold", 9F);
        public static readonly Font TitleFont = new Font("Segoe UI Semibold", 15F);
        public static readonly Font SubTitleFont = new Font("Segoe UI", 9.5F);
        public static readonly Font SectionFont = new Font("Segoe UI Semibold", 11F);
        public static readonly Font MonoFont = new Font("Cascadia Mono", 9.5F, FontStyle.Regular, GraphicsUnit.Point);

        public static void ApplyForm(Form form)
        {
            form.BackColor = Background;
            form.ForeColor = Text;
            form.Font = BaseFont;
        }

        public static Panel CreateHeader(string title, string subtitle, int height = 72)
        {
            var header = new Panel { Dock = DockStyle.Top, Height = height, BackColor = HeaderBackground, Padding = new Padding(24, 0, 24, 0) };
            var accent = new Panel { Dock = DockStyle.Bottom, Height = 3, BackColor = Primary };
            var titleLabel = new Label { Text = title, Font = TitleFont, ForeColor = HeaderText, AutoSize = true, BackColor = Color.Transparent, Location = new Point(24, 14) };
            var subtitleLabel = new Label { Text = subtitle, Font = SubTitleFont, ForeColor = HeaderSubText, AutoSize = true, BackColor = Color.Transparent, Location = new Point(26, 44) };
            header.Controls.Add(titleLabel);
            header.Controls.Add(subtitleLabel);
            header.Controls.Add(accent);
            return header;
        }

        public static Panel CreateCard()
        {
            var card = new Panel { BackColor = Surface, Padding = new Padding(20) };
            card.Paint += (s, e) =>
            {
                using var pen = new Pen(Border);
                var rect = card.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using var path = RoundedRect(rect, 8);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, path);
            };
            return card;
        }

        public static Label CreateFieldLabel(string text)
        {
            return new Label { Text = text, Font = LabelFont, ForeColor = MutedText, AutoSize = true, BackColor = Color.Transparent };
        }

        public static void StylePrimaryButton(Button button)
        {
            StyleButton(button, Primary, PrimaryHover, PrimaryText, Primary);
        }

        public static void StyleSecondaryButton(Button button)
        {
            StyleButton(button, SecondaryBackground, SecondaryHover, SecondaryText, Border);
        }

        private static void StyleButton(Button button, Color back, Color hover, Color fore, Color border)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = border;
            button.FlatAppearance.MouseOverBackColor = hover;
            button.FlatAppearance.MouseDownBackColor = hover;
            button.BackColor = back;
            button.ForeColor = fore;
            button.Font = LabelFont;
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
            button.EnabledChanged += (s, e) =>
            {
                button.BackColor = button.Enabled ? back : Color.FromArgb(203, 213, 225);
                button.ForeColor = button.Enabled ? fore : Color.White;
            };
        }

        public static void StyleTextBox(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Surface;
            textBox.ForeColor = Text;
            textBox.Font = BaseFont;
        }

        public static void StyleComboBox(ComboBox comboBox)
        {
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.BackColor = Surface;
            comboBox.ForeColor = Text;
            comboBox.Font = BaseFont;
        }

        public static void StyleNumeric(NumericUpDown numeric)
        {
            numeric.BorderStyle = BorderStyle.FixedSingle;
            numeric.BackColor = Surface;
            numeric.ForeColor = Text;
            numeric.Font = BaseFont;
        }

        public static void StyleGrid(DataGridView grid)
        {
            grid.BorderStyle = BorderStyle.None;
            grid.BackgroundColor = Surface;
            grid.GridColor = Border;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AllowUserToResizeRows = false;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersHeight = grid.LogicalToDeviceUnits(36);
            grid.RowTemplate.Height = grid.LogicalToDeviceUnits(32);
            grid.Font = BaseFont;
            grid.ColumnHeadersDefaultCellStyle.BackColor = GridHeader;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = GridHeaderText;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = GridHeader;
            grid.ColumnHeadersDefaultCellStyle.Font = LabelFont;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(grid.LogicalToDeviceUnits(8), 0, 0, 0);
            grid.DefaultCellStyle.BackColor = Surface;
            grid.DefaultCellStyle.ForeColor = Text;
            grid.DefaultCellStyle.SelectionBackColor = GridSelection;
            grid.DefaultCellStyle.SelectionForeColor = GridSelectionText;
            grid.DefaultCellStyle.Padding = new Padding(grid.LogicalToDeviceUnits(8), 0, 0, 0);
            grid.AlternatingRowsDefaultCellStyle.BackColor = GridAltRow;
            grid.Cursor = Cursors.Hand;
        }

        public static void BindStatus(Label label)
        {
            label.Font = BaseFont;
            label.ForeColor = MutedText;
            label.TextChanged += (s, e) =>
            {
                var text = label.Text;
                if (text.Contains("fail", StringComparison.OrdinalIgnoreCase) || text.Contains("required", StringComparison.OrdinalIgnoreCase) || text.Contains("not found", StringComparison.OrdinalIgnoreCase))
                {
                    label.ForeColor = Danger;
                }
                else if (text.Contains("succe", StringComparison.OrdinalIgnoreCase) || text.Contains("saved", StringComparison.OrdinalIgnoreCase) || text.Contains("issue(s)", StringComparison.OrdinalIgnoreCase) || text.Contains("Loaded", StringComparison.OrdinalIgnoreCase))
                {
                    label.ForeColor = Success;
                }
                else
                {
                    label.ForeColor = Info;
                }
            };
        }

        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            var d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
