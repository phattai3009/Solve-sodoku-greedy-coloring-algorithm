namespace Detai14_Ungdungcuadothivoicacthuattoansapxep
{
    class Cell
    {
        int Row;
        int Col;
        int Value;

        public Cell(int row, int col, int value)
        {
            Row = row;
            Col = col;
            Value = value;
        }

        public int Row_ { get => Row; set => Row = value; }
        public int Col_ { get => Col; set => Col = value; }
        public int Value_ { get => Value; set => Value = value; }
    }
}
