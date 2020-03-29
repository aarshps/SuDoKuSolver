using SuDoKu.Common;
using SuDoKu.Entities;

namespace SuDoKu.Managers
{
    public class RowColumnManager
    {
        public readonly RowColumn[] rowColumns;

        public RowColumnManager()
        {
            rowColumns = new RowColumn[9]
            {
                RowColumn.Zero,
                RowColumn.One,
                RowColumn.Two,
                RowColumn.Three,
                RowColumn.Four,
                RowColumn.Five,
                RowColumn.Six,
                RowColumn.Seven,
                RowColumn.Eight
            };
        }

        public RowColumn GetNextRowColumn(RowColumn rowColumn) => rowColumn.GetNextItem(rowColumns);

        public RowColumn GetFirstRowColumn() => rowColumns.GetFirstItem();

        public RowColumn GetLastRowColumn() => rowColumns.GetLastItem();
    }
}
