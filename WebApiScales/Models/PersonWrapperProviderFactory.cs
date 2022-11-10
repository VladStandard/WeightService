//using Microsoft.AspNetCore.Mvc.Formatters.Xml;
//using WebApiCore.Common;

//namespace WebApiScales.Models;

//public class BarcodeBottomWrapper : IUnwrappable
//{
//    public string Const1 { get; set; } = string.Empty;
//    public string Gtin { get; set; } = string.Empty;
//    public string Const2 { get; set; } = string.Empty;
//    public string Weight { get; set; } = string.Empty;
//    public string Const3 { get; set; } = string.Empty;
//    public string Date { get; set; } = string.Empty;
//    public string Const4 { get; set; } = string.Empty;
//    public string PartNumber { get; set; } = string.Empty;

//    public BarcodeBottomWrapper() { }

//    public BarcodeBottomWrapper(BarcodeBottomModel barcodeBottom)
//    {
//        Const1 = barcodeBottom.Const1;
//        Gtin = barcodeBottom.Gtin;
//        Const2 = barcodeBottom.Const2;
//        Weight = barcodeBottom.Weight;
//        Const3 = barcodeBottom.Const3;
//        Date = barcodeBottom.Date;
//        Const4 = barcodeBottom.Const4;
//        PartNumber = barcodeBottom.PartNumber;
//    }

//    //public override string ToString()
//    //{
//    //    return string.Format("{0}, {1}, {2}", Id, Name, Age);
//    //}

//    public object Unwrap(Type declaredType)
//    {
//        return new BarcodeBottomModel()
//        {
//            Const1 = Const1,
//            Gtin = Gtin,
//            Const2 = Const2,
//            Weight = Weight,
//            Const3 = Const3,
//            Date = Date,
//            Const4 = Const4,
//            PartNumber = PartNumber,
//        };
//    }
//}

//public class BarcodeBottomWrapperProvider : IWrapperProvider
//{
//    public object Wrap(object obj)
//    {
//        //BarcodeBottomModel ? barcodeBottom = obj as BarcodeBottomModel;

//        //if (barcodeBottom == null)
//        //{
//        //    return obj;
//        //}

//        ////return new BarcodeBottomWrapper(barcodeBottom);
//        //return barcodeBottom;
//        return obj is BarcodeBottomModel barcodeBottom ? barcodeBottom : obj;
//    }

//    public Type WrappingType
//    {
//        get
//        {
//            return typeof(BarcodeBottomWrapper);
//        }
//    }
//}

//public class BarcodeBottomWrapperProviderFactory : IWrapperProviderFactory
//{
//    public IWrapperProvider GetProvider(WrapperProviderContext context)
//    {
//        if (context.DeclaredType == typeof(BarcodeBottomModel))
//        {
//            return new BarcodeBottomWrapperProvider();
//        }

//        return null;
//    }
//}
