using System;
using System.Windows.Forms;

namespace FruitAccounting.UI
{
    // A DataGridView column whose cells edit through a MaskedTextBox (mask "00/00/0000"),
    // so the user sees/types "__/__/____" instead of a free-text box. Reusable anywhere a
    // grid needs a dd/MM/yyyy date cell (e.g. Bank Reconciliation's CL. Date column).
    public class DataGridViewMaskedDateColumn : DataGridViewColumn
    {
        public DataGridViewMaskedDateColumn() : base(new DataGridViewMaskedDateCell())
        {
        }

        public override DataGridViewCell? CellTemplate
        {
            get => base.CellTemplate;
            set
            {
                if (value != null && value is not DataGridViewMaskedDateCell)
                    throw new InvalidCastException("CellTemplate must be a DataGridViewMaskedDateCell");
                base.CellTemplate = value;
            }
        }
    }

    public class DataGridViewMaskedDateCell : DataGridViewTextBoxCell
    {
        private const string BlankMask = "__/__/____";

        public override Type EditType => typeof(DataGridViewMaskedDateEditingControl);

        public override Type ValueType => typeof(string);

        public override object DefaultNewRowValue => BlankMask;

        public override void InitializeEditingControl(int rowIndex, object? initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            if (DataGridView?.EditingControl is DataGridViewMaskedDateEditingControl control)
            {
                var text = initialFormattedValue as string ?? "";
                control.Text = text == BlankMask ? "" : text;
            }
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle,
            System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter,
            DataGridViewDataErrorContexts context)
        {
            var text = value as string;
            return string.IsNullOrEmpty(text) ? BlankMask : text;
        }
    }

    public class DataGridViewMaskedDateEditingControl : MaskedTextBox, IDataGridViewEditingControl
    {
        private DataGridView? _dataGridView;
        private bool _valueChanged;

        public DataGridViewMaskedDateEditingControl()
        {
            Mask = "00/00/0000";
            PromptChar = '_';
            BorderStyle = BorderStyle.None;
        }

        public DataGridView EditingControlDataGridView
        {
            get => _dataGridView!;
            set => _dataGridView = value;
        }

        public object EditingControlFormattedValue
        {
            get => GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
            set { if (value is string s) Text = s; }
        }

        public int EditingControlRowIndex { get; set; }
        public bool EditingControlValueChanged { get => _valueChanged; set => _valueChanged = value; }
        public Cursor EditingPanelCursor => Cursors.Default;
        public bool RepositionEditingControlOnValueChange => false;

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context) => MaskCompleted ? Text : (Text.Trim('_', '/').Length == 0 ? "" : Text);

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            Font = dataGridViewCellStyle.Font;
            ForeColor = dataGridViewCellStyle.ForeColor;
            BackColor = dataGridViewCellStyle.BackColor;
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            if (selectAll)
                SelectAll();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            _valueChanged = true;
            _dataGridView?.NotifyCurrentCellDirty(true);
            base.OnTextChanged(e);
        }
    }
}
