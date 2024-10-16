using System.ComponentModel;

namespace Task1QQQ.Enums
{
    public enum Sorts
    {
        [Description("Без сортировки")]
        None,
        [Description("По имени")]
        ByName,
        [Description("По плотности")]
        ByDensity,
        [Description("По удельной теплоте")]
        ByCalorificValue,
        [Description("По минимальной допустимой концетрации")]
        ByMinConcentration,
        [Description("По максимальной допустимой концетрации")]
        ByMaxConcentration

    }
}
