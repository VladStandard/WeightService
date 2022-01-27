// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataShareCore.DAL.Models
{
    public class MacAddressEntity
    {
        #region Public and private fields and properties

        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                _value = value.Length == 12 ? value : string.Empty;
            }
        }

        public string ValuePrettyLookSpace => !string.IsNullOrEmpty(Value) 
            ? $"{Value[0]}{Value[1]} {Value[2]}{Value[3]} {Value[4]}{Value[5]} " +
              $"{Value[6]}{Value[7]} {Value[8]}{Value[9]} {Value[10]}{Value[11]}"
            : string.Empty;

        public string ValuePrettyLookMinus => !string.IsNullOrEmpty(Value)
            ? $"{Value[0]}{Value[1]}-{Value[2]}{Value[3]}-{Value[4]}{Value[5]}-" +
              $"{Value[6]}{Value[7]}-{Value[8]}{Value[9]}-{Value[10]}{Value[11]}"
            : string.Empty;

        public string ValuePrettyLookColon =>
            $"{Value[0]}{Value[1]}:{Value[2]}{Value[3]}:{Value[4]}{Value[5]}:" +
            $"{Value[6]}{Value[7]}:{Value[8]}{Value[9]}:{Value[10]}{Value[11]}";

        #endregion

        #region Constructor and destructor

        public MacAddressEntity()
        {
            _value = string.Empty;
        }

        public MacAddressEntity(string address)
        {
            _value = address;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return $"{nameof(Value)}: {ValuePrettyLookMinus}.";
        }

        public virtual bool Equals(MacAddressEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                Equals(Value, entity.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MacAddressEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool EqualsNew()
        {
            return Equals(new MacAddressEntity());
        }

        public bool EqualsDefault()
        {
            return Equals(Value, string.Empty);
        }

        public object Clone()
        {
            return new MacAddressEntity
            {
                Value = Value,
            };
        }

        public void Default()
        {
            Value = "000000000000";
        }

        #endregion
    }
}
