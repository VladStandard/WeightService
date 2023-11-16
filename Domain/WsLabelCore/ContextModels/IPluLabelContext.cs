namespace WsLabelCore.ContextModels;

public interface IPluLabelContext
{
    #region Barcodes
    
    string BarCodeTop { get; }
    string BarCodeRight { get; }
    string BarCodeBottom { get; }
    
    #endregion
}