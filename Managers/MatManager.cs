using SuDoKu.Entities;
using System;
using System.Collections.Generic;

namespace SuDoKu.Managers
{
    public class MatManager
    {
        private Mat mat;
        private Mat testMat;
        private Mat lastTestMat;
        private readonly RowColumnManager rowColumnManager;
        private readonly ValueManager valueManager;
        private readonly AreaManager areaManager;
        private readonly bool isDebug = false;

        public MatManager(Mat mat)
        {
            this.mat = mat;
            rowColumnManager = new RowColumnManager();
            valueManager = new ValueManager();
            areaManager = new AreaManager();
            ResetTestMat();
        }

        private void ResetTestMat()
        {
            testMat = new Mat(mat.Values);
        }

        private Value GetMatValue(Mat mat, RowColumn row, RowColumn column) => mat.Values[(int)row, (int)column];

        private void SetMatValue(Mat mat, Value value, RowColumn row, RowColumn column)
        {
            ConsoleManager.LogLine($"Setting value : {(int)value} at row : {(int)row}, column : {(int)column}");

            mat.Values[(int)row, (int)column] = value;
        }

        private bool ValidateMatValue(Value value, RowColumn row, RowColumn column) => ValidateZero(row, column) &&
            ValidateRow(value, row) && ValidateColumn(value, column) && ValidateArea(value, row, column);

        private bool ValidateZero(RowColumn row, RowColumn column) => GetMatValue(testMat, row, column) == Value.Zero;

        private bool ValidateRow(Value value, RowColumn row)
        {
            foreach (var column in rowColumnManager.rowColumns)
            {
                if (GetMatValue(testMat, row, column) == value)
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidateColumn(Value value, RowColumn column)
        {
            foreach (var row in rowColumnManager.rowColumns)
            {
                if (GetMatValue(testMat, row, column) == value)
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidateArea(Value value, RowColumn row, RowColumn column)
        {
            var rowArea = areaManager.GetRowColumnArea(row);
            var columnArea = areaManager.GetRowColumnArea(column);

            foreach (var indexRow in rowColumnManager.rowColumns)
            {
                foreach (var indexColumn in rowColumnManager.rowColumns)
                {
                    if (areaManager.GetRowColumnArea(indexRow) == rowArea &&
                        areaManager.GetRowColumnArea(indexColumn) == columnArea &&
                            GetMatValue(testMat, indexRow, indexColumn) == value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool CheckTestMatSuccess()
        {
            lastTestMat = new Mat(testMat.Values);
            foreach (var row in rowColumnManager.rowColumns)
            {
                foreach (var column in rowColumnManager.rowColumns)
                {
                    var value = GetMatValue(testMat, row, column);
                    if (value == Value.Zero)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static void ShowMat(Mat mat)
        {
            Console.Write("\n");
            var values = mat.Values;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var value = values[i, j] == Value.Zero ? " " : ((int)values[i, j]).ToString();
                    Console.Write($"{value} ");
                }
                Console.Write("\n");
            }
        }

        public Mat SolveMat()
        {
            while (!CheckTestMatSuccess())
            {
                foreach (var value in valueManager.values)
                {
                    var settableValueTestRuns = new List<TestRun>();

                    foreach (var row in rowColumnManager.rowColumns)
                    {
                        foreach (var column in rowColumnManager.rowColumns)
                        {
                            if (ValidateMatValue(value, row, column))
                            {
                                settableValueTestRuns.Add(new TestRun
                                {
                                    Value = value,
                                    Row = row,
                                    Column = column
                                });
                            }
                        }
                    }

                    var finalSettableValueTestRuns = new List<TestRun>(settableValueTestRuns);
                    foreach (var testRun in settableValueTestRuns)
                    {
                        var rowCount = 0;
                        var columnCount = 0;
                        var areaCount = 0;
                        foreach (var compareTestRun in settableValueTestRuns)
                        {
                            var testRunRowColumnArea = new
                            {
                                RowArea = areaManager.GetRowColumnArea(testRun.Row),
                                ColumnArea = areaManager.GetRowColumnArea(testRun.Column)
                            };
                            var compareTestRunRowColumnArea = new
                            {
                                RowArea = areaManager.GetRowColumnArea(compareTestRun.Row),
                                ColumnArea = areaManager.GetRowColumnArea(compareTestRun.Column)
                            };

                            if (testRun.GetHashCode() == compareTestRun.GetHashCode())
                            {
                                continue;
                            }

                            // row check
                            if (testRun.Row == compareTestRun.Row)
                            {
                                rowCount++;
                            }
                            // column check
                            if (testRun.Column == compareTestRun.Column)
                            {
                                columnCount++;
                            }
                            // area check
                            if (testRunRowColumnArea.GetHashCode() == compareTestRunRowColumnArea.GetHashCode())
                            {
                                areaCount++;
                            }
                        }

                        if (rowCount > 0 && columnCount > 0 && areaCount > 0)
                        {
                            finalSettableValueTestRuns.Remove(testRun);
                        }
                    }

                    if (finalSettableValueTestRuns.Count > 0)
                    {
                        foreach (var testRun in finalSettableValueTestRuns)
                        {
                            SetMatValue(testMat, testRun.Value, testRun.Row, testRun.Column);

                            if (ConsoleManager.isDebug)
                            {
                                ShowMat(testMat);
                            }
                        }
                    }
                }

                if (testMat.GetHashCode() == lastTestMat.GetHashCode())
                {
                    Console.WriteLine($"Sorry, predictive solving not yet cracked...");
                    return testMat;
                }
            }

            return testMat;
        }
    }
}
