using SuDoKu.Common;
using SuDoKu.Entities;

namespace SuDoKu.Managers
{
    public class ValueManager
    {
        public readonly Value[] values;

        public ValueManager()
        {
            values = new Value[9]
            {
                Value.One,
                Value.Two,
                Value.Three,
                Value.Four,
                Value.Five,
                Value.Six,
                Value.Seven,
                Value.Eight,
                Value.Nine
            };
        }

        public Value GetNextValue(Value value) => value.GetNextItem(values);

        public Value GetFirstValue() => Value.One;

        public Value GetLastValue() => values.GetLastItem();
    }
}
