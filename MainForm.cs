using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public partial class MainForm : Form
{
    private ImageSearchEngine _searchEngine = new ImageSearchEngine();

    public MainForm()
    {
        InitializeComponent();
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            var queryImage = Image.FromFile(txtPath.Text);
            List<SearchResult> results = _searchEngine.Search(queryImage);
            dataGridView.DataSource = results;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }
}    