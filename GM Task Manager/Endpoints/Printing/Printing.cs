using GM_Task_Manager.Store.Entities.ToDoTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace GM_Task_Manager.Endpoints.Printing
{
    public class Printing
    {
        /// <summary>
        /// Запускает процесс печати в файл.
        /// </summary>
        /// <param name="items"></param>
        public void Print(IEnumerable<ToDoTask> items)
        {
            //Код здесь мной лишь в попыхах подредачен, поэтому может содержать излишки и т.п.. Это был код под datagrid с просторов интерета)

            PrintDialog printDialog = new PrintDialog();
            var res = printDialog.ShowDialog();
            if (res == true)
            {
                FlowDocument fd = new FlowDocument();

                Paragraph p = new Paragraph(new Run("ToDo List"));
                p.FontSize = 18;
                fd.Blocks.Add(p);

                Table table = new Table();
                TableRowGroup tableRowGroup = new TableRowGroup();
                TableRow r = new TableRow();
                fd.PageWidth = printDialog.PrintableAreaWidth;
                fd.PageHeight = printDialog.PrintableAreaHeight;
                fd.BringIntoView();

                fd.TextAlignment = TextAlignment.Center;
                fd.ColumnWidth = 500;
                table.CellSpacing = 0;

                var headerAction = () =>
                {
                    r.Cells.Last().ColumnSpan = 6;
                    r.Cells.Last().Padding = new Thickness(4);
                    r.Cells.Last().BorderBrush = Brushes.Black;
                    r.Cells.Last().FontWeight = FontWeights.Bold;
                    r.Cells.Last().Background = Brushes.White;
                    r.Cells.Last().Foreground = Brushes.Black;
                    r.Cells.Last().BorderThickness = new Thickness(2, 2, 2, 2);
                };
                var regularRowAction = () =>
                {
                    r.Cells.Last().ColumnSpan = 6;
                    r.Cells.Last().Padding = new Thickness(2);
                    r.Cells.Last().BorderBrush = Brushes.Black;
                    r.Cells.Last().FontWeight = FontWeights.Normal;
                    r.Cells.Last().Background = Brushes.White;
                    r.Cells.Last().Foreground = Brushes.Black;
                    r.Cells.Last().BorderThickness = new Thickness(1, 1, 1, 1);
                };

                //Добавление заголовков

                r.Cells.Add(new TableCell(new Paragraph(new Run("Name"))));
                headerAction();
                r.Cells.Add(new TableCell(new Paragraph(new Run("Description"))));
                headerAction();
                r.Cells.Add(new TableCell(new Paragraph(new Run("Status"))));
                headerAction();
                r.Cells.Add(new TableCell(new Paragraph(new Run("Created"))));
                headerAction();
                r.Cells.Add(new TableCell(new Paragraph(new Run("Updated"))));
                headerAction();
                r.Cells.Add(new TableCell(new Paragraph(new Run("Deadline"))));
                headerAction();

                tableRowGroup.Rows.Add(r);
                table.RowGroups.Add(tableRowGroup);


                //Добавление данных
                foreach (var item in items)
                {


                    table.BorderBrush = Brushes.Gray;
                    table.BorderThickness = new Thickness(1, 1, 0, 0);
                    //table.FontStyle = dataGrid.FontStyle;
                    //table.FontFamily = dataGrid.FontFamily;
                    table.FontSize = 13;
                    tableRowGroup = new TableRowGroup();
                    r = new TableRow();

                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.Name.ToString()))));
                    regularRowAction();
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.Description.ToString()))));
                    regularRowAction();
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.Status.ToString()))));
                    regularRowAction();
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.TimeCreated.ToString(("HH:mm dd.MM.yy"))))));
                    regularRowAction();
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.TimeStatusUpdated.ToString(("HH:mm dd.MM.yy"))))));
                    regularRowAction();
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.TimeDeadline.ToString(("HH:mm dd.MM.yy"))))));
                    regularRowAction();


                    tableRowGroup.Rows.Add(r);
                    table.RowGroups.Add(tableRowGroup);
                }
                fd.Blocks.Add(table);
                printDialog.PrintDocument(((IDocumentPaginatorSource)fd).DocumentPaginator, "");
            }
        }
    }
}
