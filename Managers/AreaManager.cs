using SuDoKu.Entities;

namespace SuDoKu.Managers
{
    public class AreaManager
    {
        public Area GetRowColumnArea(RowColumn rowColumn)
        {
            switch (((int)rowColumn / 3) + 1)
            {
                case 1: return Area.One;
                case 2: return Area.Two;
                case 3: return Area.Three;
                default: return Area.Zero;
            }
        }
    }
}
