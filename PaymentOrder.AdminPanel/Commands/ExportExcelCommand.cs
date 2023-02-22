using ClosedXML.Excel;
using Microsoft.Win32;
using PaymentOrder.AdminPanel.Attributes;
using PaymentOrder.AdminPanel.Models;
using PaymentOrder.AdminPanel.Utils;
using PaymentOrder.AdminPanel.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PaymentOrder.AdminPanel.Commands
{
    public class ExcelExportModel
    {
        public PropertyInfo PropertyInfo;
        public ExcelDisplayAttribute DisplayAttribute;
    }

    public class ExportExcelCommand<T> : BaseCommand where T : BaseModel, new()
    {
        private readonly BaseControlViewModel<T> _viewModel;
        public ExportExcelCommand(BaseControlViewModel<T> viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                DefaultExt = ".xlsx",
                FileName = "Data List"
            };

            var result = saveFileDialog.ShowDialog();

            if (result == false)
                return;

            using (XLWorkbook workbook = new XLWorkbook())
            {
                DataTable table = new DataTable();

                var propInfos = typeof(T).GetProperties();
                var exportModels = new List<ExcelExportModel>();

                foreach (var propInfo in propInfos)
                {
                    var ignoreAttributes = propInfo.GetCustomAttributes(typeof(ExcelIgnoreAttribute), false);

                    if (ignoreAttributes.Length > 0)
                        continue;

                    var displayAttributes = propInfo.GetCustomAttributes(typeof(ExcelDisplayAttribute), false);
                    var displayAttribute = displayAttributes.FirstOrDefault() as ExcelDisplayAttribute;

                    ExcelExportModel model = new ExcelExportModel()
                    {
                        PropertyInfo = propInfo,
                        DisplayAttribute = displayAttribute
                    };

                    exportModels.Add(model);
                }

                exportModels = exportModels.OrderBy(x => x.DisplayAttribute?.ColumnNo).ToList();

                foreach (var exportModel in exportModels)
                {
                    string columnName = exportModel.DisplayAttribute?.Name ?? exportModel.PropertyInfo.Name;

                    table.Columns.Add(columnName);
                }

                foreach (var value in _viewModel.Values)
                {
                    object[] propValues = new object[exportModels.Count];

                    for (int i = 0; i < exportModels.Count; i++)
                    {
                        var exportModel = exportModels[i];
                        var propInfo = exportModel.PropertyInfo;

                        var propValue = propInfo.GetValue(value);

                        if (propValue is BaseModel model)
                        {
                            propValues[i] = model.ToExcelString();
                        }
                        else
                        {
                            propValues[i] = propValue;
                        }
                    }

                    table.Rows.Add(propValues);
                }

                var worksheet = workbook.AddWorksheet("Data List");

                worksheet.Cell(1, 1).InsertTable(table);
                worksheet.Columns().AdjustToContents();

                workbook.SaveAs(saveFileDialog.FileName);

                _viewModel.Message = Constants.OperationSuccessMessage;
                DoAnimation(_viewModel.ErrorDialog);
            }
        }
    }
}
