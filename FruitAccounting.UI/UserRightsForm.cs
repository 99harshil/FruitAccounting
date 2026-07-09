using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class UserRightsForm : Form
    {
        private readonly UserService _userService;
        private List<User> _allUsers = new();
        private long _selectedUserId = 0;
        private Dictionary<string, UserPermission> _permissions = new();
        private bool _updatingTree = false;

        public UserRightsForm(UserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private async void UserRightsForm_Load(object sender, EventArgs e)
        {
            await LoadUsers();
            BuildMenuStructure();

            // Wire up tree events
            tvMenuHierarchy.AfterCheck += TvMenuHierarchy_AfterCheck;
            tvMenuHierarchy.AfterSelect += TvMenuHierarchy_AfterSelect;
        }

        private async Task LoadUsers()
        {
            try
            {
                _allUsers = await _userService.GetAllUsersAsync();
                cmbUserIdRight.DataSource = new List<User>(_allUsers);
                cmbUserIdRight.DisplayMember = "DisplayName";
                cmbUserIdRight.ValueMember = "UserId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error");
            }
        }

        private void BuildMenuStructure()
        {
            tvMenuHierarchy.Nodes.Clear();
            tvMenuHierarchy.CheckBoxes = true;

            var menuStructure = new[]
            {
                ("Main", new[] {
                    ("User Manager", new[] { "Create user", "User Rights", "Delete User", "Company" }),
                    ("Log Off", Array.Empty<string>()),
                    ("Exit", Array.Empty<string>())
                }),
                ("Master", new[] {
                    ("Account", new[] { "Main Group", "Sub Group", "Account", "Daybook" }),
                    ("Product", new[] { "Group", "Category", "Item", "Count" }),
                    ("Region", Array.Empty<string>()),
                    ("Country", Array.Empty<string>())
                }),
                ("Account", new[] {
                    ("Receipt", new[] { "Cash", "Bank" }),
                    ("Payment", new[] { "Cash", "Bank", "TDS Payment", "Freight Payment" }),
                    ("Journal", Array.Empty<string>()),
                    ("Bank Reconciliation", Array.Empty<string>()),
                    ("Expense Entry", Array.Empty<string>()),
                    ("TDS Return", Array.Empty<string>()),
                    ("Reports", new[] { "Bank Register", "Cash Register - All", "Cash Register - User Wise", "Journal Register" })
                }),
                ("Domestic", new[] {
                    ("Purchase", Array.Empty<string>()),
                    ("Sales", Array.Empty<string>()),
                    ("Crate", new[] { "Receipt", "Delivery", "Crate Amount Conversion" }),
                    ("Lot Split", Array.Empty<string>()),
                    ("Lot Transfer", Array.Empty<string>()),
                    ("Lot Merge", Array.Empty<string>()),
                    ("Cold Store", Array.Empty<string>()),
                    ("Reports", new[] { "Stock", "Chithi", "Chitha" })
                }),
                ("Desavar", new[] {
                    ("Sales", Array.Empty<string>()),
                    ("Reports", new[] { "Register" })
                }),
                ("Utility", new[] {
                    ("Calculator", Array.Empty<string>()),
                    ("Company Change", Array.Empty<string>()),
                    ("New Year", Array.Empty<string>()),
                    ("Data Check", Array.Empty<string>()),
                    ("Account Merging", Array.Empty<string>()),
                    ("Pending Cheque", Array.Empty<string>()),
                    ("OHCS Update", Array.Empty<string>()),
                    ("RD Update", Array.Empty<string>()),
                    ("Stock Set", Array.Empty<string>()),
                    ("Activity Report", Array.Empty<string>())
                })
            };

            foreach (var (mainMenu, subMenus) in menuStructure)
            {
                var mainNode = tvMenuHierarchy.Nodes.Add(mainMenu);
                mainNode.Tag = mainMenu;

                foreach (var (subMenu1, items) in subMenus)
                {
                    var subNode1 = mainNode.Nodes.Add(subMenu1);
                    subNode1.Tag = $"{mainMenu}/{subMenu1}";

                    foreach (var item in items)
                    {
                        var itemNode = subNode1.Nodes.Add(item);
                        itemNode.Tag = $"{mainMenu}/{subMenu1}/{item}";
                    }
                }

                // Expand the main node so children are visible
                mainNode.Expand();
            }
        }

        private void TvMenuHierarchy_AfterCheck(object? sender, TreeViewEventArgs e)
        {
            if (_updatingTree) return;

            _updatingTree = true;

            try
            {
                // If parent is checked, recursively check all descendants
                if (e.Node.Checked)
                {
                    CheckAllDescendants(e.Node);
                }
                // If parent is unchecked, recursively uncheck all descendants
                else
                {
                    UncheckAllDescendants(e.Node);
                }

                // Update all ancestors up the tree
                UpdateAncestors(e.Node.Parent);
            }
            finally
            {
                _updatingTree = false;
            }
        }

        private void CheckAllDescendants(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = true;
                if (child.Nodes.Count > 0)
                {
                    CheckAllDescendants(child);
                }
            }
        }

        private void UncheckAllDescendants(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = false;
                if (child.Nodes.Count > 0)
                {
                    UncheckAllDescendants(child);
                }
            }
        }

        private void TvMenuHierarchy_BeforeCheck(object? sender, TreeViewCancelEventArgs e)
        {
            // Placeholder for any pre-check logic
        }

        private void UpdateAncestors(TreeNode? node)
        {
            if (node == null) return;

            // Check if all children are checked
            bool allChildrenChecked = node.Nodes.Cast<TreeNode>().All(n => n.Checked);
            bool anyChildrenChecked = node.Nodes.Cast<TreeNode>().Any(n => n.Checked);

            if (allChildrenChecked)
                node.Checked = true;
            else if (!anyChildrenChecked)
                node.Checked = false;
            // If some but not all are checked, leave parent as is

            // Recursively update parent
            UpdateAncestors(node.Parent);
        }

        private void TvMenuHierarchy_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            UpdatePermissionsGrid();
        }

        private void UpdatePermissionsGrid()
        {
            dgvPermissions.Rows.Clear();

            // Show all leaf nodes with their current permissions
            GetAllLeafNodes(tvMenuHierarchy.Nodes);
        }

        private void GetAllLeafNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Nodes.Count == 0) // Only show leaf nodes
                {
                    var key = node.Tag?.ToString() ?? "";
                    var perm = _permissions.ContainsKey(key) ? _permissions[key] : new UserPermission { MenuKey = key };

                    dgvPermissions.Rows.Add(
                        node.Text,
                        perm.CanView ? "Yes" : "No",
                        perm.CanAdd ? "Yes" : "No",
                        perm.CanModify ? "Yes" : "No",
                        perm.CanDelete ? "Yes" : "No"
                    );
                }
                else
                {
                    GetAllLeafNodes(node.Nodes);
                }
            }
        }

        private List<TreeNode> GetAllCheckedNodes(TreeNodeCollection nodes)
        {
            var result = new List<TreeNode>();
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    result.Add(node);
                }
                // ALWAYS recurse into children, even if parent is not checked
                result.AddRange(GetAllCheckedNodes(node.Nodes));
            }
            return result;
        }

        private async void cmbUserIdRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUserIdRight.SelectedIndex < 0 || cmbUserIdRight.SelectedItem == null)
                return;

            if (cmbUserIdRight.SelectedItem is User selectedUser)
            {
                _selectedUserId = selectedUser.UserId;
                _permissions.Clear();  // Clear old permissions when switching users
                await LoadUserPermissions();
            }
        }

        private async Task LoadUserPermissions()
        {
            try
            {
                var permissions = await _userService.GetUserPermissionsAsync(_selectedUserId);
                _permissions = permissions.ToDictionary(p => p.MenuKey);

                // Check nodes based on permissions
                _updatingTree = true;
                try
                {
                    UncheckAllNodes(tvMenuHierarchy.Nodes);
                    CheckPermissionNodes(tvMenuHierarchy.Nodes);
                }
                finally
                {
                    _updatingTree = false;
                }

                UpdatePermissionsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading permissions: {ex.Message}", "Error");
            }
        }

        private void CheckPermissionNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                var key = node.Tag?.ToString() ?? "";
                if (_permissions.ContainsKey(key) && _permissions[key].CanView)
                {
                    node.Checked = true;
                }
                CheckPermissionNodes(node.Nodes);
            }
        }

        public void SelectAllPermissions()
        {
            _updatingTree = true;
            try
            {
                CheckAllNodes(tvMenuHierarchy.Nodes);
            }
            finally
            {
                _updatingTree = false;
            }
            UpdatePermissionsGrid();
        }

        public void ClearAllPermissions()
        {
            _updatingTree = true;
            try
            {
                UncheckAllNodes(tvMenuHierarchy.Nodes);
            }
            finally
            {
                _updatingTree = false;
            }
            UpdatePermissionsGrid();
        }

        private void CheckAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = true;
                CheckAllNodes(node.Nodes);
            }
        }

        private void UncheckAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = false;
                UncheckAllNodes(node.Nodes);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == 0)
            {
                MessageBox.Show("Please select a user", "Info");
                return;
            }

            var checkedNodes = GetAllCheckedNodes(tvMenuHierarchy.Nodes);
            var permissions = new List<UserPermission>();

            // Collect all checked leaf nodes
            foreach (var node in checkedNodes)
            {
                if (node.Nodes.Count == 0) // Only save leaf nodes
                {
                    var key = node.Tag?.ToString() ?? "";
                    permissions.Add(new UserPermission
                    {
                        UserId = _selectedUserId,
                        MenuKey = key,
                        CanView = true,
                        CanAdd = true,
                        CanModify = true,
                        CanDelete = true
                    });
                }
            }

            var (success, message) = await _userService.SaveUserPermissionsAsync(_selectedUserId, permissions);

            if (success)
            {
                // Reload from database to ensure we have the latest data
                await LoadUserPermissions();
                MessageBox.Show(message, "Success");
            }
            else
            {
                MessageBox.Show(message, "Error");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
