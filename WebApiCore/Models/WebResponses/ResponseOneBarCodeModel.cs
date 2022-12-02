using DataCore.Sql.Core;
using DataCore.Sql.TableScaleModels;
using NHibernate.Type;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WebApiCore.Models.WebResponses
{
    [Serializable]
    public class ResponseSingleBarCodeModel: BarCodeModel, ISerializable
    {

        //[XmlIgnore] public virtual PluLabelModel PluLabel { get; set; }

        public ResponseSingleBarCodeModel() { }
        protected ResponseSingleBarCodeModel(SerializationInfo info, StreamingContext context)
        {
            TypeTop = info.GetString(nameof(TypeTop));
            ValueTop = info.GetString(nameof(ValueTop));
            TypeRight = info.GetString(nameof(TypeRight));
            ValueRight = info.GetString(nameof(ValueRight));
            TypeBottom = info.GetString(nameof(TypeBottom));
            ValueBottom = info.GetString(nameof(ValueBottom));
            // PluLabel = (PluLabelModel)info.GetValue(nameof(PluLabel), typeof(PluLabelModel));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // base.GetObjectData(info, context);
            info.AddValue(nameof(TypeTop), TypeTop);
            info.AddValue(nameof(ValueTop), ValueTop);
            info.AddValue(nameof(TypeRight), TypeRight);
            info.AddValue(nameof(ValueRight), ValueRight);
            info.AddValue(nameof(TypeBottom), TypeBottom);
            info.AddValue(nameof(ValueBottom), ValueBottom);
           // info.AddValue(nameof(PluLabel), PluLabel);
        }
    }


}
