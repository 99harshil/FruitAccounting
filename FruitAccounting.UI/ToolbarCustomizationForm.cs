using FruitAccounting.Core.services;

namespace FruitAccounting.UI
{
    public partial class ToolbarCustomizationForm : Form
    {
        private readonly UserPreferencesService _preferencesService;
        private readonly long _userId;
        private List<string> _currentButtons;

        public List<string> CustomizedButtons { get; private set; } = new();

        public ToolbarCustomizationForm(UserPreferencesService preferencesService, long userId, List<string> currentButtons)
        {
            InitializeComponent();
            _preferencesService = preferencesService;
            _userId = userId;
            _currentButtons = new List<string>(currentButtons);
            CustomizedButtons = new List<string>(_currentButtons);
        }

        private void ToolbarCustomizationForm_Load(object sender, EventArgs e)
        {
            LoadAvailableButtons();
            LoadSelectedButtons();
        }

        private void LoadAvailableButtons()
        {
            lstAvailable.Items.Clear();
            var allButtons = UserPreferencesService.GetAllAvailableButtons();

            foreach (var button in allButtons.Where(b => !_currentButtons.Contains(b)))
            {
                lstAvailable.Items.Add(button);
            }
        }

        private void LoadSelectedButtons()
        {
            lstSelected.Items.Clear();
            foreach (var button in _currentButtons)
            {
                lstSelected.Items.Add(button);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lstAvailable.SelectedItem == null)
            {
                MessageBox.Show("Please select a button to add.", "Info");
                return;
            }

            var button = (string)lstAvailable.SelectedItem;
            _currentButtons.Add(button);
            lstAvailable.Items.Remove(button);
            lstSelected.Items.Add(button);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstSelected.SelectedItem == null)
            {
                MessageBox.Show("Please select a button to remove.", "Info");
                return;
            }

            var button = (string)lstSelected.SelectedItem;
            _currentButtons.Remove(button);
            lstSelected.Items.Remove(button);
            lstAvailable.Items.Add(button);
            lstAvailable.Items.Cast<string>().OrderBy(s => s).ToList();
            LoadAvailableButtons();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lstSelected.SelectedIndex <= 0)
                return;

            int index = lstSelected.SelectedIndex;
            var item = _currentButtons[index];
            _currentButtons.RemoveAt(index);
            _currentButtons.Insert(index - 1, item);
            LoadSelectedButtons();
            lstSelected.SelectedIndex = index - 1;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (lstSelected.SelectedIndex < 0 || lstSelected.SelectedIndex >= lstSelected.Items.Count - 1)
                return;

            int index = lstSelected.SelectedIndex;
            var item = _currentButtons[index];
            _currentButtons.RemoveAt(index);
            _currentButtons.Insert(index + 1, item);
            LoadSelectedButtons();
            lstSelected.SelectedIndex = index + 1;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset toolbar to default buttons?", "Confirm",
                    MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            _currentButtons = UserPreferencesService.GetDefaultToolbarButtons();
            LoadAvailableButtons();
            LoadSelectedButtons();
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {
            CustomizedButtons = new List<string>(_currentButtons);
            await _preferencesService.SaveToolbarButtonsAsync(_userId, CustomizedButtons);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
